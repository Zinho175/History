using UnityEngine;
using UnityEngine.InputSystem;

public class SoldierPlacement : MonoBehaviour
{
    [Header("Soldado")]
    public GameObject soldierPrefab;

    [Header("Configuração")]
    public int soldierCost = 400;
    public int maxSoldiers = 6;

    [Header("Área de colocação")]
    public LayerMask placementLayer;

    [Header("Câmera")]
    public Camera playerCamera;

    [Header("Altura do Soldado")]
    public float placementHeight = 1f;

    [Header("Estado")]
    public bool placementMode = false;

    void Update()
    {
        // Apertar 1 ativa/desativa o modo de colocação
        if (Keyboard.current != null &&
            Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            placementMode = !placementMode;

            if (placementMode)
            {
                Debug.Log("MODO DE COLOCAÇÃO: ATIVADO");
            }
            else
            {
                Debug.Log("MODO DE COLOCAÇÃO: DESATIVADO");
            }
        }

        // Só pode colocar soldados se o modo estiver ativado
        if (placementMode &&
            Mouse.current != null &&
            Mouse.current.leftButton.wasPressedThisFrame)
        {
            TryPlaceSoldier();
        }
    }

    void TryPlaceSoldier()
    {
        // Verifica limite de soldados
        SoldierHealth[] soldiers =
            FindObjectsByType<SoldierHealth>(
                FindObjectsSortMode.None
            );

        if (soldiers.Length >= maxSoldiers)
        {
            Debug.Log(
                "LIMITE DE SOLDADOS ATINGIDO! Máximo: " +
                maxSoldiers
            );

            return;
        }

        // Cria o raio a partir da câmera
        Ray ray = playerCamera.ScreenPointToRay(
            Mouse.current.position.ReadValue()
        );

        if (!Physics.Raycast(
            ray,
            out RaycastHit hit,
            1000f))
        {
            return;
        }

        // Verifica se o local pode receber soldados
        int hitLayer = 1 << hit.collider.gameObject.layer;

        if ((placementLayer.value & hitLayer) == 0)
        {
            Debug.Log(
                "Não é possível colocar o soldado aqui!"
            );

            return;
        }

        // Procura o sistema de dinheiro
        PlayerMoney playerMoney =
            FindFirstObjectByType<PlayerMoney>();

        if (playerMoney == null)
        {
            Debug.LogWarning(
                "PlayerMoney não foi encontrado na cena!"
            );

            return;
        }

        // Verifica dinheiro
        if (!playerMoney.SpendMoney(soldierCost))
        {
            Debug.Log(
                "Dinheiro insuficiente para colocar o soldado."
            );

            return;
        }

        // Corrige a altura do soldado
        Vector3 spawnPosition =
            hit.point + Vector3.up * placementHeight;

        // Cria o soldado
        Instantiate(
            soldierPrefab,
            spawnPosition,
            Quaternion.identity
        );

        Debug.Log(
            "Soldado colocado! Custo: $" +
            soldierCost +
            " | Soldados: " +
            (soldiers.Length + 1) +
            "/" +
            maxSoldiers
        );
    }

    public int GetSoldierCount()
    {
        SoldierHealth[] soldiers =
            FindObjectsByType<SoldierHealth>(
                FindObjectsSortMode.None
            );

        return soldiers.Length;
    }

    public int GetMaxSoldiers()
    {
        return maxSoldiers;
    }
}