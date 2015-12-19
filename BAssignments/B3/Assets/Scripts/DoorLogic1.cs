using UnityEngine;
using System.Collections;

public class DoorLogic1 : MonoBehaviour {

    private bool drawGUI = false;
    private bool doorIsClosed = true;
    public Animation theDoor;
    // Use this for initialization
    void Start () 
    {
       
    }

    void changeDoorState()
    {
        if (doorIsClosed == true)   
        {
           
            theDoor.GetComponent<Animation>();    
            theDoor.CrossFade("PleaseOpenDoor");
            //theDoor.audio.PlayOneShot();
           
            doorIsClosed = false;
        }
    }

    void Hide() { }

    // Update is called once per frame
    void Update () {
        if (drawGUI == true)
        {
            doorIsClosed = true;
            changeDoorState();
        }
    }

 

    void OnTriggerEnter(Collider theCollider)
    {
        if (theCollider.tag == "Soldier")
        {
            drawGUI = true;
            Debug.Log("Rajan Patel");
        }
    }

  

    void OnTriggerExit(Collider theCollider)
    {
        if (theCollider.tag == "Soldier")
        {
            drawGUI = false;
           
        }
    }

    void OnGUI()
    {
        if (drawGUI == true)
        {
            //GUI.Box(new Rect((float)(Screen.width * 0.5 - 51), 300, 100, 22), "Press E to open");
        }
    }
}
