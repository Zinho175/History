using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movimento")]
    public float velocidade = 5f;
    public float velocidadeCorrendo = 9f;

    [Header("Pulo")]
    public float forcaPulo = 8f;
    public float gravidade = -20f;

    [Header("Dash")]
    public float dashDistance = 8f;
    public float dashDuration = 0.15f;
    public float dashCooldown = 1f;

    public Transform cameraTransform;

    private CharacterController controller;
    private float velocidadeY;

    private bool dashing = false;
    private float dashTimer = 0f;
    private float dashCooldownTimer = 0f;

    // Guarda a última direção em que o jogador estava andando
    private Vector3 ultimaDirecaoMovimento = Vector3.forward;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        dashCooldownTimer -= Time.deltaTime;

        // =========================
        // INPUT DE MOVIMENTO
        // =========================

        Vector2 input = Vector2.zero;

        if (Keyboard.current.wKey.isPressed)
            input.y = 1;

        if (Keyboard.current.sKey.isPressed)
            input.y = -1;

        if (Keyboard.current.aKey.isPressed)
            input.x = -1;

        if (Keyboard.current.dKey.isPressed)
            input.x = 1;

        Vector3 frente = cameraTransform.forward;
        Vector3 direita = cameraTransform.right;

        frente.y = 0;
        direita.y = 0;

        frente.Normalize();
        direita.Normalize();

        Vector3 movimento =
            frente * input.y +
            direita * input.x;

        if (movimento.magnitude > 1)
            movimento.Normalize();

        // Guarda a direção atual do movimento
        if (movimento.magnitude > 0.1f)
        {
            ultimaDirecaoMovimento = movimento.normalized;
        }

        // =========================
        // DASH
        // =========================

        if (Keyboard.current.qKey.wasPressedThisFrame &&
            !dashing &&
            dashCooldownTimer <= 0f)
        {
            StartDash();
        }

        if (dashing)
        {
            dashTimer -= Time.deltaTime;

            controller.Move(
                ultimaDirecaoMovimento *
                (dashDistance / dashDuration) *
                Time.deltaTime
            );

            if (dashTimer <= 0f)
            {
                dashing = false;
            }

            return;
        }

        // =========================
        // VELOCIDADE
        // =========================

        float velocidadeAtual = velocidade;

        if (Keyboard.current.leftShiftKey.isPressed ||
            Keyboard.current.rightShiftKey.isPressed)
        {
            velocidadeAtual = velocidadeCorrendo;
        }

        // =========================
        // PULO
        // =========================

        if (controller.isGrounded)
        {
            velocidadeY = -2f;

            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                velocidadeY = forcaPulo;
            }
        }
        else
        {
            velocidadeY += gravidade * Time.deltaTime;
        }

        Vector3 movimentoFinal =
            movimento * velocidadeAtual;

        movimentoFinal.y = velocidadeY;

        controller.Move(
            movimentoFinal * Time.deltaTime
        );
    }

    void StartDash()
    {
        dashing = true;

        dashTimer = dashDuration;
        dashCooldownTimer = dashCooldown;

        Debug.Log(
            "DASH na direção do movimento!"
        );
    }
}