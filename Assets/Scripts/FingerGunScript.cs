using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Rendering;


public class FingerGunScript : MonoBehaviour
{

    [Header("Damage Calculations")]
    [SerializeField] private int damage = 40;
    [SerializeField] private float limbMultiplier = 1;
    [SerializeField] private float headshotMultiplier = 2;
    [SerializeField] private float range;
    public float rateOfFire = 0.25f;



    //Magazine stats
    [Header("Magazine stats")]
    [SerializeField] private int bulletsLoaded = 0;
    [SerializeField] private float reloadSpeed = 1f;


    [Header("Projectile info")]
    [SerializeField] private int bulletsPerTriggerPull = 1;
    [SerializeField] private bool readyForNextShot = true;
    [SerializeField] private float bulletTravelTime = 0.1f;
    [SerializeField] private float laserOffset = 0.25f;
    [SerializeField] private float distanceSpeed = 50f;



    //Other Components
    [Header("Components")]
    [SerializeField] private Camera fpsCam;
    [SerializeField] private Animator anim;
    [SerializeField] private LineRenderer bulletLineRenderer;
    [SerializeField] private GameObject muzzle;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private LayerMask shootableLayers;
    [SerializeField] private GameObject bulletHole;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip shootingSound;
    [SerializeField] AudioClip headshotSound;
    [SerializeField] PauseScript pauseScript;


    private void Start()
    {
        pauseScript = FindObjectOfType<PauseScript>();
        audioSource = GetComponentInParent<AudioSource>();
    }


    void Update()
    {

        if (Input.GetMouseButton(0) && readyForNextShot)
        {

            for (int i = 0; i < (bulletsPerTriggerPull); i++)
            {
                if(!pauseScript.isPaused)
                Shoot();

            }
        }

    }




    public void Shoot()
    {
        bulletLineRenderer.enabled = true;
        Vector3 directionRay = fpsCam.transform.TransformDirection(0, 0, 1);
        RaycastHit hit;




        if (Physics.Raycast(fpsCam.transform.position, directionRay, out hit, range, shootableLayers))
        {
            HealthSystem target = hit.transform.GetComponentInParent<HealthSystem>();
            //MovieBossController target = hit.transform.GetComponent<MovieBossController>();


            //Check if we should increase or reduce damage based on where we hit
            if (target != null)
            {
                if (hit.collider.name == "Limbs")
                {
                    target.TakeDamage(damage * limbMultiplier);
                    audioSource.clip = shootingSound;
                }
                else if (hit.collider.name == "Head")
                {
                    target.TakeDamage(damage * headshotMultiplier);
                    audioSource.clip = headshotSound;
                }
                else
                {
                    target.TakeDamage(damage);
                    audioSource.clip = shootingSound;
                }

                Debug.Log("We hit something");
                if(audioSource.clip == null)
                {
                    audioSource.clip = shootingSound;
                }

                audioSource.Play();
            }


            //Particles are initiated if we hit something

            if(target == null)
            {
                PlaySound(shootingSound);
            }
            
            Instantiate(bulletHole, hit.point, Quaternion.LookRotation(hit.normal));
        }
        else
        {
            PlaySound(shootingSound);
        }






        //If we don't hit anything, we'll treat the laser as going the full distance. If we hit something, that will be the endpoint
        if (hit.collider != null)
        {
            StartCoroutine(SpawnLineTrail(hit.point, Vector3.Distance(muzzle.transform.position, hit.point)));
        }
        else
        {
            StartCoroutine(SpawnLineTrail((muzzle.transform.position + (fpsCam.transform.forward + directionRay) * range), range));
        }



        muzzleFlash.Play();

        readyForNextShot = false;
        anim.SetBool("Firing", true);

        StartCoroutine(RateOfFire());

    }

    IEnumerator RateOfFire()
    {
        yield return new WaitForSeconds(rateOfFire);
        readyForNextShot = true;

    }


    public IEnumerator SpawnLineTrail(Vector3 hitPoint, float distance)
    {

        float timer = 0;
        Vector3 startPosition = muzzle.transform.position;

        //Add starting and end positions to the line renderer
        int currentPositions = bulletLineRenderer.positionCount;
        bulletLineRenderer.positionCount = currentPositions + 2;

        //Equation to make laser travel at a consistent speed, no matter the distance from hand
        float timeBasedOnDistance = bulletTravelTime * ((Mathf.Abs(distance) / distanceSpeed));


        while (timer < timeBasedOnDistance)
        {
            yield return null;

            //Front end of laser
            bulletLineRenderer.SetPosition(currentPositions, Vector3.Lerp(muzzle.transform.position, hitPoint, timer));


            //Back end of laser. Lags a little behind the front
            if (timer > laserOffset)
            {
                bulletLineRenderer.SetPosition(currentPositions + 1, Vector3.Lerp(muzzle.transform.position, hitPoint, (timer - laserOffset)));
            }
            else
            {
                bulletLineRenderer.SetPosition(currentPositions + 1, muzzle.transform.position);
            }


            timer += Time.deltaTime;

        }


        //Reset everything to get rid of laser
        bulletLineRenderer.positionCount = 0;
        bulletLineRenderer.enabled = false;
        anim.SetBool("Firing", false);
    }



    public void PlaySound(AudioClip newClip)
    {
        audioSource.clip = newClip;
        audioSource.Play();
    }

}
