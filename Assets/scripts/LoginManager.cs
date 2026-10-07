using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{
    public void Entrar()
    {
        SceneManager.LoadScene("SampleScene");
    }
}