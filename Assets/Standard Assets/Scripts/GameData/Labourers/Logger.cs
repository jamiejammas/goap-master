using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Logger : NPC_Entities
{
	/**
	 * Our only goal will ever be to chop trees.
	 * The ChopTreeAction will be able to fulfill this goal.
	 */
	public override HashSet<KeyValuePair<string,object>> CreateMyGoalState ()
    {
		this.goal.Add(new KeyValuePair<string, object>("collectLogs", true ));
		return goal;
	}

}

