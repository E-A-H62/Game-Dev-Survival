using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    [SerializeField]
    private Dialogue dialogue;

    [SerializeField]
    private DialogueManager dialogueManager;

    [SerializeField]
    private KeyCode interactKey = KeyCode.E;

    private bool playerInRange;

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(interactKey))
        {
            dialogueManager.BeginNpcDialogue(dialogue);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}