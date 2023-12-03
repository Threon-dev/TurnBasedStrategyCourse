using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PathFindingGridDebugObject : GridDebugObject
{
    [SerializeField] private TextMeshPro gCostText;
    [SerializeField] private TextMeshPro hCostText;
    [SerializeField] private TextMeshPro fCostText;

    [SerializeField] private SpriteRenderer isWalkalbeSpriteRenderer;

    private PathNode _pathNode;
    public override void SetGridObject(object gridObject)
    {
        _pathNode = (PathNode)gridObject;
        base.SetGridObject(gridObject);
    }

    protected override void Update()
    {
        base.Update();
        gCostText.text = _pathNode.GetGCost().ToString();
        fCostText.text = _pathNode.GetFCost().ToString();
        hCostText.text = _pathNode.GetHCost().ToString();
        isWalkalbeSpriteRenderer.color = _pathNode.IsWalkable() ? Color.green : Color.red;
    }
}
