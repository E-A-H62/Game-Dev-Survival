using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public bool playerInRange;

    public Item ItemData;
 
    public string GetItemName()
    {
        return ItemData.Name;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && playerInRange && SelectionManager.Instance.onTarget)
        {
            // if inventory NOT full
            if (!InventorySystem.Instance.CheckIfFull())
            {
                Debug.Log("Item picked up: " + ItemData.Name);
                InventorySystem.Instance.AddToInventory(ItemData);
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Inventory is full");
            }
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