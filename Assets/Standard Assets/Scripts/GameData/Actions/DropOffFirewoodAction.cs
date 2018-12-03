
using System;
using UnityEngine;

public class DropOffFirewoodAction : GoapAction
{
	private bool droppedOffFirewood = false;
	private SupplyPileComponent targetSupplyPile; // where we drop off the firewood
	
	public DropOffFirewoodAction () {
		AddPrecondition ("hasFirewood", true); // can't drop off firewood if we don't already have some
		AddEffect ("hasFirewood", false); // we now have no firewood
		AddEffect ("collectFirewood", true); // we collected firewood
	}
	
	
	public override void Reset ()
	{
		droppedOffFirewood = false;
		targetSupplyPile = null;
	}
	
	public override bool IsDone ()
	{
		return droppedOffFirewood;
	}
	
	public override bool RequiresInRange ()
	{
		return true; // yes we need to be near a supply pile so we can drop off the firewood
	}
	
	public override bool CheckProceduralPrecondition (GameObject agent)
	{
		// find the nearest supply pile that has spare firewood
		SupplyPileComponent[] supplyPiles = (SupplyPileComponent[]) UnityEngine.GameObject.FindObjectsOfType ( typeof(SupplyPileComponent) );
		SupplyPileComponent closest = null;
		float closestDist = 0;
		
		foreach (SupplyPileComponent supply in supplyPiles) {
			if (closest == null) {
				// first one, so choose it for now
				closest = supply;
				closestDist = (supply.gameObject.transform.position - agent.transform.position).magnitude;
			} else {
				// is this one closer than the last?
				float dist = (supply.gameObject.transform.position - agent.transform.position).magnitude;
				if (dist < closestDist) {
					// we found a closer one, use it
					closest = supply;
					closestDist = dist;
				}
			}
		}
		if (closest == null)
			return false;

		targetSupplyPile = closest;
		target = targetSupplyPile.gameObject;
		
		return closest != null;
	}
	
	public override bool Perform (GameObject agent)
	{
        Blackboard blackBoard = agent.GetComponent<NPC_Entities>().blackBoard;
        targetSupplyPile.numFirewood += blackBoard.numFirewood;
		droppedOffFirewood = true;
        blackBoard.numFirewood = 0;
		
		return true;
	}
}
