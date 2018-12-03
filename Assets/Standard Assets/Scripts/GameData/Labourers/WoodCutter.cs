using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WoodCutter : NPC_Entities
{
	/**
	 * Our only goal will ever be to chop logs.
	 * The ChopFirewoodAction will be able to fulfill this goal.
	 */
	public override HashSet<KeyValuePair<string,object>> CreateMyGoalState ()
    {
		this.goal.Add(new KeyValuePair<string, object>("collectFirewood", true ));
		return goal;
	}
}

