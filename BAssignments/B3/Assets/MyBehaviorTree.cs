using UnityEngine;
using System.Collections;
using TreeSharpPlus;

public class MyBehaviorTree : MonoBehaviour
{
	public Transform wander1;
	public Transform wander2;
	public Transform wander3;
    public Transform wander4;
    public Transform wander5;
    public Transform wander6;
	public GameObject participant;
    public GameObject participant1;

	private BehaviorAgent behaviorAgent;
    private BehaviorAgent behaviorAgent1;
	// Use this for initialization
	void Start ()
	{
		behaviorAgent = new BehaviorAgent (this.BuildTreeRoot ());
		BehaviorManager.Instance.Register (behaviorAgent);
		behaviorAgent.StartBehavior ();
        //behaviorAgent1 = new BehaviorAgent(this.BuildTreeRoot1());
        //BehaviorManager.Instance.Register(behaviorAgent1);
        //behaviorAgent1.StartBehavior();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

    protected Node ST_ApproachAndWait(GameObject participant, Transform target)
	{
		Val<Vector3> position = Val.V (() => target.position);
		return new Sequence(participant.GetComponent<BehaviorMecanim>().Node_GoTo(position), new LeafWait(1000));
	}
    protected Node ST_ApproachAndWait1(Transform target)
    {
        Val<Vector3> position = Val.V(() => target.position);
        return new Sequence(participant1.GetComponent<BehaviorMecanim>().Node_GoTo(position), new LeafWait(1000));
    }

	protected Node BuildTreeRoot()
	{
        GameObject denial = GameObject.FindGameObjectWithTag ("Daniel");
        GameObject harry = GameObject.FindGameObjectWithTag ("Harry");
        GameObject denial1 = GameObject.FindGameObjectWithTag("Daniel1");
        GameObject harry1 = GameObject.FindGameObjectWithTag("Harry1");
        GameObject denial2 = GameObject.FindGameObjectWithTag("Daniel2");
        GameObject harry2 = GameObject.FindGameObjectWithTag("Harry2");
        Sequence maintree = new Sequence(
                  new SequenceParallel(
                    this.ST_ApproachAndWait(denial, this.wander1), 
                    this.ST_ApproachAndWait(harry, this.wander2),
                    this.ST_ApproachAndWait(denial1, this.wander3), 
                    this.ST_ApproachAndWait(harry1, this.wander4),
                    this.ST_ApproachAndWait(denial2, this.wander5), 
                    this.ST_ApproachAndWait(harry2, this.wander6)
                    //this.LookAt(denial, harry.transform.position)
                )
                //this.LookAt(denial,harry)
                
        );

        return maintree;
			
              
	}
    protected Node LookAt(GameObject from, GameObject to)
    {
        BehaviorMecanim behavior = from.GetComponent<BehaviorMecanim>();
        return behavior.Node_OrientTowards(to.transform.position);
    }

    protected Node Fight(GameObject from, GameObject to)
    {
		BehaviorMecanim behavior = from.GetComponent<BehaviorMecanim> ();
		return behavior.Node_Fight (to);
    }
    //protected Node BuildTreeRoot1()
    //{
    //    return
    //        new DecoratorLoop(
    //            new SequenceShuffle(
    //                this.LookAt(participant,participant1)));
    //}
}