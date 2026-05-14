using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager Instance { get; set; }

    public bool isOpen;
    public GameObject skillUIPanel;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) && !isOpen)
        {
            OpenSkillUI();
        }
        else if (Input.GetKeyDown(KeyCode.K) && isOpen)
        {
            CloseSkillUI();
        }
    }

    private void CloseSkillUI()
    {
        isOpen = false;
        skillUIPanel.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OpenSkillUI()
    {
        isOpen = true;
        skillUIPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
