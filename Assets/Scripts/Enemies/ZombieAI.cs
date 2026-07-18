using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
public class ZombieAI : MonoBehaviour
{
    public Transform target;
    public float attackRange = 1.8f;
    public float chaseRange = 25f; // Increased so they chase sooner
    public float lookRange = 40f; 
    public float moveSpeed = 3.5f;
    
    private NavMeshAgent agent;
    private Animator animator;
    private float lastAttackTime = 0f;
    public float attackCooldown = 1.5f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        
        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null) target = player.transform;
        }
    }

    void Update()
    {
        if (target == null) return;

        float distanceToTarget = Vector3.Distance(transform.position, target.position);
        bool useNavMesh = agent != null && agent.isOnNavMesh;

        if (distanceToTarget <= lookRange)
        {
            if (distanceToTarget <= chaseRange)
            {
                if (distanceToTarget <= attackRange)
                {
                    // ATACAR
                    if (useNavMesh) agent.isStopped = true;
                    
                    if (Time.time - lastAttackTime >= attackCooldown)
                    {
                        lastAttackTime = Time.time;
                        animator.Play("Attack", 0, 0f); 
                        animator.SetFloat("Speed", 0f);
                        target.SendMessage("TakeDamage", 10, SendMessageOptions.DontRequireReceiver);
                    }
                    
                    Vector3 direction = (target.position - transform.position).normalized;
                    if (direction != Vector3.zero)
                    {
                        direction.y = 0;
                        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 10f);
                    }
                }
                else
                {
                    // PERSEGUIR (CORRER HACIA EL JUGADOR)
                    if (useNavMesh) {
                        agent.isStopped = false;
                        agent.SetDestination(target.position);
                        animator.SetFloat("Speed", agent.velocity.magnitude);
                    } else {
                        // FALLBACK SI NO HAY NAVMESH
                        Vector3 direction = (target.position - transform.position).normalized;
                        direction.y = 0;
                        transform.position += direction * moveSpeed * Time.deltaTime;
                        if (direction != Vector3.zero) {
                            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 8f);
                        }
                        animator.SetFloat("Speed", moveSpeed);
                    }
                }
            }
            else
            {
                // MIRAR AL JUGADOR
                if (useNavMesh) agent.isStopped = true;
                animator.SetFloat("Speed", 0f);
                
                Vector3 direction = (target.position - transform.position).normalized;
                if (direction != Vector3.zero)
                {
                    direction.y = 0;
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 3f);
                }
            }
        }
        else
        {
            // IDLE COMPLETO
            if (useNavMesh) agent.isStopped = true;
            animator.SetFloat("Speed", 0f);
        }
    }
}