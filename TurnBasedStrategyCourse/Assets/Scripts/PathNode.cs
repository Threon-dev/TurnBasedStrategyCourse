using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    private GridPosition _gridPosition;
    private int gCost;
    private int hCost;
    private int fCost;
    private PathNode cameFromPathNode;

    public PathNode(GridPosition gridPosition)
    {
        this._gridPosition = gridPosition;
    }
    
    public override string ToString()
    {
        return _gridPosition.ToString();
    }

    public int GetGCost()
    {
        return gCost;
    }
    public int GetHCost()
    {
        return hCost;
    }
    public int GetFCost()
    {
        return fCost;
    }

    public void SetGCost(int gCost)
    {
        this.gCost = gCost;
    }
    public void SetHCost(int hCost)
    {
        this.hCost = hCost;
    }

    public void CalculateFCost()
    {
        fCost = hCost + gCost;
    }

    public void SetCameFromPathNode(PathNode pathNode)
    {
        cameFromPathNode = pathNode;
    }
    public PathNode GetCameFromPathNode()
    {
        return cameFromPathNode;
    }
    public void ResetCameFromPathNode()
    {
        cameFromPathNode = null;
    }

    public GridPosition GetGridPosition()
    {
        return _gridPosition;
    }
}