using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Miner : NPC_Entities
{
	/**
	 * Our only goal will ever be to mine ore.
	 * The MineOreAction will be able to fulfill this goal.
	 */
	public override HashSet<KeyValuePair<string,object>> CreateMyGoalState ()
    {
		this.goal.Add(new KeyValuePair<string, object>("collectOre", true ));
		return goal;
	}

}

