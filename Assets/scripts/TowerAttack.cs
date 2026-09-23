using UnityEngine;

public class TowerAttack : MonoBehaviour
{
    [Header("Configuração da Torre")]
    public float attackRange = 15f;
    public float damage = 25f;
    public float attackCooldown = 1f;

    [Header("Mira")]
    public float rotationSpeed = 5f;

    private float attackTimer = 0f;

    void Update()
    {
        attackTimer -= Time.deltaTime;

        EnemyHealth target = FindFirstEnemy();

        if (target != null)
        {
            AimAtTarget(target);

            if (attackTimer <= 0f)
            {
                Attack(target);
                attackTimer = attackCooldown;
            }
        }
    }

    EnemyHealth FindFirstEnemy()
    {
        EnemyHealth[] enemies = FindObjectsByType<EnemyHealth>(
            FindObjectsSortMode.None
        );

        EnemyHealth firstEnemy = null;

        float maiorProgresso = -1f;

        foreach (EnemyHealth enemy in enemies)
        {
            float distance = Vector3.Distance(
                transform.position,
                enemy.transform.position
            );

            // Ignora inimigos fora do alcance
            if (distance > attackRange)
                continue;

            EnemyMovement movement = enemy.GetComponent<EnemyMovement>();

            if (movement == null)
                continue;

            // Descobre exatamente o quanto o inimigo avançou no caminho
            float progresso = movement.GetProgressoNoCaminho();

            // Escolhe o inimigo mais avançado
            if (progresso > maiorProgresso)
            {
                maiorProgresso = progresso;
                firstEnemy = enemy;
            }
        }

        return firstEnemy;
    }

    void AimAtTarget(EnemyHealth target)
    {
        Vector3 direction =
            target.transform.position - transform.position;

        direction.y = 0f;

        if (direction == Vector3.zero)
            return;

        Quaternion targetRotation =
            Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
    }

    void Attack(EnemyHealth target)
    {
        target.TakeDamage(damage);

        Debug.Log(
            "Torre atacou " + target.name +
            " causando " + damage + " de dano!"
        );
    }
}