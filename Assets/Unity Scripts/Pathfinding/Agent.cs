using UnityEngine;
using UnityEngine.AI;

public class Agent : MonoBehaviour
{
    public Transform target;

    NavMeshAgent agentComp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agentComp = GetComponent<NavMeshAgent>();
        agentComp.updateRotation = false;
        agentComp.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        agentComp.SetDestination(target.position);
        
        transform.rotation = Quaternion.identity;
    }
}
