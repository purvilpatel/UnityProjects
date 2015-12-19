using UnityEngine;
using System.Collections;

public class DanVisible : MonoBehaviour {
    public static bool isDanVisible = false;
    private Animator anim;
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            anim.SetBool("runaway", true);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            anim.SetBool("turn", true);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            anim.SetBool("turn", false);
        }

    }
}
