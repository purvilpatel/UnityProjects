using UnityEngine;
using System.Collections;

public class LocomotionSimpleAgent : MonoBehaviour
{
    public Animator anim;
    NavMeshAgent agent;
    Vector2 smoothDeltaPosition = Vector2.zero;
    Vector2 velocity = Vector2.zero;

    private AnimatorStateInfo currentBaseState;
    private CapsuleCollider col;                    // a reference to the capsule collider of the character
    public bool useCurves;

    static int idleState = Animator.StringToHash("Base Layer.Idle");
    static int walkState = Animator.StringToHash("Base Layer.WalkForward");
    static int locoState = Animator.StringToHash("Base Layer.Locomotion");          // these integers are references to our animator's states
    static int jumpState = Animator.StringToHash("Base Layer.Jump");                // and are used to check state for various actions to occur
    static int jumpDownState = Animator.StringToHash("Base Layer.JumpDown");        // within our FixedUpdate() function below
    static int fallState = Animator.StringToHash("Base Layer.Fall");
    static int rollState = Animator.StringToHash("Base Layer.Roll");
    static int waveState = Animator.StringToHash("Layer2.Wave");

    void Start()
    {
        //anim = GetComponent<Animator>();
        col = GetComponent<CapsuleCollider>();
        agent = GetComponent<NavMeshAgent>();
        // Don’t update position automatically
        agent.updatePosition = false;
    }

    void FixedUpdate()
    {
        Vector3 worldDeltaPosition = agent.nextPosition - transform.position;

        // Map 'worldDeltaPosition' to local space
        float dx = Vector3.Dot(transform.right, worldDeltaPosition);
        float dy = Vector3.Dot(transform.forward, worldDeltaPosition);
        Vector2 deltaPosition = new Vector2(dx, dy);

        // Low-pass filter the deltaMove
        float smooth = Mathf.Min(1.0f, Time.deltaTime / 0.15f);
        smoothDeltaPosition = Vector2.Lerp(smoothDeltaPosition, deltaPosition, smooth);

        // Update velocity if time advances
        if (Time.deltaTime > 1e-5f)
            velocity = smoothDeltaPosition / Time.deltaTime;

        bool shouldMove = velocity.magnitude > 0.5f && agent.remainingDistance > agent.radius;

        // Update animation parameters
        anim.SetBool("move", shouldMove);
        anim.SetFloat("Speed", velocity.x);
        anim.SetFloat("Direction", velocity.y);
        currentBaseState = anim.GetCurrentAnimatorStateInfo(0);

        GetComponent<LookAt>().lookAtTargetPosition = agent.steeringTarget + transform.forward;
        // STANDARD JUMPING

        // if we are currently in a state called Locomotion (see line 25), then allow Jump input (Space) to set the Jump bool parameter in the Animator to true
        if (currentBaseState.nameHash == locoState)
        {
            if (Input.GetButtonDown("Jump"))
            {
                anim.SetBool("Jump", true);
            }
            if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
            {
                anim.SetBool("ShiftKey", false);
            }
        }

        if (currentBaseState.nameHash == walkState)
        {
            if (Input.GetButtonDown("Jump"))
            {
                anim.SetBool("Jump", true);
            }
            if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
            {
                anim.SetBool("ShiftKey", true);
            }
        }

        // if we are in the jumping state... 
        else if (currentBaseState.nameHash == jumpState)
        {
            //  ..and not still in transition..
            if (!anim.IsInTransition(0))
            {
                if (useCurves)
                    // ..set the collider height to a float curve in the clip called ColliderHeight
                    col.height = anim.GetFloat("ColliderHeight");

                // reset the Jump bool so we can jump again, and so that the state does not loop 
                anim.SetBool("Jump", false);
            }

            // Raycast down from the center of the character.. 
            Ray ray = new Ray(transform.position + Vector3.up, -Vector3.up);
            RaycastHit hitInfo = new RaycastHit();

            if (Physics.Raycast(ray, out hitInfo))
            {
                // ..if distance to the ground is more than 1.75, use Match Target
                if (hitInfo.distance > 1.75f)
                {

                    // MatchTarget allows us to take over animation and smoothly transition our character towards a location - the hit point from the ray.
                    // Here we're telling the Root of the character to only be influenced on the Y axis (MatchTargetWeightMask) and only occur between 0.35 and 0.5
                    // of the timeline of our animation clip
                    anim.MatchTarget(hitInfo.point, Quaternion.identity, AvatarTarget.Root, new MatchTargetWeightMask(new Vector3(0, 1, 0), 0), 0.35f, 0.5f);
                }
            }
        }


        // JUMP DOWN AND ROLL 

        // if we are jumping down, set our Collider's Y position to the float curve from the animation clip - 
        // this is a slight lowering so that the collider hits the floor as the character extends his legs
        else if (currentBaseState.nameHash == jumpDownState)
        {
            col.center = new Vector3(0, anim.GetFloat("ColliderY"), 0);
        }

        // if we are falling, set our Grounded boolean to true when our character's root 
        // position is less that 0.6, this allows us to transition from fall into roll and run
        // we then set the Collider's Height equal to the float curve from the animation clip
        else if (currentBaseState.nameHash == fallState)
        {
            col.height = anim.GetFloat("ColliderHeight");
        }

        // if we are in the roll state and not in transition, set Collider Height to the float curve from the animation clip 
        // this ensures we are in a short spherical capsule height during the roll, so we can smash through the lower
        // boxes, and then extends the collider as we come out of the roll
        // we also moderate the Y position of the collider using another of these curves on line 128
        else if (currentBaseState.nameHash == rollState)
        {
            if (!anim.IsInTransition(0))
            {
                if (useCurves)
                    col.height = anim.GetFloat("ColliderHeight");

                col.center = new Vector3(0, anim.GetFloat("ColliderY"), 0);

            }
        }
        // IDLE

        // check if we are at idle, if so, let us Wave!
        else if (currentBaseState.nameHash == idleState)
        {
            if (Input.GetButtonUp("Jump"))
            {
                anim.SetBool("Wave", true);
            }
            if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
            {
                anim.SetBool("ShiftKey", false);
            }
            if (Input.GetButtonDown("Jump"))
            {
                anim.SetBool("Jump", true);
            }
        }

    }

    void OnAnimatorMove()
    {
        // Update position to agent position
        transform.position = agent.nextPosition;
    }
}