using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class MoveAction : BaseAction
{
    [SerializeField] private int maxMoveDistance = 4;
    
    private Vector3 _targetPosition;
    private Animator _animator;

    protected override void Awake()
    {
        base.Awake();
        _animator = GetComponentInChildren<Animator>();
        _targetPosition = transform.position;
    }
    private void Update()
    {
        if (!_isActive) return;
        
        Vector3 moveDir = (_targetPosition - transform.position).normalized;
        float stoppingDistance = .1f;
        
        if (Vector3.Distance(transform.position, _targetPosition) > stoppingDistance)
        {
            float moveSpeed = 4f;
            transform.position += moveDir * moveSpeed * Time.deltaTime;
            
            _animator.SetBool("IsWalking",true);
        }
        else
        {
            _animator.SetBool("IsWalking",false);
            _isActive = false;
            _onActionComplete();
        }
        float rotateSpeed = 10f;
        transform.forward = Vector3.Lerp(transform.forward,moveDir,Time.deltaTime * rotateSpeed);
    }
    public override void TakeAction(GridPosition gridPosition,Action OnActionComplete)
    {
        _targetPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);
        _isActive = true;
        _onActionComplete = OnActionComplete;
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
                
                validGridPositionList.Add(testGridPosition);
            }
        }
        
        return validGridPositionList;
    }

    public override string GetActionName()
    {
        return "Move";
    }
}
