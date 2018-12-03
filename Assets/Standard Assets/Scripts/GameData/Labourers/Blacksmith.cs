using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Blacksmith : NPC_Entities
{
	/**
	 * Our only goal will ever be to make tools.
	 * The ForgeTooldAction will be able to fulfill this goal.
	 */
	public override HashSet<KeyValuePair<string,object>> CreateMyGoalState ()
    {
      //  this.goal.Add(new KeyValuePair<string, object>("collectTools", true ));
        this.goal.Add(new KeyValuePair<string, object>("clean", true));
        return goal;
	}
}


/*

forge tool action
drop off tools action
pick up ore action

*/