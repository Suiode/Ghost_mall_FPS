using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostFace : MonoBehaviour
{

    //Variables for fieldOfView
    [Header("Field of view")]
    [SerializeField] private float viewRadius = 60;
    [SerializeField] private float viewAngle = 45f;
    private bool canSeePlayer = false;
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private LayerMask wallsMask;



    [Header("Attack Variables")]
    public float enemySpeed = 10f;
    public float attackDistance = 5f;
    public Animator anim;
    [SerializeField] bool currentlyAttacking = false;
    [SerializeField] float damage = 25f;


    //slowed down time variables
    [Header("Variables for the slow down to work")]
    [SerializeField] private bool timeSlowActive = false;
    [SerializeField] private float normalMoveSpeed;
    [SerializeField] private float normalAnimSpeed;

    public float timeSlowDownLength = 2f;
    public float timeNormalSpeed = 1;
    public float timeSlowMoveScale = 0.5f;
    public float timeSlowAnimScale = 0.5f;

    
    public Color slowedFireColor;
    public Color defaultFireColor;


    [SerializeField] GameObject player;
    [SerializeField] GameManager gameManager;
    [SerializeField] ParticleSystem fireParticles;
    [SerializeField] AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gameManager = GameManager.FindObjectOfType<GameManager>();
        StartCoroutine(FOVRoutine());
        StartCoroutine(IdlePhase());

        normalMoveSpeed = enemySpeed;
        normalAnimSpeed = anim.speed;

        BackToNormalTime();
    }

    // Update is called once per frame
    void Update()
    {
        if (canSeePlayer && !currentlyAttacking )
        {
            if (!GameManager.pauseScript.isPaused)
            {
                MoveTowardsPoint(player.transform.position);
            }


            transform.LookAt(player.transform);
 
            if (Vector3.Distance(transform.position, player.transform.position) < attackDistance)
            {
                StartCoroutine(Attack());
            }
        }
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



    private void MoveTowardsPoint(Vector3 movingToPoint)
    {
        transform.position = transform.position + ((movingToPoint - transform.position).normalized * (Time.deltaTime)) * enemySpeed;
    }


    private IEnumerator Attack()
    {
        currentlyAttacking = true;
        anim.SetBool("Attacking", true);
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("Attacking", false);
        currentlyAttacking = false;
        transform.LookAt(player.transform);
    }


    public void SlowDownTime()
    {
        var main = fireParticles.main;

        anim.speed *= timeSlowAnimScale;
        enemySpeed *= timeSlowMoveScale;


        main.simulationSpeed = 0.75f;
        main.startColor = slowedFireColor;
    }

    public void BackToNormalTime()
    {
        var main = fireParticles.main;
        
        Debug.Log("Ghosties going back to normal time");
        anim.speed = normalAnimSpeed;
        enemySpeed = normalMoveSpeed;

        main.simulationSpeed = 1;
        main.startColor = defaultFireColor;
        
    }

    public IEnumerator IdlePhase()
    {
        while(!canSeePlayer)
        {
            anim.SetBool("Idle", true);
            yield return null;

            if(canSeePlayer)
            {
                anim.SetBool("Idle", false);
                break;
            }
        }
        
    }


    public void OnCollisionEnter(Collision collision)
    {

        //audioSource.Play();

        HealthSystem targetHealth = collision.transform.GetComponentInParent<HealthSystem>();

        if (targetHealth != null && currentlyAttacking)
        {
            targetHealth.TakeDamage(damage);
        }
    }

}
