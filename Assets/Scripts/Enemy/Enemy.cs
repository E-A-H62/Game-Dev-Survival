using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Basic enemy health
    [SerializeField] int Health = 10;
    [SerializeField] int MaxHealth = 10;

    [SerializeField] int Armor = 0;
    [SerializeField] int Damage = 0;

	public void TakeDamage(int amount)
    {
        int finalDamage = Mathf.Max(amount - Armor, 0);

        Health -= finalDamage;

        Debug.Log($"{gameObject.name} took {finalDamage} damage. Health: {Health}/{MaxHealth}");

        if (Health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();

        if (dialogueManager != null)
        {
            dialogueManager.EnemyDefeated();
        }

        Debug.Log("Destroying object: " + gameObject.name);
        Debug.Log("Parent: " + (transform.parent != null ? transform.parent.name : "No parent"));

        Destroy(gameObject);
    }

    /*
    [SerializeField] List<(ItemEntry, int)> Drops;

    public List<ItemEntry> RollDrops()
    {
        List<ItemEntry> rolledDrops = new(); // Same as "new List<ItemEntry>();"
        System.Random rand = new();

        foreach ((ItemEntry, int) dropRate in Drops)
        {
            if (rand.Next(1, 101) <= dropRate.Item2)
            {
                rolledDrops.Add(dropRate.Item1);
            }
        }

        return rolledDrops;
    }
    */
}