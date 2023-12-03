using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PathFinding : MonoBehaviour
{

    public static PathFinding Instance { get; private set; }
    
    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;
    
    
    [SerializeField] private Transform gridDebugObjectPrefab;
    [SerializeField] private LayerMask obstaclesLayerMask;
    private int _width;
    private int _height;
    private float _cellSize;

    private GridSystem<PathNode> _gridSystem;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Setup(int width, int height, float cellSize)
    {
        _width = width;
        _height = height;
        _cellSize = cellSize;
        
        _gridSystem = new GridSystem<PathNode>(_width, _height,_cellSize,
            (GridSystem<PathNode> g,GridPosition gridPosition) =>  new PathNode(gridPosition));
        //_gridSystem.CreateDebugObjects(gridDebugObjectPrefab);

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);
                Vector3 worldPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);
                float raycastOffsetDistnace = 5f;
                if (Physics.Raycast(worldPosition + Vector3.down * raycastOffsetDistnace,
                        Vector3.up,
                        raycastOffsetDistnace * 2,
                        obstaclesLayerMask))
                {
                    GetNode(x,z).SetIsWalkable(false);
                }
            }
        }
    }

    public List<GridPosition> FindPath(GridPosition startGridPosition, GridPosition endGridPosition, out int pathLenght)
    {
        List<PathNode> openList = new List<PathNode>();
        List<PathNode> closedList = new List<PathNode>();

        PathNode startNode = _gridSystem.GetGridObject(startGridPosition);
        PathNode endNode = _gridSystem.GetGridObject(endGridPosition);
        openList.Add(startNode);

        for (int x = 0; x < _gridSystem.GetWidth(); x++)
        {
            for (int z = 0; z < _gridSystem.GetHeight(); z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);
                PathNode pathNode = _gridSystem.GetGridObject(gridPosition);
                
                pathNode.SetGCost(int.MaxValue);
                pathNode.SetHCost(0);
                pathNode.CalculateFCost();
                pathNode.ResetCameFromPathNode();
            }
        }
        
        startNode.SetGCost(0);
        startNode.SetHCost(CalculateDistance(startGridPosition,endGridPosition));
        startNode.CalculateFCost();

        while (openList.Count > 0)
        {
            PathNode currentNode = GetLowestFCostPathNode(openList);

            if (currentNode == endNode)
            {
                //Reached final node
                pathLenght = endNode.GetFCost();
                return CalculatePath(endNode);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);


            foreach (var VARIABLE in GetNeighbourList(currentNode))
            {
                if (closedList.Contains(VARIABLE))
                {
                    continue;
                }

                if (!VARIABLE.IsWalkable())
                {
                    closedList.Add(VARIABLE);
                    continue;
                }

                int tentativeGCost = currentNode.GetGCost() +
                                     CalculateDistance(currentNode.GetGridPosition(), VARIABLE.GetGridPosition());

                if (tentativeGCost < VARIABLE.GetGCost())
                {
                    VARIABLE.SetCameFromPathNode(currentNode);
                    VARIABLE.SetGCost(tentativeGCost);
                    VARIABLE.SetHCost(CalculateDistance(VARIABLE.GetGridPosition(),endGridPosition));
                    VARIABLE.CalculateFCost();
                    if (!openList.Contains(VARIABLE))
                    {
                        openList.Add(VARIABLE);
                    }
                }
            }
        }
        
        // No path found
        pathLenght = 0;
        return null;
    }

    public int CalculateDistance(GridPosition a, GridPosition b)
    {
        GridPosition gridPositionDistance = a - b;
        
        int xDistance = Mathf.Abs(gridPositionDistance.x);
        int zDistance = Mathf.Abs(gridPositionDistance.z);
        int remaining = Mathf.Abs(xDistance - zDistance);
        return MOVE_DIAGONAL_COST * Mathf.Min(xDistance,zDistance) + MOVE_STRAIGHT_COST * remaining;
    }

    private PathNode GetLowestFCostPathNode(List<PathNode> pathNodeList)
    {
        PathNode lowestFCostPathNode = pathNodeList[0];
        for (int i = 0; i < pathNodeList.Count; i++)
        {
            if (pathNodeList[i].GetFCost() < lowestFCostPathNode.GetFCost())
            {
                lowestFCostPathNode = pathNodeList[i];
            }
        }

        return lowestFCostPathNode;
    }

    private PathNode GetNode(int x, int z)
    {
        return _gridSystem.GetGridObject(new GridPosition(x, z));
    }

    private List<PathNode> GetNeighbourList(PathNode currentNode)
    {
        List<PathNode> neighboutList = new List<PathNode>();

        GridPosition gridPosition = currentNode.GetGridPosition();

        if (gridPosition.x - 1 >= 0)
        {
            neighboutList.Add(GetNode(gridPosition.x - 1, gridPosition.z + 0));
            if (gridPosition.z + 1 < _gridSystem.GetHeight())
            {
                neighboutList.Add(GetNode(gridPosition.x - 1, gridPosition.z + 1));
            }
            if (gridPosition.z - 1 >= 0)
            {
                neighboutList.Add(GetNode(gridPosition.x - 1, gridPosition.z - 1));
            }
        }

        if (gridPosition.x + 1 < _gridSystem.GetWidth())
        {
            neighboutList.Add(GetNode(gridPosition.x + 1, gridPosition.z + 0));

            if (gridPosition.z + 1 < _gridSystem.GetHeight())
            { 
                neighboutList.Add(GetNode(gridPosition.x + 1, gridPosition.z + 1));
            }

            if (gridPosition.z - 1 >= 0)
            {
                neighboutList.Add(GetNode(gridPosition.x + 1, gridPosition.z - 1));
            }
        }

        if (gridPosition.z + 1 < _gridSystem.GetHeight())
        {
            neighboutList.Add(GetNode(gridPosition.x + 0, gridPosition.z + 1));
        }

        if (gridPosition.z - 1 >= 0)
        {
            neighboutList.Add(GetNode(gridPosition.x  + 0, gridPosition.z - 1));
        }
        return neighboutList;
    }

    private List<GridPosition> CalculatePath(PathNode endNode)
    {
        List<PathNode> pathNodeList = new List<PathNode>();
        pathNodeList.Add(endNode);
        PathNode currentNode = endNode;
        while (currentNode.GetCameFromPathNode() != null)
        {
            pathNodeList.Add(currentNode.GetCameFromPathNode());
            currentNode = currentNode.GetCameFromPathNode();
        }
        
        pathNodeList.Reverse();

        List<GridPosition> gridPositionList = new List<GridPosition>();
        foreach (var VARIABLE in pathNodeList)
        {
            gridPositionList.Add(VARIABLE.GetGridPosition());
        }

        return gridPositionList;
    }

    public void SetWalkableGridPosition(GridPosition gridPosition,bool isWalkable)
    {
        _gridSystem.GetGridObject(gridPosition).SetIsWalkable(isWalkable);
    }
    public bool IsWalkableGridPosition(GridPosition gridPosition)
    {
        return _gridSystem.GetGridObject(gridPosition).IsWalkable();
    }

    public bool HasPath(GridPosition startGridPosition, GridPosition endGridPosition)
    {
        return FindPath(startGridPosition, endGridPosition,out int pathLenght) != null;
    }

    public int GetPathLenght(GridPosition startGridPosition, GridPosition endGridPosition)
    {
        FindPath(startGridPosition, endGridPosition, out int pathLenght);
        return pathLenght;
    }
}
