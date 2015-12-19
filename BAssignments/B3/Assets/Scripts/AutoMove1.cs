using UnityEngine;
using System.Collections;

public class AutoMove1 : MonoBehaviour {
    int move = 1;
	// Use this for initialization
	void Start () {
        move = 1;
	}
	
	// Update is called once per frame
	void Update () {
        float step = 5 * Time.deltaTime;
        if (move == 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(-13, 0.8f, 0), step);
            if (transform.position == new Vector3(-13, 0.8f, 0))
            {
                move = -1;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(-13, 0.8f, 5), step);
            if (transform.position == new Vector3(-13, 0.8f, 5))
            {
                move = 1;
            }
        }
	}
}
