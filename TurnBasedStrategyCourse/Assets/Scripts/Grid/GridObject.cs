using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject
{
    private GridSystem _gridSystem;
    private GridPosition _gridPosition;
    public List<Unit> UnitList { private set; get; }

    public GridObject(GridSystem gridSystem, GridPosition gridPosition)
    {
        _gridSystem = gridSystem;
        _gridPosition = gridPosition;
        UnitList = new List<Unit>();
    }

    public void AddUnit(Unit unit)
    {
        UnitList.Add(unit);
    }

    public void RemoveUnit(Unit unit)
    {
        UnitList.Remove(unit);
    }
    public void ClearUnit()
    {
        UnitList = null;
    }
    public override string ToString()
    {
        string unitString = "";
        foreach (var VARIABLE in UnitList)
        {
            unitString += VARIABLE + "\n";
        }
        return _gridPosition.ToString() + "\n" + unitString;
    }

    public bool HasAnyUnit()
    {
        return UnitList.Count > 0;
    }
}
