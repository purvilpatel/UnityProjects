using UnityEngine;
using System.Collections;

public class RayCast : MonoBehaviour {

    private GameObject Harry, Daniel;
    private bool isHarryVisible, isDanielVisible;

	// Use this for initialization
	void Start () {
        Harry = GameObject.FindGameObjectWithTag("Harry");
        Daniel = GameObject.FindGameObjectWithTag("Daniel");
        isHarryVisible = false;
        isDanielVisible = false;
	}
	
	// Update is called once per frame
	void Update () {
    
            //if (Harry.GetComponent<MeshRenderer>().isVisible == true)
            //{
            //    isHarryVisible = true;
            //    Debug.Log("harry true");
            //}
            //else
            //{
            //    isHarryVisible = false;
            //    Debug.Log("harry false");
            //}

            //if (Daniel.GetComponent<MeshRenderer>().isVisible == true)
            //{
            //    isDanielVisible = true;
            //    Debug.Log("dan true");
            //}
            //else
            //{
            //    isDanielVisible = false;
            //    Debug.Log("dan false");
            //}

	}
}
