using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovieNavigation : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private List<Vector3> nextPosition;
    [SerializeField] private Transform playerTrans;
    public bool canWeMove = true;




    // Update is called once per frame
    void Update()
    {
        if (canWeMove)
        {
            navMeshAgent.destination = playerTrans.position;
        }

    }

    public IEnumerator moveToPosition(Vector3 newPosition)
    {
        while(canWeMove)
        {
            yield return null;
            navMeshAgent.destination = playerTrans.position;
        }
    }
}
