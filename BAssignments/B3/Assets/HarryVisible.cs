using UnityEngine;
using System.Collections;

public class HarryVisible : MonoBehaviour {
    public static bool isHarryVisible = false;
    private Animator anim;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.M))
        {
            anim.SetBool("runaway", true);
        }
	}


}
