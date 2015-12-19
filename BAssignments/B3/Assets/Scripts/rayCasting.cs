using UnityEngine;
using System.Collections;

public class rayCasting : MonoBehaviour {

    public Camera camera;
    GameObject[] agents,obstacles,nazguls;
    bool isAgentSelected, isObstacleSelected, isNazgulSelected;
    int[] agentSelected, nazgulSelected;
    int obstacleSelected;
    public int agentCount,obstacleCount,nazgulCount;

    void Start()
    {
        agents = new GameObject[agentCount];
        nazguls = new GameObject[nazgulCount];
        obstacles = new GameObject[obstacleCount];
        agentSelected = new int[agentCount];
        nazgulSelected = new int[nazgulCount];
        for (int i=0;i< agentCount; i++)
        {
            agentSelected[i] = 0;
            agents[i] = GameObject.FindGameObjectWithTag("Agent"+i);
        }
        for (int i = 0; i < nazgulCount; i++)
        {
            nazgulSelected[i] = 0;
            nazguls[i] = GameObject.FindGameObjectWithTag("Nazgul" + i);
        }
        for (int i = 0; i < obstacleCount; i++)
        {
            obstacles[i] = GameObject.FindGameObjectWithTag("Obstacle" + i);
        }
        obstacleSelected = -1;
        isAgentSelected = false;
        isObstacleSelected =false;
        isNazgulSelected = false;
    }

    void Update()
    {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity) ) // Physics.Raycast(ray, out hit))
            {
                isAgentSelected = false;
                isNazgulSelected = false;
                for (int i = 0; i < agentCount; i++)
                {
                    if(hit.collider.tag == ("Agent" + i))
                    {
                        agents[i].GetComponent<MeshRenderer>().material.color = Color.white;
                        agentSelected[i] = 1;
                        isAgentSelected = true;
                    }
                }
                for (int i = 0; i < nazgulCount; i++)
                {
                    if (hit.collider.tag == ("Nazgul" + i))
                    {
                        nazguls[i].GetComponent<MeshRenderer>().material.color = Color.black;
                        nazgulSelected[i] = 1;
                        isNazgulSelected = true;
                    }
                }
                if (!isAgentSelected && !isNazgulSelected && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
                {
                    for (int i = 0; i < agentCount ; i++)
                    {
                        if(agentSelected[i] == 1)
                        {
                            NavMeshAgent NavAgent = agents[i].GetComponent<NavMeshAgent>();
                            agents[i].GetComponent<MeshRenderer>().material.color = Color.blue;
                            agentSelected[i] = 0;
                            NavAgent.destination = hit.point;
                        }                        
                    }

                    for (int i = 0; i < nazgulCount; i++)
                    {
                        if (nazgulSelected[i] == 1)
                        {
                            NavMeshAgent NavAgent = nazguls[i].GetComponent<NavMeshAgent>();
                            nazguls[i].GetComponent<MeshRenderer>().material.color = Color.yellow;
                            nazgulSelected[i] = 0;
                            NavAgent.destination = hit.point;
                        }
                    }
                }


                if (obstacleSelected >= 0 && isObstacleSelected)
                {
                    obstacles[obstacleSelected].GetComponent<MeshRenderer>().material.color = Color.red;
                    isObstacleSelected = false;
                    obstacleSelected = -1;
                }

                for (int i = 0; i < obstacleCount; i++)
                {
                    if (hit.collider.tag == ("Obstacle" + i))
                    {
                        obstacles[i].GetComponent<MeshRenderer>().material.color = Color.green;
                        obstacleSelected = i;
                        isObstacleSelected = true;
                    }
                }
            }
        }  
        
        if(isObstacleSelected && obstacleSelected>-1)
        {
            Rigidbody rb = obstacles[obstacleSelected].GetComponent<Rigidbody>();
            float horMovement = Input.GetAxis("Horizontal");
            float verMovement = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(horMovement, 0.0f, verMovement);
            rb.AddForce(movement * 20);
        }
        
            
    }

}
