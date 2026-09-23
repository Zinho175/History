using UnityEngine;
using UnityEngine.InputSystem;

public class CameraLook : MonoBehaviour
{
    [Header("Configurações")]
    public float sensibilidade = 0.15f;

    [Header("Limites da câmera")]
    public float limiteCima = 80f;
    public float limiteBaixo = -80f;

    [Header("Jogador")]
    public Transform jogador;

    private float rotacaoX = 0f;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Mouse.current == null)
            return;

        Vector2 movimentoMouse = Mouse.current.delta.ReadValue();

        float mouseX = movimentoMouse.x * sensibilidade;
        float mouseY = movimentoMouse.y * sensibilidade;

        // CIMA / BAIXO
        rotacaoX -= mouseY;
        rotacaoX = Mathf.Clamp(rotacaoX, limiteBaixo, limiteCima);

        transform.localRotation = Quaternion.Euler(rotacaoX, 0f, 0f);

        // ESQUERDA / DIREITA
        if (jogador != null)
        {
            jogador.Rotate(0f, mouseX, 0f);
        }
    }
}