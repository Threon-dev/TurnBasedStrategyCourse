using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFindingUpdater : MonoBehaviour
{
    void Start()
    {
        DestructibleCrate.OnAnyDestroyed += DestructibleCrateOnOnAnyDestroyed;
    }

    private void DestructibleCrateOnOnAnyDestroyed(object sender, EventArgs e)
    {
        DestructibleCrate destructibleCrate = sender as DestructibleCrate;
        PathFinding.Instance.SetWalkableGridPosition(destructibleCrate.GetGridPosition(),true);
    }
    
}
