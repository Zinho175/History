using UnityEngine;
using UnityEngine.SceneManagement;

public class DefeatUI : MonoBehaviour
{
    [Header("Painel de Derrota")]
    public GameObject defeatPanel;

    void Start()
    {
        // Começa escondido
        defeatPanel.SetActive(false);
    }

    public void ShowDefeat()
    {
        // Mostra a tela de derrota
        defeatPanel.SetActive(true);

        // Libera o cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Debug.Log("💀 TELA DE DERROTA EXIBIDA!");
    }

    public void VoltarAoMenu()
    {
        SceneManager.LoadScene("LoginTela");
    }
}