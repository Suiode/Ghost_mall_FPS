using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
    //[SerializeField] float[] healthPhases = {75, 50, 25};    
    [SerializeField] List<float> healthPhasesList = new List<float>{75, 50, 25};
    public GameObject player;
    public GameManager gameManager;
    public float enemySpeed = 10f;
    public float defaultMoveSpeed = 10f;
    public float attackDistance = 5f;
    public Animator anim;
    private bool currentlyAttacking;
    [SerializeField] private float groundSlamTime = 2f;


    [Header("Movement and gravity")]
    private Vector3 velocity;
    [SerializeField] private float gravity = -20f;
    [SerializeField] private bool isGrounded = false;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.1f;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private MovieNavigation navController;






    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gameManager = GameManager.FindObjectOfType<GameManager>();
        StartCoroutine(FOVRoutine());


    }

    // Update is called once per frame
    void Update()
    {
        if (canSeePlayer && !currentlyAttacking)
        {
            navController.canWeMove = true;
            anim.SetBool("Running", true);

            if (Vector3.Distance(transform.position, player.transform.position) < attackDistance)
            {
                StartCoroutine(Attack());
            }

        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        /*if (velocity.y < gravity && !isGrounded)
        {
            
            Gravity();
        }*/
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
        currentlyAttacking = true;
        anim.SetBool("Attacking", true);
        anim.SetBool("Running", false);
        navController.canWeMove = false;
        yield return new WaitForSeconds(groundSlamTime);
        anim.SetBool("Attacking", false);
        currentlyAttacking = false;
        navController.canWeMove = true;
    }






}
