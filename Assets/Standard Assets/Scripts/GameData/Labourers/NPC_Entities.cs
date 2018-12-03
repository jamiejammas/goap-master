using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/**
 * A general labourer class.
 * You should subclass this for specific Labourer classes and implement
 * the createGoalState() method that will populate the goal for the GOAP
 * planner.
 */
public abstract class NPC_Entities : MonoBehaviour, IGoap
{
    public float moveSpeed = 1;
    public Blackboard blackBoard;

    protected HashSet<KeyValuePair<string, object>> goal = new HashSet<KeyValuePair<string, object>>();


   [HideInInspector]
    public string currentPlan = "Plan:";

    [HideInInspector]
    public string currentGoal
    {
        get
        {
            var t = "";
            foreach (KeyValuePair<string, object> g in goal)
            {
                t ="Goal: " + g.Key.ToString() + " <|:|> " + g.Value.ToString();
            }
            return t;
        }
    }


    void Start ()
	{
		if (blackBoard == null)
			blackBoard = gameObject.GetComponent<Blackboard>();
		if (blackBoard.tool == null) {
			GameObject prefab = Resources.Load<GameObject> (blackBoard.toolType);
			GameObject tool = Instantiate (prefab, transform.position, transform.rotation) as GameObject;
			blackBoard.tool = tool;
			tool.transform.parent = transform; // attach the tool
		}
	}
    


	void Update ()
	{

	}


    public void UpdateWorldState(object key, object value)
    {
        //update the world state
    }


    /**
	 * Key-Value data that will feed the GOAP actions and system while planning.
	 */
    public HashSet<KeyValuePair<string,object>> GetMyWorldState ()
    {
		HashSet<KeyValuePair<string,object>> worldData = new HashSet<KeyValuePair<string,object>> ();

		worldData.Add(new KeyValuePair<string, object>("hasOre", (blackBoard.numOre > 0) ));
		worldData.Add(new KeyValuePair<string, object>("hasLogs", (blackBoard.numLogs > 0) ));
		worldData.Add(new KeyValuePair<string, object>("hasFirewood", (blackBoard.numFirewood > 0) ));
		worldData.Add(new KeyValuePair<string, object>("hasTool", (blackBoard.tool != null) ));

		return worldData;
	}

	/**
	 * Implement in subclasses
	 */
	public abstract HashSet<KeyValuePair<string,object>> CreateMyGoalState ();


	public void PlanFailed (HashSet<KeyValuePair<string, object>> failedGoal)
	{
		// Not handling this here since we are making sure our goals will always succeed.
		// But normally you want to make sure the world state has changed before running
		// the same goal again, or else it will just fail.
	}

	public void PlanFound (HashSet<KeyValuePair<string, object>> goal, Queue<GoapAction> actions)
	{
		// Yay we found a plan for our goal
		currentPlan = ("Plan: "+ GoapAgent.PrettyPrint(actions));
	}

	public void ActionsFinished ()
	{
		// Everything is done, we completed our actions for this gool. Hooray!
		Debug.Log ("<color=blue>Actions completed</color>");
	}

	public void PlanAborted (GoapAction aborter)
	{
		// An action bailed out of the plan. State has been reset to plan again.
		// Take note of what happened and make sure if you run the same goal again
		// that it can succeed.
		Debug.Log ("<color=red>Plan Aborted</color> "+GoapAgent.PrettyPrint(aborter));
	}

	public bool MoveAgent(GoapAction nextAction)
    {
		// move towards the NextAction's target
		float step = moveSpeed * Time.deltaTime;
		gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, nextAction.target.transform.position, step);
		
		if (gameObject.transform.position.Equals(nextAction.target.transform.position) )
        {
			// we are at the target location, we are done
			nextAction.SetInRange(true);
			return true;
		} else
			return false;
	}
}
