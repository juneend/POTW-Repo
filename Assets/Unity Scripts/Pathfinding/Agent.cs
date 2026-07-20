//  Author: June Endstrasser

using UnityEngine;
using UnityEngine.AI;

public class Agent : MonoBehaviour
{
    public Transform target;

    NavMeshAgent agentComp;
    Animator anim;
    Vector2 Oldmovement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agentComp = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        agentComp.updateRotation = false;
        agentComp.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        //set the new agent destination
        agentComp.SetDestination(target.position);
        
        //lock 2d rotation
        transform.rotation = Quaternion.identity;

        //read the agent's movement vector
        Vector2 movement = agentComp.desiredVelocity.normalized;

        //is the agent moving?
        if (movement.sqrMagnitude >= 0.01f)
        {

            Oldmovement = movement;

            anim.SetBool("isWalking", true);

            anim.SetFloat("InputX", movement.x);
            anim.SetFloat("InputY", movement.y);
        }
        else
        {
            anim.SetBool("isWalking", false);

            anim.SetFloat("LastInputX", Oldmovement.x);
            anim.SetFloat("LastInputY", Oldmovement.y);
        }

    }
}
