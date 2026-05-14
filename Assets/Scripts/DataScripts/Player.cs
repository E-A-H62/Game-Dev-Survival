using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private const int BASE_HEALTH = 100;
    private static readonly int[] BASE_SKILLS = new int[] { 1, 1, 1, 1, 1, 1 };

    [field: SerializeField] public int Health { get; private set; }
    [field: SerializeField] public int MaxHealth { get; private set; }

    [SerializeField] int[] Skills;
    [SerializeField] List<ItemEntry> Inventory;
    [SerializeField] Durable Armor;

    [SerializeField] private string mainMenuSceneName = "MainMenu";

    private bool isDead = false;

    void Awake()
    {
        Health = BASE_HEALTH;
        MaxHealth = BASE_HEALTH;
    }

    public void Heal(int change)
    {
        if (isDead)
        {
            return;
        }

        if (Health + change > MaxHealth)
        {
            Health = MaxHealth;
        }
        else
        {
            Health += change;
        }

        if (Health < 0)
        {
            Health = 0;
        }
    }

    public void TakeDamage(int damage, bool ignoreArmor = false)
    {
        if (isDead)
        {
            return;
        }

        if (!ignoreArmor && Armor != null)
        {
            damage *= (100 - Armor.Efficiency) / 100;
        }

        Health -= damage;

        if (Health <= 0)
        {
            Health = 0;
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        Debug.Log("Player died. Loading main menu.");

        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void EquipArmor(int inventoryIndex)
    {
        ItemEntry armorEntry = Inventory[inventoryIndex];

        if (armorEntry.Count > 1)
        {
            armorEntry.Count--;
        }
        else
        {
            Inventory.Remove(armorEntry);
        }

        Armor = (Durable)armorEntry.EntryItem;
    }

    public void AddItem(Item item)
    {
        foreach (ItemEntry itemEntry in Inventory)
        {
            if (itemEntry.EntryItem.Equals(item))
            {
                itemEntry.Count++;
                return;
            }
        }

        Inventory.Add(new ItemEntry(item, 1));
    }

    public void RemoveItem(Item item, int count)
    {
        foreach (ItemEntry itemEntry in Inventory)
        {
            if (itemEntry.EntryItem.Equals(item))
            {
                if (itemEntry.Count < count)
                {
                    throw new System.Exception("Player does not have " + count + " " + item.name + " to remove!");
                }

                itemEntry.Count -= count;

                if (itemEntry.Count == 0)
                {
                    Inventory.Remove(itemEntry);
                }

                break;
            }
        }
    }
}