using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanAction : GoapAction
{
    private float startTime = 0;
    public float Duration = 2; // seconds
    private bool isClean = false;
    private SupplyPileComponent targetSupplyPile;

    public CleanAction()
    {
        AddPrecondition("hasOre", false);
        AddEffect("clean", true);
    }


    public override void Reset()
    {
        isClean = false;
        startTime = 0;
    }

    public override bool IsDone()
    {
        return isClean;
    }

    public override bool CheckProceduralPrecondition(GameObject agent)
    {
        SupplyPileComponent[] supplyPiles = (SupplyPileComponent[])UnityEngine.GameObject.FindObjectsOfType(typeof(SupplyPileComponent));
        SupplyPileComponent closest = null;
        float closestDist = 0;

        foreach (SupplyPileComponent supply in supplyPiles)
        {
            if (closest == null)
            {
                // first one, so choose it for now
                closest = supply;
                closestDist = (supply.gameObject.transform.position - agent.transform.position).magnitude;
            }
            else
            {
                // is this one closer than the last?
                float dist = (supply.gameObject.transform.position - agent.transform.position).magnitude;
                if (dist < closestDist)
                {
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

    public override bool RequiresInRange()
    {
        return true; // yes we need to be near a supply pile
    }

 
    public override bool Perform(GameObject agent)
    {
        if (startTime == 0)
            startTime = Time.time;

        if (Time.time - startTime > Duration)
        {
            // finished forging a tool
            Blackboard blackBoard = agent.GetComponent<NPC_Entities>().blackBoard;
            blackBoard.numOre = 0;
            isClean = true;
        }
        return true;
    }

}
