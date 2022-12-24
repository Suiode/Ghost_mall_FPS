using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    //Slow down variables for ghosts
    [Header("Enemy variables")]
    public bool slowDownActive;
    [SerializeField] private float slowDownLength = 2f;
    [SerializeField] private float slowDownTimer = 2f;
    public MovieLightController movieBossLights;
    [SerializeField] MovieBossController movieBossControl;


    [Header("Player Settings")]
    public float mouseXSens;
    public float mouseYSens;



    public PauseScript pauseScript;

    // Start is called before the first frame update
    void Start()
    {
        pauseScript = GameObject.FindObjectOfType<PauseScript>();

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
            RecountEnemies();
        }



    }

    public void RecountEnemies()
    {
        GhostFace[] ghosts = FindObjectsOfType<GhostFace>();

        if (ghosts.Length == 1)
        {
            if (movieBossLights != null && movieBossControl != null)
            {
                movieBossLights.DefaultAttackColor(10);
                movieBossControl.PlaySound(movieBossControl.awakenSound);
            }
            else
            {
                movieBossLights = FindObjectOfType<MovieLightController>();
                movieBossControl = FindObjectOfType<MovieBossController>();

                RecountEnemies();
            }
        }

    }

    public IEnumerator SlowDownEnemies()
    {
        Debug.Log("We have started the slow down script");

        slowDownTimer = slowDownLength;
        slowDownActive = true;

        GhostFace[] ghosts = FindObjectsOfType<GhostFace>();

        if (ghosts.Length == 1)
        {
            movieBossLights.BackToDefault(3);
        }

        //Tell enemies to slow down
        foreach (GhostFace ghostie in ghosts)
        {
            ghostie.SlowDownTime();
            Debug.Log("Slowing down for ghosties");

        }

        while (slowDownActive)
        {
            //Debug.Log("Made it to the counter");
            yield return null;
            slowDownTimer -= Time.deltaTime;


            //Debug.Log("WaitForSeconds completed successfully");
            if (slowDownTimer <= 0)
            {
                for (int i = 0; i < ghosts.Length; i++)
                {
                    GhostFace enemyScript = ghosts[i].GetComponent<GhostFace>();
                    enemyScript.BackToNormalTime();
                    Debug.Log("Back to normal time");
                    slowDownActive = false;
                    slowDownTimer = 0;
                    RecountEnemies();
                }
            }
        }
    }
}

