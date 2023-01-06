using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainMenuScript : MonoBehaviour
{
    public void LoadMainMenu()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();

        GameManager.pauseScript.ResumeGame();

        
        
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
