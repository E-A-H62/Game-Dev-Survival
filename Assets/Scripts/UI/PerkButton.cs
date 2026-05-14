using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class PerkButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public string perkName;
    [TextArea] public string description;
    public string perkRequirement;

    public GameObject lockOverlay;
    public SkillUI parentSkill;

    private bool unlocked = false;

    public int perkPointsCost;

    public void OnPointerEnter(PointerEventData eventData)
    {
        string displayReq = unlocked ? "" : perkRequirement;

        parentSkill.SetPerkData(perkName, description, displayReq);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        parentSkill.SetPerkData("", "", "");
    }

    public void OnClickUnlock()
    {
        if (unlocked) return;

        if (parentSkill.TryUsePerkPoint(perkPointsCost))
        {
            unlocked = true;
            lockOverlay.SetActive(false);
        }
    }
}
