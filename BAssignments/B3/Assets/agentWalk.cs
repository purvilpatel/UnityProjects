using UnityEngine;
using System.Collections;


public class agentWalk : MonoBehaviour {
    private NavMeshAgent agent;
    public Vector3 desti;
    private bool keepwalking=true;
    private Animator anim;
    public NavMeshAgent dan;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        
    }
    void Update()
    {
        if (keepwalking == true)
        {
            agent.SetDestination(desti);
        }
        if (agent.gameObject.transform.position.x == desti.x && keepwalking == true)
        {
            keepwalking = false;
            anim.speed = 0;
        }
        if (keepwalking == false)
        {
            anim.speed = 1;
            agent.SetDestination(dan.transform.position);
        }
    }
}
