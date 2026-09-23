using UnityEngine;
using TMPro;

public class GameHUD : MonoBehaviour
{
    [Header("Textos da HUD")]
    public TMP_Text moneyText;
    public TMP_Text baseHealthText;
    public TMP_Text waveText;
    public TMP_Text soldierCountText;
    public GameObject placementModeText;

    [Header("Sistemas do jogo")]
    public PlayerMoney playerMoney;
    public BaseHealth baseHealth;
    public EnemySpawner enemySpawner;
    public SoldierPlacement soldierPlacement;

    void Update()
    {
        AtualizarDinheiro();
        AtualizarVidaBase();
        AtualizarOnda();
        AtualizarSoldados();
        AtualizarModoColocacao();
    }

    void AtualizarDinheiro()
    {
        if (playerMoney != null)
        {
            moneyText.text =
                "Dinheiro: $" + playerMoney.GetMoney();
        }
    }

    void AtualizarVidaBase()
    {
        if (baseHealth != null)
        {
            baseHealthText.text =
                "Base: " +
                baseHealth.GetCurrentHealth() +
                " HP";
        }
    }

    void AtualizarOnda()
    {
        if (enemySpawner != null)
        {
            waveText.text =
                "Onda: " +
                enemySpawner.GetCurrentWave() +
                " / " +
                enemySpawner.GetMaxWaves();
        }
    }

    void AtualizarSoldados()
    {
        if (soldierPlacement != null)
        {
            soldierCountText.text =
                "Rifleman: " +
                soldierPlacement.GetSoldierCount() +
                " / " +
                soldierPlacement.GetMaxSoldiers();
        }
    }

    void AtualizarModoColocacao()
    {
        if (soldierPlacement != null)
        {
            bool modoAtivo = soldierPlacement.placementMode;

            if (placementModeText != null)
            {
                placementModeText.SetActive(modoAtivo);
            }

            if (soldierCountText != null)
            {
                soldierCountText.gameObject.SetActive(modoAtivo);
            }
        }
    }
}