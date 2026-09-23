using UnityEngine;

public class BaseHealth : MonoBehaviour
{
    [Header("Vida da Base")]
    public float maxHealth = 1000f;

    private float currentHealth;

    void Start()
    {
        // Começa com a vida máxima
        currentHealth = maxHealth;

        Debug.Log("Base criada com " + currentHealth + " de vida.");
    }

    public void TakeDamage(float damage)
    {
        // Diminui a vida
        currentHealth -= damage;

        Debug.Log("A base recebeu " + damage + " de dano. Vida: " + currentHealth);

        // Verifica se a base foi destruída
        if (currentHealth <= 0)
        {
            DestroyBase();
        }
    }

    void DestroyBase()
    {
        Debug.Log("A BASE FOI DESTRUÍDA!");

        // Futuramente vamos colocar a tela de derrota aqui
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }
}