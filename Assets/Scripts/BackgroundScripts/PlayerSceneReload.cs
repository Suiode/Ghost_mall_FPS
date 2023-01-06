using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSceneReload : MonoBehaviour
{
    //We'll have this script on the player so whenever a new scene loads, it will run through the following maintneance classes
    [SerializeField] GameManager gameManager;
    [SerializeField] MovieBossController movieBossControl;
    [SerializeField] MovieLightController movieLightController;
    [SerializeField] MoonScript moonController;




    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();



        //if (!gameManager.movieBossControl)
        //    gameManager.movieBossControl = movieBossControl;

        //if (!gameManager.movieBossLights)
        //    gameManager.movieBossLights = movieLightController;

        //if (!GameManager.moonController)
        //    GameManager.moonController = moonController;

    }




    //Scene management, make sure that all necessary
    void OnEnable()
    {
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
        Debug.Log("The scene started loading: " + gameManager.pauseMenu);
    }

    void OnDisable()
    {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
        Debug.Log("The scene finished loading: " + gameManager.pauseMenu);

    }


    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        gameManager = FindObjectOfType<GameManager>();


        Debug.Log("Level Loaded");
        Debug.Log(scene.name);
        Debug.Log(mode);


        //Screen options reload


        //if (gameManager != null)
        //    StopCoroutine(gameManager.SlowDownEnemies());



        GameManager.pauseScript = GameObject.FindObjectOfType<PauseScript>();

 




        //if (!gameManager.movieBossControl)
        //    gameManager.movieBossControl = movieBossControl;

        //if (!gameManager.movieBossLights)
        //    gameManager.movieBossLights = movieLightController;

        //if (!GameManager.moonController)
        //    GameManager.moonController = moonController;

    }
}
