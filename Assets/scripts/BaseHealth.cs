using UnityEngine;

public class BaseHealth : MonoBehaviour
{
    [Header("Vida da Base")]
    public float maxHealth = 1000f;

    [Header("Tela de Derrota")]
    public DefeatUI defeatUI;

    private float currentHealth;
    private bool baseDestruida = false;

    void Start()
    {
        currentHealth = maxHealth;

        Debug.Log("Base criada com " + currentHealth + " de vida.");
    }

    public void TakeDamage(float damage)
    {
        if (baseDestruida)
            return;

        currentHealth -= damage;

        currentHealth = Mathf.Max(currentHealth, 0);

        Debug.Log(
            "A base recebeu " + damage +
            " de dano. Vida: " + currentHealth
        );

        if (currentHealth <= 0)
        {
            DestroyBase();
        }
    }

    void DestroyBase()
    {
        if (baseDestruida)
            return;

        baseDestruida = true;

        Debug.Log("💀 A BASE FOI DESTRUÍDA!");

        // Para o spawn e elimina os inimigos
        EnemySpawner spawner =
            FindFirstObjectByType<EnemySpawner>();

        if (spawner != null)
        {
            spawner.StopSpawningAndKillEnemies();
        }
        else
        {
            Debug.LogWarning(
                "⚠️ EnemySpawner não encontrado na cena!"
            );
        }

        // Mostra a tela de derrota
        if (defeatUI != null)
        {
            defeatUI.ShowDefeat();
        }
        else
        {
            Debug.LogWarning(
                "⚠️ DefeatUI não foi configurado no BaseHealth!"
            );
        }
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }
}