using UnityEngine;

public class EnemyAttackSoldier : MonoBehaviour
{
    [Header("Ataque")]
    public float attackRange = 5f;
    public float damage = 20f;
    public float attackCooldown = 1f;

    private float attackTimer = 0f;

    void Update()
    {
        attackTimer -= Time.deltaTime;

        SoldierHealth target = FindSoldier();

        if (target != null && attackTimer <= 0f)
        {
            Attack(target);
            attackTimer = attackCooldown;
        }
    }

    SoldierHealth FindSoldier()
    {
        SoldierHealth[] soldiers =
            FindObjectsByType<SoldierHealth>(
                FindObjectsSortMode.None
            );

        SoldierHealth closestSoldier = null;
        float closestDistance = Mathf.Infinity;

        foreach (SoldierHealth soldier in soldiers)
        {
            float distance = Vector3.Distance(
                transform.position,
                soldier.transform.position
            );

            if (distance > attackRange)
                continue;

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestSoldier = soldier;
            }
        }

        return closestSoldier;
    }

    void Attack(SoldierHealth target)
    {
        target.TakeDamage(damage);

        Debug.Log(
            "Inimigo atacou o soldado causando " +
            damage + " de dano!"
        );
    }
}