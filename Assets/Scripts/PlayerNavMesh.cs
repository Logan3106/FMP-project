using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerNavMesh : MonoBehaviour
{
    [SerializeField] private Transform movePositionTranform;

    private NavMeshAgent agent;

    private void Awake()
    {
        GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        agent.destination = movePositionTranform.position;
    }
}
