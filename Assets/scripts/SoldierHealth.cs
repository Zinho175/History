using UnityEngine;

public class SoldierHealth : MonoBehaviour
{
    [Header("Vida")]
    public float maxHealth = 100f;

    [Header("Custo do Soldado")]
    public int soldierCost = 400;

    [Header("Proteção da Trincheira")]
    public bool insideTrench = false;

    [Tooltip("0.25 = recebe apenas 25% do dano dentro da trincheira")]
    public float trenchDamageMultiplier = 0.25f;

    [Header("Visual da Proteção")]
    public Color protectedColor = new Color(0.3f, 1f, 0.3f);

    private float currentHealth;

    private Renderer soldierRenderer;
    private Color originalColor;

    void Start()
    {
        currentHealth = maxHealth;

        // Procura o Renderer do corpo do soldado.
        soldierRenderer = GetComponent<Renderer>();

        if (soldierRenderer != null)
        {
            originalColor = soldierRenderer.material.color;
        }

        UpdateVisual();

        Debug.Log(
            "Soldado criado! Vida: " + currentHealth +
            " | Dentro da trincheira: " + insideTrench
        );
    }

    public void SetInsideTrench(bool value)
    {
        insideTrench = value;

        UpdateVisual();

        if (insideTrench)
        {
            Debug.Log(
                "Soldado está PROTEGIDO pela trincheira."
            );
        }
        else
        {
            Debug.Log(
                "Soldado está EXPOSTO fora da trincheira."
            );
        }
    }

    void UpdateVisual()
    {
        if (soldierRenderer == null)
            return;

        if (insideTrench)
        {
            soldierRenderer.material.color = protectedColor;
        }
        else
        {
            soldierRenderer.material.color = originalColor;
        }
    }

    public void TakeDamage(float damage)
    {
        float danoOriginal = damage;

        if (insideTrench)
        {
            damage *= trenchDamageMultiplier;
        }

        currentHealth -= damage;

        Debug.Log(
            "Soldado recebeu " + damage +
            " de dano. " +
            "Dano original: " + danoOriginal +
            " | Protegido: " + insideTrench +
            " | Vida: " + currentHealth
        );

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(
            "SOLDADO MORREU! " +
            "Investimento perdido: $" + soldierCost
        );

        Destroy(gameObject);
    }
}