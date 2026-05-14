using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    private Slider slider;
    private Player playerScript;

    [SerializeField] private TMP_Text healthCounter;
    [SerializeField] private GameObject player;

    void Awake()
    {
        slider = GetComponent<Slider>();

        if (player != null)
        {
            playerScript = player.GetComponent<Player>();

            if (playerScript == null)
            {
                playerScript = player.GetComponentInParent<Player>();
            }
        }
    }

    void Update()
    {
        if (playerScript == null)
        {
            healthCounter.text = "No Player";
            slider.value = 0f;
            return;
        }

        float currentHealth = playerScript.Health;
        float maxHealth = playerScript.MaxHealth;

        if (maxHealth <= 0)
        {
            slider.value = 0f;
            healthCounter.text = "0 / 0";
            return;
        }

        float fillValue = currentHealth / maxHealth;
        slider.value = fillValue;
        healthCounter.text = currentHealth + " / " + maxHealth;
    }
}