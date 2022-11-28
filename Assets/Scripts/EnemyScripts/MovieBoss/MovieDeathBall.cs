using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieDeathBall : MonoBehaviour
{
    [SerializeField] GameObject deathBall;
    [SerializeField] float attackWaitTime = 0.5f;
    [SerializeField] Vector3 spawnPos;
    [SerializeField] Transform movieBoss;



    public IEnumerator DeathBallStart()
    {
        yield return new WaitForSeconds(attackWaitTime);

        Instantiate(deathBall, movieBoss.transform.position + spawnPos, Quaternion.Euler(new Vector3(-90, 0, 0)));
    }
}
