using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAction : BaseAction
{
    private float totalSpinAmount;
    private void Update()
    {
        if (!_isActive) return;
        
        float spinAddAmount = 360f * Time.deltaTime;
        transform.eulerAngles += new Vector3(0,spinAddAmount, 0);

        totalSpinAmount += spinAddAmount;
        if(totalSpinAmount >= 360f)
        {
            _isActive = false;
            _onActionComplete();
        }
    }

    public override void TakeAction(GridPosition gridPosition,Action OnSpinComplete)
    {
        _isActive = true;
        totalSpinAmount = 0;
        _onActionComplete = OnSpinComplete;
    }
    
    public override string GetActionName()
    {
        return "Spin";
    }

    public override List<GridPosition> GetValidActionGridPositionList()
    {
        GridPosition unitGridPosition = _unit.GetGridPosition();
        return new List<GridPosition>
        {
            unitGridPosition
        };
    }
}
