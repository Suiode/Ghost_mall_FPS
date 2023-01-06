using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{


    public float health = 100f;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float dashSpeed = 10f;
    [SerializeField] private float dashMaxTime = 0.5f;
    [SerializeField] private bool dashing = false;


    [Header("Jumping and gravity")]
    public float currentGravity = -20f;
    [SerializeField] private float defaultGravity = -20f;
    [SerializeField] private float jumpHeight = 10f;
    [SerializeField] private float jumpSpeed = 0.25f;
    [SerializeField] private float jumpMaxTimer = 1f;
    [SerializeField] private float groundDistance = 0.3f;
    private Vector3 velocity;
    [SerializeField] private bool isGrounded;


    [Header("Health")]
    [SerializeField] private float maxHealth;
    [SerializeField] PlayerDamagedScript playerDamage;


    [Header("Game Objects")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Animator anim;
    [SerializeField] private Camera playerCam;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private CharacterController controller;
    [SerializeField] PauseScript pauseScript;

    [Header("Misc")]
    public bool godMode = false;



    private void Start()
    {
        pauseScript = FindObjectOfType<PauseScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!pauseScript.isPaused)
        {
            MovementSystem();
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);


            ////Dash
            //if ((Input.GetKeyDown(KeyCode.LeftShift) && !dashing))
            //{
            //    StartCoroutine(Dash(moveSpeed));
            //}
        }

    }





    public void MovementSystem()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");



        //Apply gravity, and only until a top speed set by currentGravity
        velocity.y += currentGravity;

        if (velocity.y < currentGravity)
        {
            velocity.y = currentGravity;
        }


        Vector3 move = (transform.right * x) * moveSpeed + (transform.forward * z) * moveSpeed + (velocity);

        controller.Move(move * Time.deltaTime);

    }


    public void JumpEvent(InputAction.CallbackContext context)
    {


        if (isGrounded && context.performed)
        {

            StartCoroutine(Jump());
        }
    }


    public IEnumerator Jump()
    {
        float timer = 0;
        moveSpeed *= 2f;


        while (timer <= jumpMaxTimer)
        {
            yield return null;
            timer += Time.deltaTime;

            //Jumping goes from current y velocity to the jump height set.
            velocity.y = Mathf.Lerp(velocity.y, Mathf.Abs(currentGravity) * jumpHeight, jumpSpeed - timer);

            //Make sure the jump isn't ended just because we're still touching the ground
            if (isGrounded && timer > 0.5f)
            {
                break;
            }
        }


        moveSpeed = walkSpeed;
    }




    public void TakeDamage(float damageTaken)
    {
        health -= damageTaken;



        if (health <= 0)
        {
            Die();
        }
        else
        {
            playerDamage.PlayerDamageVisualUpdate(health);
        }
    }


    void Die()
    {
        pauseScript.GameOver();
    }


    private IEnumerator Dash(float curMoveSpeed)
    {
        moveSpeed = curMoveSpeed * dashSpeed;
        float dashTimer = 0;
        dashing = true;

        yield return null;
        

        while (dashing)
        {
            dashTimer += Time.deltaTime;


            if (dashTimer > dashMaxTime)
            {
                dashing = false;
                moveSpeed = curMoveSpeed;
            }

        }

    }

}
