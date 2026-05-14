using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public int dayInGame = 1;
    public TextMeshProUGUI dayUI;

    private void Start()
    {
        dayUI.text = "Day: " + dayInGame;
    }

    public void TriggerNextDay()
    {
        dayInGame++;
        Debug.Log("Day " + dayInGame + " started");
        dayUI.text = "Day: " + dayInGame;
    }
}
