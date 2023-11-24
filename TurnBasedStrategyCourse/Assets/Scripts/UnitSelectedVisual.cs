using System;
using UnityEngine;

public class UnitSelectedVisual : MonoBehaviour
{
    private Unit _unit;
    private MeshRenderer _meshRenderer;
    
    private void Awake()
    {
        _unit = GetComponentInParent<Unit>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        UnitActionSystem.Instance.OnSelectedUnitChanged += InstanceOnOnSelectedUnitChanged;
        UpdateVisual();
    }

    private void InstanceOnOnSelectedUnitChanged()
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        if (_unit == UnitActionSystem.Instance.SelectedUnit)
        {
            SetUnitSelectedVisual(true);
        }
        else
        {
            SetUnitSelectedVisual(false);
        }
    }

    private void SetUnitSelectedVisual(bool isShowed)
    {
        _meshRenderer.enabled = isShowed;
    }

    private void OnDestroy()
    {
        UnitActionSystem.Instance.OnSelectedUnitChanged -= InstanceOnOnSelectedUnitChanged;
    }
}
