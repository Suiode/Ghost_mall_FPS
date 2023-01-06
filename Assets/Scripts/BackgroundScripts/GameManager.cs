using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    //Slow down variables for ghosts
    [Header("Enemy variables")]
    public bool slowDownActive;
    [SerializeField] private float slowDownLength = 2f;
    [SerializeField] private float slowDownTimer = 2f;
    public MovieLightController movieBossLights;
    public MovieBossController movieBossControl;
    public static MoonScript moonController;
    public GhostFace[] ghosts;


    [Header("Player Settings")]
    public static float mouseXSens = 100;
    public static float mouseYSens = 100;


    public static PauseScript pauseScript;
    public GameObject pauseMenu;


    [Header("Screen settings")]
    public Video_options videoOptions;
    public int currentResolution;
    public static bool FPSCounterEnabled;
    public static int framerateTarget;



    // Start is called before the first frame update
    void Start()
    {
        pauseScript = GameObject.FindObjectOfType<PauseScript>();
        moonController = FindObjectOfType<MoonScript>();

        if (movieBossControl == null)
        {
            movieBossLights = FindObjectOfType<MovieLightController>();
            movieBossControl = FindObjectOfType<MovieBossController>();
        }

        //Makes sure the GameManager is kept between scenes. If there isn't one, then create it
        DontDestroyOnLoad(gameObject);

        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
        {
            instance = this;
        }
    }

    public void StartSlowDown()
    {
        if (!slowDownActive)
        {
            StartCoroutine(SlowDownEnemies());
        }
        else
        {
            slowDownTimer = slowDownLength;
        }

    }

    public void EnableBoss()
    {
            if (movieBossLights != null && movieBossControl != null)
            {
                moonController.ShiftColorBloodDefault(2f);
                movieBossLights.BackToDefault();
                movieBossControl.PlaySound(movieBossControl.awakenSound);
                movieBossControl.isIdle = false;
            }
            else
            {
                movieBossLights = FindObjectOfType<MovieLightController>();
                movieBossControl = FindObjectOfType<MovieBossController>();

                if (movieBossLights != null && movieBossControl != null)
                {
                    EnableBoss();
                }
            }

    }

    public IEnumerator SlowDownEnemies()
    {
        slowDownTimer = slowDownLength;
        slowDownActive = true;


        ghosts = FindObjectsOfType<GhostFace>();




        //Tell enemies to slow down
        foreach (GhostFace ghostie in ghosts)
        {
            ghostie.SlowDownTime();
        }


        while (slowDownActive)
        {
            yield return null;
            slowDownTimer -= Time.deltaTime;

            ghosts = FindObjectsOfType<GhostFace>();

            if (ghosts.Length == 0 && movieBossControl.isIdle)
            {
                EnableBoss();
            }


            if (slowDownTimer <= 0)
            {
                for (int i = 0; i < ghosts.Length; i++)
                {
                    GhostFace enemyScript = ghosts[i].GetComponent<GhostFace>();

                    if (ghosts == null)
                        Debug.Log("Number of ghosts left: " + 0);

                    enemyScript.BackToNormalTime();
                    slowDownActive = false;
                    slowDownTimer = 0;
                }
            }


        }
    }
}

