using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{

    public GameObject pauseMenu;
    private GameManager gameManager;
    public bool isPaused = false;
    public float timeScaleSynth = 0;
    public GameObject gameOverPrompt;



    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.FindObjectOfType<GameManager>();

        if (pauseMenu == null)
        {
            pauseMenu = GameObject.Find("/GameUI/PauseMenu");

            if(pauseMenu != null)
            pauseMenu.SetActive(false);
        }
        else
        {
            pauseMenu.SetActive(false);
        }

        GameManager.pauseScript = this;

        ResumeGame();
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(pauseMenu == null)
                pauseMenu = GameObject.Find("/GameUI/PauseMenu");


            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            isPaused = true;
        }
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        
    }


    public void GameOver()
    {
        gameOverPrompt.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        isPaused = true;
        Debug.Log("OH NO THE GAME'S DONE!!");
    }
}
