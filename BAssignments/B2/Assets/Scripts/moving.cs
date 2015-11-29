using UnityEngine;

public class moving : MonoBehaviour
{
    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100) && agent.gameObject.GetComponent<MeshRenderer>().material.color == Color.white)
            {
                agent.destination = hit.point;
                //agent.gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;
            }
        }
    }
}