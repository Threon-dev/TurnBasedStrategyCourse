using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnitActionSystemUI : MonoBehaviour
{
    [SerializeField] private Transform actionButtonPrefab;
    [SerializeField] private Transform actionButtonContainerTransform;
    [SerializeField] private TextMeshProUGUI actionPointsText;

    private List<ActionButtonUI> _actionButtonUIList;

    private void Awake()
    {
        _actionButtonUIList = new List<ActionButtonUI>();
    }

    private void Start()
    {
        UnitActionSystem.Instance.OnSelectedUnitChanged += InstanceOnOnSelectedUnitChanged;
        UnitActionSystem.Instance.OnSelectedActionChanged += UpdateSelectedVisual;
        UnitActionSystem.Instance.OnActionStarted += UpdateActionPoints;
        Unit.OnAnyActionPointsChanged += UpdateActionPoints;
        
        UpdateActionPoints();
        CreateUnitActionButtons();
        UpdateSelectedVisual();
    }

    private void InstanceOnOnSelectedUnitChanged()
    {
        CreateUnitActionButtons();
        UpdateActionPoints();
    }

    private void CreateUnitActionButtons()
    {
        foreach (Transform VARIABLE in actionButtonContainerTransform)
        {
            Destroy(VARIABLE.gameObject);
        }
        _actionButtonUIList.Clear();
        
        Unit selectedUnit = UnitActionSystem.Instance.SelectedUnit;
        
        foreach (var VARIABLE in selectedUnit.GetBaseActionArray())
        {
            Transform actionButtonTransform = Instantiate(actionButtonPrefab, actionButtonContainerTransform);
            ActionButtonUI actionButtonUI = actionButtonTransform.GetComponent<ActionButtonUI>();
            actionButtonUI.SetBaseAction(VARIABLE);
            _actionButtonUIList.Add(actionButtonUI);
        }
    }

    private void UpdateSelectedVisual()
    {
        foreach (var VARIABLE in _actionButtonUIList)
        {
            VARIABLE.UpdateSelectedVisual();
        }
    }

    private void UpdateActionPoints()
    {
        Unit selectedUnit = UnitActionSystem.Instance.SelectedUnit;
        actionPointsText.text = "Action Points: " + selectedUnit.GetActionPoints();
    }
}
