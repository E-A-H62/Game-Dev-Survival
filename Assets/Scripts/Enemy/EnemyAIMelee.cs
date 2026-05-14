using UnityEngine;
using UnityEngine.AI;

public class EnemyAIMelee : MonoBehaviour
{
    public enum EnemyState
    {
        Patrol,
        Pursue,
        Attack
    }

    [Header("References")]
    public Transform player;
    public Transform[] patrolPoints;

    [Header("Sword")]
    public GameObject swordObject;
    public Collider swordHitbox;

    private NavMeshAgent agent;
    private Animator animator;

    [Header("Ranges")]
    public float pursueRange = 10f;
    public float attackRange = 2f;

    [Header("Attack")]
    public float attackCooldown = 1.5f;
    private float lastAttackTime;

    private int currentPatrolIndex = 0;
    private EnemyState currentState;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        currentState = EnemyState.Patrol;

        if (swordObject != null)
        {
            swordObject.SetActive(true);
        }

        if (swordHitbox != null)
        {
            swordHitbox.enabled = true;
        }

        if (patrolPoints.Length > 0 && agent != null && agent.isOnNavMesh)
        {
            agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        }
    }

    void Update()
    {
        if (player == null || agent == null || animator == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        switch (currentState)
        {
            case EnemyState.Patrol:
                if (distanceToPlayer <= attackRange)
                    ChangeState(EnemyState.Attack);
                else if (distanceToPlayer <= pursueRange)
                    ChangeState(EnemyState.Pursue);
                break;

            case EnemyState.Pursue:
                if (distanceToPlayer <= attackRange)
                    ChangeState(EnemyState.Attack);
                else if (distanceToPlayer > pursueRange)
                    ChangeState(EnemyState.Patrol);
                break;

            case EnemyState.Attack:
                if (distanceToPlayer > attackRange && distanceToPlayer <= pursueRange)
                    ChangeState(EnemyState.Pursue);
                else if (distanceToPlayer > pursueRange)
                    ChangeState(EnemyState.Patrol);
                break;
        }

        switch (currentState)
        {
            case EnemyState.Patrol:
                Patrol();
                break;

            case EnemyState.Pursue:
                Pursue();
                break;

            case EnemyState.Attack:
                Attack();
                break;
        }

        UpdateAnimator();
    }

    void ChangeState(EnemyState newState)
    {
        currentState = newState;
    }

    void Patrol()
    {
        if (patrolPoints.Length == 0) return;
        if (!agent.isOnNavMesh) return;

        agent.isStopped = false;

        if (!agent.pathPending && agent.remainingDistance <= 0.5f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        }
    }

    void Pursue()
    {
        if (!agent.isOnNavMesh) return;

        agent.isStopped = false;
        agent.SetDestination(player.position);
    }

    void Attack()
    {
        if (!agent.isOnNavMesh) return;

        agent.isStopped = true;

        Vector3 lookPos = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(lookPos);

        if (Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;

            animator.ResetTrigger("Throw");
            animator.SetTrigger("Throw");
        }
    }

    void UpdateAnimator()
    {
        if (animator.runtimeAnimatorController == null) return;

        float speed = agent.isStopped ? 0f : agent.velocity.magnitude;
        animator.SetFloat("Speed", speed);
    }

    public void SpawnProjectile()
    {
        // Intentionally empty.
        // This exists only because the old throw animation still has a SpawnProjectile event.
        // The sword is always active, so nothing needs to happen here.
    }
}