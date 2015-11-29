using UnityEngine;
using System.Collections;

// Require these components when using this script
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
public class DanControl : MonoBehaviour
{
    [System.NonSerialized]
    public float lookWeight;                    // the amount to transition when using head look

    [System.NonSerialized]
    public Transform enemy;                     // a transform to Lerp the camera to during head look

    public float animSpeed = 1.5f;              // a public setting for overall animator animation speed
    public float lookSmoother = 3f;             // a smoothing setting for camera motion
    public bool useCurves;                      // a setting for teaching purposes to show use of curves


    private Animator anim;                          // a reference to the animator on the character
    private AnimatorStateInfo currentBaseState;         // a reference to the current state of the animator, used for base layer
    private AnimatorStateInfo layer2CurrentState;   // a reference to the current state of the animator, used for layer 2
    private CapsuleCollider col;                    // a reference to the capsule collider of the character

    static int idleState = Animator.StringToHash("Layer2.Idle");
    static int fightState = Animator.StringToHash("Layer2.Fight");
    static int walkrun = Animator.StringToHash("Layer2.WalkRun");
    private bool idleAgain = false;
    private bool walking = false;

    void Start()
    {
        // initialising reference variables
        anim = GetComponent<Animator>();
        col = GetComponent<CapsuleCollider>();
        enemy = GameObject.Find("Enemy").transform;
        if (anim.layerCount == 2)
            anim.SetLayerWeight(1, 1);
    }


    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");              // setup h variable as our horizontal input axis
        float v = Input.GetAxis("Vertical");                // setup v variables as our vertical input axis
        anim.SetFloat("Speed", v);                          // set our animator's float parameter 'Speed' equal to the vertical input axis				
        anim.SetFloat("Direction", h);                      // set our animator's float parameter 'Direction' equal to the horizontal input axis		
        anim.speed = animSpeed;                             // set the speed of our animator to the public variable 'animSpeed'
        anim.SetLookAtWeight(lookWeight);                   // set the Look At Weight - amount to use look at IK vs using the head's animation
        currentBaseState = anim.GetCurrentAnimatorStateInfo(0); // set our currentState variable to the current state of the Base Layer (0) of animation

        if (anim.layerCount == 2)
            layer2CurrentState = anim.GetCurrentAnimatorStateInfo(1);   // set our layer2CurrentState variable to the current state of the second Layer (1) of animation


        // LOOK AT ENEMY

        // if we hold Alt..
        if (Input.GetButton("Fire2"))
        {
            // ...set a position to look at with the head, and use Lerp to smooth the look weight from animation to IK (see line 54)
            anim.SetLookAtPosition(enemy.position);
            lookWeight = Mathf.Lerp(lookWeight, 1f, Time.deltaTime * lookSmoother);
        }
        // else, return to using animation for the head by lerping back to 0 for look at weight
        else
        {
            lookWeight = Mathf.Lerp(lookWeight, 0f, Time.deltaTime * lookSmoother);
        }

        // STANDARD JUMPING

        // if we are currently in a state called Locomotion (see line 25), then allow Jump input (Space) to set the Jump bool parameter in the Animator to true
        
        if (currentBaseState.nameHash == walkrun)
        {
            walking = true;
            Debug.Log("walk true");
            anim.SetBool("walking", true);
        }

        if (currentBaseState.nameHash == fightState)
        {
            float random = Mathf.Ceil(Random.Range(-1.0F, 2.0F));
            anim.SetFloat("FightMove", random);
            Debug.Log(random);

        }

        // if we are in the jumping state... 
        


        // JUMP DOWN AND ROLL 

        // if we are jumping down, set our Collider's Y position to the float curve from the animation clip - 
        // this is a slight lowering so that the collider hits the floor as the character extends his legs
        

        // if we are falling, set our Grounded boolean to true when our character's root 
        // position is less that 0.6, this allows us to transition from fall into roll and run
        // we then set the Collider's Height equal to the float curve from the animation clip
        

        // if we are in the roll state and not in transition, set Collider Height to the float curve from the animation clip 
        // this ensures we are in a short spherical capsule height during the roll, so we can smash through the lower
        // boxes, and then extends the collider as we come out of the roll
        // we also moderate the Y position of the collider using another of these curves on line 128
       
        // IDLE

        // check if we are at idle, if so, let us Wave!
        else if (currentBaseState.nameHash == idleState)
        {
            if (walking == true)
            {
                Debug.Log("fight true");
                anim.SetBool("fight", true);
            }
            
        }
       
    }
}
