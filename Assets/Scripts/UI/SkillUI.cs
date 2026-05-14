using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class SkillUI : MonoBehaviour
{
    [Header("Skill Info")]
    public string skillName;
    public int currentLevel = 0;
    public float currentXP = 0;
    public float xpToNextLevel = 100;

    [Header("UI")]
    public Slider xpSlider;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI xpText;
    public TextMeshProUGUI perkPointsText;

    public int perkPoints = 0;

    public TextMeshProUGUI perkNameUI;
    public TextMeshProUGUI perkDescriptionUI;
    public TextMeshProUGUI perkRequirementUI;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateUI();
        SetPerkData("", "", "");
    }

    public void AddXP(float amount)
    {
        currentXP += amount;
        while (currentXP >= xpToNextLevel)
        {
            currentXP -= xpToNextLevel;
            currentLevel++;
            perkPoints++;
            xpToNextLevel *= 1.25f;
        }
        UpdateUI();
    }

    public bool TryUsePerkPoint(int perkPointsCost)
    {
        if (perkPoints < perkPointsCost) return false;

        perkPoints -= perkPointsCost;
        UpdateUI();
        return true;
    }

    public void UpdateUI()
    {
        levelText.text = "Level: " + currentLevel;
        xpText.text = "XP: " + currentXP + "/" + xpToNextLevel;
        xpSlider.value = currentXP / xpToNextLevel;
        perkPointsText.text = "Perk Points: " + perkPoints;
    }

    public void SetPerkData(string name, string description, string req)
    {
        perkNameUI.text = name;
        perkDescriptionUI.text = description;
        perkRequirementUI.text = req;
    }
}
