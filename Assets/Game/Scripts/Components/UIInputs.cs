using UnityEngine;
using UnityEngine.SceneManagement;

public class UIInputs : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}