using UnityEngine;

public class EnemySwordDamage : MonoBehaviour
{
    public int damage = 10;
    public float damageCooldown = 1f;

    private float lastDamageTime;

    void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (Time.time < lastDamageTime + damageCooldown) return;

        Player player = other.GetComponentInParent<Player>();

        if (player != null)
        {
            lastDamageTime = Time.time;
            player.TakeDamage(damage);
            Debug.Log("Player hit by sword for " + damage + ". Current health: " + player.Health);
        }
        else
        {
            Debug.LogWarning("Hit Player tag, but no Player script found in parent.");
        }
    }
}