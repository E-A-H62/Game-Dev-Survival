using UnityEngine;
using TMPro;
using System.Collections;
using System.Text;
using UnityEngine.UI;
using StarterAssets;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    GameObject dialoguePanel;

    [SerializeField]
    TextMeshProUGUI dialogueText;

    [SerializeField]
    GameObject buttonPrefab;

    [SerializeField]
    Transform buttonsParent;

    [SerializeField]
    StarterAssetsInputs playerController;

    [Header("Enemy Defeat Dialogue")]
    [SerializeField]
    Dialogue dialogueAfterEnemiesDefeated;

    [SerializeField]
    int enemiesNeededForDialogue = 2;

    private int defeatedEnemies = 0;
    public bool EnemiesDefeatedDialogueReady { get; private set; }

    private Coroutine typingCoroutine;

    public void BeginDialogue(Dialogue dialogue)
    {
        if (dialogue == null)
        {
            Debug.LogWarning("Dialogue is missing.");
            return;
        }

        playerController.ToggleMovement(false);
        playerController.cursorLocked = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        dialoguePanel.SetActive(true);

        ClearChoices();

        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        dialogueText.text = "";
        typingCoroutine = StartCoroutine(TypeText(dialogue));
    }

    public void BeginNpcDialogue(Dialogue defaultDialogue)
    {
        if (EnemiesDefeatedDialogueReady && dialogueAfterEnemiesDefeated != null)
        {
            BeginDialogue(dialogueAfterEnemiesDefeated);
        }
        else
        {
            BeginDialogue(defaultDialogue);
        }
    }

    public void EnemyDefeated()
    {
        if (EnemiesDefeatedDialogueReady) return;

        defeatedEnemies++;

        Debug.Log("Enemy defeated. Count: " + defeatedEnemies + "/" + enemiesNeededForDialogue);

        if (defeatedEnemies >= enemiesNeededForDialogue)
        {
            EnemiesDefeatedDialogueReady = true;
            Debug.Log("Enemy defeat dialogue is now ready. Talk to the NPC again.");
        }
    }

    IEnumerator TypeText(Dialogue dialogue)
    {
        StringBuilder textToShow = new StringBuilder();

        for (int i = 0; i < dialogue.DialogueText.Length; i++)
        {
            textToShow.Append(dialogue.DialogueText[i]);
            dialogueText.text = textToShow.ToString();

            yield return new WaitForSeconds(1f / 20f);
        }

        ShowChoices(dialogue);
    }

    void ShowChoices(Dialogue dialogue)
    {
        ClearChoices();

        if (dialogue.Choices.Count == 0)
        {
            GameObject endButton = Instantiate(buttonPrefab, buttonsParent);

            endButton.GetComponentInChildren<TextMeshProUGUI>().text = "End";

            endButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                EndDialogue();
            });

            return;
        }

        foreach (Dialogue choice in dialogue.Choices)
        {
            GameObject newButton = Instantiate(buttonPrefab, buttonsParent);

            newButton.GetComponentInChildren<TextMeshProUGUI>().text = choice.OptionName;

            Dialogue nextDialogue = choice;

            newButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                BeginDialogue(nextDialogue);
            });
        }
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false);

        ClearChoices();

        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }

        dialogueText.text = "";

        playerController.ToggleMovement(true);
        playerController.cursorLocked = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void ClearChoices()
    {
        foreach (Transform child in buttonsParent)
        {
            Destroy(child.gameObject);
        }
    }
}