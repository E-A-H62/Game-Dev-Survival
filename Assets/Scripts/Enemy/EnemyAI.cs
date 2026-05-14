using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
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
    public Transform throwPoint;
    public GameObject projectilePrefab;

    private NavMeshAgent agent;
    private Animator animator;

    [Header("Ranges")]
    public float pursueRange = 10f;
    public float attackRange = 5f;

    [Header("Attack")]
    public float attackCooldown = 1.5f;
    public float projectileSpeed = 12f;
    private float lastAttackTime;

    private int currentPatrolIndex = 0;
    private EnemyState currentState;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        currentState = EnemyState.Patrol;

        if (patrolPoints.Length > 0)
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

    // Called by Animation Event during the throw animation
    public void SpawnProjectile()
    {
        if (projectilePrefab == null || throwPoint == null || player == null) return;

        GameObject projectile = Instantiate(projectilePrefab, throwPoint.position, Quaternion.identity);

        Vector3 direction = (player.position - throwPoint.position).normalized;

        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = direction * projectileSpeed;
        }
    }
}