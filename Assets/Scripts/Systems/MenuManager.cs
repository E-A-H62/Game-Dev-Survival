using UnityEngine;

public class MenuManager : MonoBehaviour
{

    public GameObject menuCanvas;
    public GameObject UICanvas;
    public GameObject menu;

    public bool isMenuOpen;

    public static MenuManager Instance { get; set; }

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) && !isMenuOpen)
        {
            UICanvas.SetActive(false);
            menuCanvas.SetActive(true);
            isMenuOpen = true;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if (Input.GetKeyDown(KeyCode.M) && isMenuOpen)
        {
            menu.SetActive(true);
            UICanvas.SetActive(true);
            menuCanvas.SetActive(false);
            isMenuOpen = false;

            if (!InventorySystem.Instance.isOpen)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}
