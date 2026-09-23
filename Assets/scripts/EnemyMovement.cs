using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    // Componente responsável pelo movimento
    private NavMeshAgent agent;

    // Pontos que o inimigo deve seguir
    public Transform[] waypoints;

    // Vida que o inimigo tira da base
    public float baseDamage = 10f;

    // Qual waypoint o inimigo está seguindo
    private int waypointAtual = 0;

    // Permite que outros scripts saibam o progresso do inimigo
    public int GetWaypointAtual()
    {
        return waypointAtual;
    }

    // Retorna um valor que representa o progresso total no caminho
    public float GetProgressoNoCaminho()
    {
        if (waypoints == null || waypoints.Length == 0)
            return 0f;

        // Se chegou ao final
        if (waypointAtual >= waypoints.Length)
            return waypoints.Length;

        // Quanto mais perto do próximo waypoint, maior o progresso
        float distanciaTotal = 0f;

        if (waypointAtual > 0)
        {
            for (int i = 1; i <= waypointAtual; i++)
            {
                distanciaTotal += Vector3.Distance(
                    waypoints[i - 1].position,
                    waypoints[i].position
                );
            }
        }

        float distanciaAteWaypoint = Vector3.Distance(
            transform.position,
            waypoints[waypointAtual].position
        );

        float distanciaDoWaypointAnterior = 0f;

        if (waypointAtual > 0)
        {
            distanciaDoWaypointAnterior = Vector3.Distance(
                waypoints[waypointAtual - 1].position,
                waypoints[waypointAtual].position
            );
        }

        float progressoAtual = 0f;

        if (distanciaDoWaypointAnterior > 0f)
        {
            progressoAtual =
                1f - (distanciaAteWaypoint / distanciaDoWaypointAnterior);
        }

        return distanciaTotal + progressoAtual;
    }

    void Start()
    {
        // Pegamos o NavMesh Agent do inimigo
        agent = GetComponent<NavMeshAgent>();

        // Se existirem pontos, começamos pelo primeiro
        if (waypoints.Length > 0)
        {
            agent.SetDestination(waypoints[waypointAtual].position);
        }
    }

    void Update()
    {
        // Se não existem pontos, não fazemos nada
        if (waypoints.Length == 0)
            return;

        // Verifica se chegou ao waypoint atual
        if (!agent.pathPending &&
            agent.remainingDistance <= agent.stoppingDistance)
        {
            // Passa para o próximo waypoint
            waypointAtual++;

            // Se ainda existem pontos, continua o caminho
            if (waypointAtual < waypoints.Length)
            {
                agent.SetDestination(waypoints[waypointAtual].position);
            }
            else
            {
                // Chegou ao final do caminho
                ChegouNaBase();
            }
        }
    }

    void ChegouNaBase()
    {
        Debug.Log("Inimigo chegou à base!");

        // Procuramos a base na cena
        BaseHealth baseHealth = FindFirstObjectByType<BaseHealth>();

        // Se encontramos a base, causamos dano
        if (baseHealth != null)
        {
            baseHealth.TakeDamage(baseDamage);
        }

        // O inimigo desaparece depois de atacar
        Destroy(gameObject);
    }
}