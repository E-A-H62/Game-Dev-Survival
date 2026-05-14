using UnityEngine;

public class AxeAttack : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private int damage = 5;
    [SerializeField] private float attackCooldown = 0.8f;

    public AxeSwing axeSwing;

    private float nextAttackTime;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + attackCooldown;
            //Debug.Log("Mouse Button Down Axe");
        }
    }

    private void Attack()
    {
        axeSwing.Swing();

        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        //Debug.DrawRay(ray.origin, ray.direction * attackRange, Color.red, 1f);

        if (Physics.Raycast(ray, out RaycastHit hit, attackRange, ~0, QueryTriggerInteraction.Collide))
        {
            Debug.Log("Ray hit: " + hit.collider.gameObject.name);

            if (hit.collider.CompareTag("EnemyHitbox"))
            {
                Enemy enemy = hit.collider.GetComponentInParent<Enemy>();

                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }
            }
        }
    }
}