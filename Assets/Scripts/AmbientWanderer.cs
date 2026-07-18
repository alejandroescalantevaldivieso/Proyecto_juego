using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AmbientWanderer : MonoBehaviour
{
    public float wanderRadius = 20f;
    public float wanderTimer = 5f;

    private NavMeshAgent agent;
    private float timer;
    private Animator animator;

    void Start() {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        timer = wanderTimer;
        
        // Configurar NavMeshAgent para caminar
        agent.speed = 1.5f;
    }

    void Update() {
        timer += Time.deltaTime;

        if (timer >= wanderTimer) {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            timer = 0;
        }

        // Si tenemos animador, le pasamos la velocidad
        if (animator != null) {
            bool hasParam = false;
            foreach (var param in animator.parameters) {
                if (param.name == "Speed") { hasParam = true; break; }
            }
            if (hasParam) {
                animator.SetFloat("Speed", agent.velocity.magnitude);
            }
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask) {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;
        NavMeshHit navHit;
        if (NavMesh.SamplePosition(randDirection, out navHit, dist, layermask)) {
            return navHit.position;
        }
        return origin;
    }
}