using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovieBossController : MonoBehaviour
{
    //Variables for fieldOfView
    [Header("Field of View Variables")] 
    [SerializeField] private float viewRadius = 60;
    [SerializeField] private float viewAngle = 45f;
    private bool canSeePlayer = false;
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private LayerMask wallsMask;
    [SerializeField] private Vector3 rotationOffset;


    [Header("Attacking and health")]
    public float health = 100;  
    public float enemySpeed = 10f;
    public float defaultMoveSpeed = 10f;
    public float attackDistance = 5f;
    public Animator anim;
    private bool currentlyAttacking;
    [SerializeField] private float groundSlamTime = 2f;
    public GameObject player;
    public GameManager gameManager;
    [SerializeField] MovieBasicAttack basicAttack;
    [SerializeField] PopcornShooting popcornScript;
    [SerializeField] MovieDeathBall deathBallScript;
    [SerializeField] HealthSystem healthSystem;
    [SerializeField] MovieBigRayAttack bigRayAttack;


    [Header("Movement and gravity")]
    [SerializeField] private NavMeshAgent navController;

    [Header("Health Phases")]
    [SerializeField] List<float> healthPhasesList = new List<float> { 75, 50, 25 };
    [SerializeField] int currentHealthPhase;
    [SerializeField] float popcornAttackTimer;
    [SerializeField] List<string> phaseAnimName = new List<string>() { "Popcorn", "Blast", "Death" };
    [SerializeField] List<float> phaseAnimTimes = new List<float>() {2.75f, 2.75f, 1f};

    [Header("Lights Controller")]
    [SerializeField] MovieLightController lightController;






    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gameManager = GameManager.FindObjectOfType<GameManager>();
        navController = NavMeshAgent.FindObjectOfType<NavMeshAgent>();
        navController.speed = defaultMoveSpeed;
        StartCoroutine(FOVRoutine());


    }

    // Update is called once per frame
    void Update()
    {
        if (canSeePlayer && !currentlyAttacking)
        {
            if(navController.remainingDistance < attackDistance)
            {
                StartCoroutine(Attack());
            }

        }

        if(navController.speed > 0)
        {
            anim.SetBool("Running", true);
        }


        HealthCheck();
    }



    private IEnumerator FOVRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            FieldOfView();
        }
    }

    private void FieldOfView()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        if (rangeChecks.Length != 0)
        {
            for (int i = 0; i < rangeChecks.Length; i++)
            {
                Transform target = rangeChecks[i].transform;
                Vector3 directionToTarget = (target.position - transform.position).normalized;

                if (Vector3.Angle(transform.forward, directionToTarget) < viewAngle / 2)
                {
                    float distanceToTarget = Vector3.Distance(transform.position, target.position);

                    if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, wallsMask))
                    {
                        canSeePlayer = true;
                    }
                    else
                        canSeePlayer = false;
                }
                else
                    canSeePlayer = false;
            }
        }
        else if (canSeePlayer)
        {
            canSeePlayer = false;
        }


    }






    private IEnumerator Attack()
    {
        StopRunning();
        lightController.DefaultAttackColor();
        currentlyAttacking = true;
        anim.SetBool("Attacking", true);
        StartCoroutine(basicAttack.NormalAttack());
        yield return new WaitForSeconds(groundSlamTime);
        anim.SetBool("Attacking", false);
        currentlyAttacking = false;
        StartRunning();
        lightController.BackToDefault();
    }

    //Check the health script to make sure phases are changing as needed
    public void HealthCheck()
    {
        if((healthSystem.health <= healthPhasesList[currentHealthPhase]))
        {
            ChangePhases();
        }
    }


    //Once health is below the point specified in healthPhasesList, kick off the appropriate phase
    public void ChangePhases()
    {
        currentlyAttacking = true;
        currentHealthPhase += 1;
        Debug.Log("We're now in phase: " + currentHealthPhase);

        if(currentHealthPhase == 1) //Popcorn attack
        {
            Debug.Log("This is the popcorn one");
            StartCoroutine(PhaseTimer(phaseAnimTimes[currentHealthPhase -1], phaseAnimName[currentHealthPhase - 1]));
            StartCoroutine(popcornScript.PopcornDestruction());
        }
        else if(currentHealthPhase == 2) //Big boi ray
        {
            Debug.Log("Big death ray");
            StartCoroutine(PhaseTimer(phaseAnimTimes[currentHealthPhase - 1], phaseAnimName[currentHealthPhase - 1]));
            StartCoroutine(bigRayAttack.bigRayAttack());
        }
        else if(currentHealthPhase == 3) //Death
        {
            Debug.Log("Big light sphere");
            StartCoroutine(PhaseTimer(phaseAnimTimes[currentHealthPhase - 1], phaseAnimName[currentHealthPhase - 1]));
            StartCoroutine(deathBallScript.DeathBallStart());
        }
    }


    //Actually starting the next phase. Setting the correct animations and timers
    public IEnumerator PhaseTimer(float time, string currentPhase)
    {
        StopRunning();
        lightController.DefaultAttackColor(3);
        anim.SetBool(currentPhase, true);
        
        
        Debug.Log("Now starting animation for phase: " + currentHealthPhase + " and this is the animation we played: " + currentPhase);
        yield return new WaitForSeconds(time);
        anim.SetBool(currentPhase, false);
        currentlyAttacking = false;
        lightController.BackToDefault(3);
        StartRunning();
    }





    //Stop and start the character from running
    public void StopRunning()
    {
        navController.speed = 0;
        anim.SetBool("Running", false);
    }

    public void StartRunning()
    {
        navController.speed = defaultMoveSpeed;
    }
}
