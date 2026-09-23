using UnityEngine;

public class PlayerMoney : MonoBehaviour
{
    [Header("Dinheiro inicial")]
    public int startingMoney = 500;

    // Dinheiro atual do jogador
    private int currentMoney;

    void Start()
    {
        // Começa com o dinheiro definido no Inspector
        currentMoney = startingMoney;

        Debug.Log("Dinheiro inicial: $" + currentMoney);
    }

    public void AddMoney(int amount)
    {
        // Adiciona dinheiro
        currentMoney += amount;

        Debug.Log("Dinheiro recebido: $" + amount + " | Total: $" + currentMoney);
    }

    public bool SpendMoney(int amount)
    {
        // Verifica se o jogador possui dinheiro suficiente
        if (currentMoney < amount)
        {
            Debug.Log("Dinheiro insuficiente!");

            return false;
        }

        // Retira o dinheiro
        currentMoney -= amount;

        Debug.Log("Dinheiro gasto: $" + amount + " | Total: $" + currentMoney);

        return true;
    }

    public int GetMoney()
    {
        return currentMoney;
    }
}