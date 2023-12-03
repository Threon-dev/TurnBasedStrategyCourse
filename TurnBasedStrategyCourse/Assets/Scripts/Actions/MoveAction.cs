using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class MoveAction : BaseAction
{
    public event Action OnStartMoving;
    public event Action OnStopMoving;
    
    
    [SerializeField] private int maxMoveDistance = 4;
    
    private List<Vector3> _positionList;
    private int currentPositionIndex;
    
    private void Update()
    {
        if (!_isActive) return;


        Vector3 targetPosition = _positionList[currentPositionIndex];
        Vector3 moveDir = (targetPosition - transform.position).normalized;
        float stoppingDistance = .1f;
        
        float rotateSpeed = 10f;
        transform.forward = Vector3.Lerp(transform.forward,moveDir,Time.deltaTime * rotateSpeed);
        if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance)
        {
            float moveSpeed = 4f;
            transform.position += moveDir * moveSpeed * Time.deltaTime;
        }
        else
        {
            currentPositionIndex++;
            if (currentPositionIndex >= _positionList.Count)
            {
                OnStopMoving?.Invoke();
                ActionComplete();
            }
        }
    }
    public override void TakeAction(GridPosition gridPosition,Action OnActionComplete)
    {
        List<GridPosition> gridPositionsList = PathFinding.Instance.FindPath(_unit.GetGridPosition(), gridPosition,out int pathLenght);
        currentPositionIndex = 0;

        _positionList = new List<Vector3>();

        foreach (var VARIABLE in gridPositionsList)
        {
            _positionList.Add(LevelGrid.Instance.GetWorldPosition(VARIABLE));
        }
        
        OnStartMoving?.Invoke();
        
        ActionStart(OnActionComplete);
    }

    public override List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();
        GridPosition unitGridPosition = _unit.GetGridPosition();

        for (int x = -maxMoveDistance; x <= maxMoveDistance; x++)
        {
            for (int z = -maxMoveDistance; z <= maxMoveDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;
                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                {
                    continue;
                }

                if (unitGridPosition == testGridPosition)
                {
                    continue;
                }

                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                {
                    continue;
                }

                if (!PathFinding.Instance.IsWalkableGridPosition(testGridPosition))
                {
                    continue;
                }
                if (!PathFinding.Instance.HasPath(unitGridPosition,testGridPosition))
                {
                    continue;
                }

                
                int pathFindingDistanceMultiplier = 10;
                if (PathFinding.Instance.GetPathLenght(unitGridPosition, testGridPosition) >
                    maxMoveDistance * pathFindingDistanceMultiplier)
                {
                    continue;
                }
                
                validGridPositionList.Add(testGridPosition);
            }
        }
        
        return validGridPositionList;
    }

    public override string GetActionName()
    {
        return "Move";
    }
    
    public override EnemyAIAction GetEnemyAIAction(GridPosition gridPosition)
    {
        int targetCountAtGridPosition = _unit.GetAction<ShootAction>().GetTargetCountAtPosition(gridPosition);
        return new EnemyAIAction()
        {
            GridPosition = gridPosition,
            ActionValue = targetCountAtGridPosition * 10,
        };
    }
}
