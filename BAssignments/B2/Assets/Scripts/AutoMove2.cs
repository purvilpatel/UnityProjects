using UnityEngine;
using System.Collections;

public class AutoMove2 : MonoBehaviour {
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
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(-15, 4.5f, -27), step);
            if (transform.position == new Vector3(-15, 4.5f, -27))
            {
                move = -1;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(-15, 4.5f, -15), step);
            if (transform.position == new Vector3(-15, 4.5f, -15))
            {
                move = 1;
            }
        }
	}
}
