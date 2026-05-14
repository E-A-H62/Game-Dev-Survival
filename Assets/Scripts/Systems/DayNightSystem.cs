using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class DayNightSystem : MonoBehaviour
{
    public Light directionalLight;

    public float dayDurationInSeconds = 360.0f; // 6 minutes* 60seconds = 360 seconds --> each day is 6 minutes right now
    public int currentHour;
    float currentTimeOfDay = 0.35f; // about 8 in the morning

    public List<SkyboxTimeMapping> timeMappings;

    float blendingFactor = 0.0f;

    bool lockNextDayTrigger = false;

    public TextMeshProUGUI timeUI;

    // Update is called once per frame
    void Update()
    {
        // calculate current time of day
        currentTimeOfDay += Time.deltaTime / dayDurationInSeconds;
        currentTimeOfDay %= 1.0f; // ensure it's between 0 and 1

        currentHour = Mathf.FloorToInt(currentTimeOfDay * 24);

        timeUI.text = $"{currentHour:00}:00";

// update light rotation based on current time of day
        directionalLight.transform.rotation = Quaternion.Euler(new Vector3((currentTimeOfDay * 360.0f) - 90.0f, 170.0f, 0.0f));

        // update skybox based on current time of day
        UpdateSkybox();
    }

    private void UpdateSkybox()
    {
        // find appropriate skybox material based on current time of day
        Material currentSkybox = null;
        foreach (SkyboxTimeMapping mapping in timeMappings)
        {
            if (currentHour == mapping.hour)
            {
                currentSkybox = mapping.skyboxMaterial;

                if (currentSkybox.shader != null)
                {
                    if (currentSkybox.shader.name == "Custom/SkyboxTransition")
                    {
                        blendingFactor += Time.deltaTime;
                        blendingFactor = Mathf.Clamp01(blendingFactor);
                        currentSkybox.SetFloat("_TransitionFactor", blendingFactor);
                    }
                    else
                    {
                        blendingFactor = 0.0f;
                    }
                }
                break;
            }
        }

        if (currentHour == 0 && lockNextDayTrigger == false)
        {
            TimeManager.Instance.TriggerNextDay();
            lockNextDayTrigger = true;
        }

        if (currentHour != 0)
        {
            lockNextDayTrigger = false;
        }

        if (currentSkybox != null)
        {
            RenderSettings.skybox = currentSkybox;
        }
    }

}

[System.Serializable]
public class SkyboxTimeMapping{
    public string phaseName;
    public int hour; // hour of the day (0-23)
    public Material skyboxMaterial; // skybox material to use for this time of day
}