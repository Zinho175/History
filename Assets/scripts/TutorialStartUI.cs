using UnityEngine;

public class TutorialStartUI : MonoBehaviour
{
    [Header("Painel")]
    public GameObject tutorialPanel;

    [Header("Spawner")]
    public EnemySpawner enemySpawner;

    [Header("Jogador")]
    public PlayerMovement playerMovement;

    void Start()
    {
        // Garante que o mouse fique livre no menu
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Impede o jogador de andar enquanto o menu está aberto
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }
    }

    public void StartTutorial()
    {
        // Esconde a tela de introdução
        tutorialPanel.SetActive(false);

        // Libera o movimento do jogador
        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }

        // Mantém o mouse livre
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Inicia as ondas
        if (enemySpawner != null)
        {
            enemySpawner.StartWaves();
        }

        Debug.Log("===== TUTORIAL INICIADO! =====");
    }
}