using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Vida")]
    public float maxHealth = 100f;

    [Header("Recompensa")]
    public int moneyReward = 10;

    private float currentHealth;

    void Start()
    {
        // Começa com a vida máxima
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        // Diminui a vida
        currentHealth -= damage;

        Debug.Log("Inimigo recebeu " + damage + " de dano. Vida: " + currentHealth);

        // Verifica se morreu
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Inimigo derrotado!");

        // Procura o sistema de dinheiro
        PlayerMoney playerMoney = FindFirstObjectByType<PlayerMoney>();

        // Se encontrou, entrega a recompensa
        if (playerMoney != null)
        {
            playerMoney.AddMoney(moneyReward);
        }

        // Destrói o inimigo
        Destroy(gameObject);
    }
}