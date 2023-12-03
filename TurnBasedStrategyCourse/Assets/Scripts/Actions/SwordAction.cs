using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAction : BaseAction
{
    public static event Action OnAnySwordHit;
    
    public event Action OnSwordActionStarted;
    public event Action OnSwordActionCompleted;
    private enum State
    {
        SwingingSwordBeforeHit,
        SwingingSwordAfterHit,
    }
    
    private int maxSwordDistance = 1;
    private State _state;
    private float _stateTimer;
    private Unit _targetUnit;

    private void Update()
    {
        if (!_isActive)
        {
            return;
        }
        _stateTimer -= Time.deltaTime;
        switch (_state)
        {
            case State.SwingingSwordBeforeHit:
                Vector3 aimDir = (_targetUnit.GetWorldPosition() - _unit.GetWorldPosition()).normalized;
                
                float rotateSpeed = 10f;
                transform.forward = Vector3.Lerp(transform.forward,aimDir,Time.deltaTime * rotateSpeed);
                break;
            case State.SwingingSwordAfterHit:
                break;
        }
        if (_stateTimer <= 0)
        {
            NextState();
        }
    }

    private void NextState()
    {
        switch (_state)
        {
            case State.SwingingSwordBeforeHit:
                _state = State.SwingingSwordAfterHit;
                float afterHitStateTime = 0.5f;
                _stateTimer = afterHitStateTime;
                _targetUnit.Damage(100);
                OnAnySwordHit?.Invoke();
                break;
            case State.SwingingSwordAfterHit:
                OnSwordActionCompleted?.Invoke();
                ActionComplete();
                break;
        }
    }
    public int GetMaxSwordDistance()
    {
        return maxSwordDistance;
    }
    public override string GetActionName()
    {
        return "Sword";
    }

    public override void TakeAction(GridPosition gridPosition, Action OnActionComplete)
    {
        _targetUnit = LevelGrid.Instance.GetUnitOnGridPosition(gridPosition);
        
        _state = State.SwingingSwordBeforeHit;
        float beforeHitStateTime = 0.7f;
        _stateTimer = beforeHitStateTime;
        
        OnSwordActionStarted?.Invoke();
        ActionStart(OnActionComplete);
    }

    public override List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();

        GridPosition unitGridPosition = _unit.GetGridPosition();
            
        for (int x = -maxSwordDistance; x <= maxSwordDistance; x++)
        {
            for (int z = -maxSwordDistance; z <= maxSwordDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;
                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                {
                    continue;
                }

                if (!LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                {
                    continue;
                }

                Unit targetUnit = LevelGrid.Instance.GetUnitOnGridPosition(testGridPosition);

                if (targetUnit.IsEnemy() == _unit.IsEnemy())
                {
                    continue;
                }
                
                validGridPositionList.Add(testGridPosition);
            }
        }
        
        return validGridPositionList;
    }

    public override EnemyAIAction GetEnemyAIAction(GridPosition gridPosition)
    {
        return new EnemyAIAction
        {
            GridPosition = gridPosition,
            ActionValue = 200,
        };
    }
}
