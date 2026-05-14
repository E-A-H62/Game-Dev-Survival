using UnityEngine;
using System.Collections;

public class EnemyProjectile : MonoBehaviour
{
    public int damage = 10;
    public float lifetime = 5f;
    public float floorDestroyDelay = 3f;

    private bool hitFloor = false;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter(Collision collision)
    {
        Collider other = collision.collider;

        //Debug.Log(
           // "Projectile collided with: " + other.name +
            //" | Tag: " + other.tag +
           // " | Layer: " + LayerMask.LayerToName(other.gameObject.layer) +
           // " | Parent: " + (other.transform.parent != null ? other.transform.parent.name : "No parent")
        //);

        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponentInParent<Player>();

            if (player != null)
            {
                player.TakeDamage(damage);
                Debug.Log("Player hit for " + damage + ". Current health: " + player.Health);
            }
            else
            {
                Debug.LogWarning("Hit Player tag, but no Player script found in parent.");
            }

            Destroy(gameObject);
        }
        else if (!hitFloor)
        {
            hitFloor = true;
            Debug.Log("Hit non-player object: " + other.name);
            StartCoroutine(DestroyAfterFloorDelay());
        }
    }

    IEnumerator DestroyAfterFloorDelay()
    {
        yield return new WaitForSeconds(floorDestroyDelay);
        Destroy(gameObject);
    }
}