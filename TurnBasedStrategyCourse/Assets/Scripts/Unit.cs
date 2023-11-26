using System;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public static event Action OnAnyActionPointsChanged;
    public static event EventHandler OnAnyUnitSpawned;
    public static event EventHandler OnAnyUnitDead;

    [SerializeField] private bool isEnemy;

    private const int ACTION_POINTS_MAX = 2;
    private GridPosition _gridPosition;
    private HealthSystem _healthSystem;

    private BaseAction[] _baseActionsArray;
    private int _actionPoints = 2;
    private void Awake()
    {
        _baseActionsArray = GetComponents<BaseAction>();
        _healthSystem = GetComponent<HealthSystem>();
    }

    private void Start()
    {
        _gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.AddUnitAtGridPosition(_gridPosition,this);
        
        TurnSystem.Instance.OnTurnChanged += OnTurnChanged;
        _healthSystem.OnDead += HealthSystemOnOnDead;
        
        OnAnyUnitSpawned?.Invoke(this,EventArgs.Empty);
    }

    private void Update()
    {
        GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        
        if (newGridPosition != _gridPosition)
        {
            GridPosition oldGridPosition = _gridPosition;
            _gridPosition = newGridPosition;
            LevelGrid.Instance.UnitMovedGridPosition(this,oldGridPosition,newGridPosition);
        }
    }


    public T GetAction<T>() where T : BaseAction
    {
        foreach (var action in _baseActionsArray)
        {
            if (action is T)
            {
                return (T)action;
            }
        }

        return null;
    }


    public GridPosition GetGridPosition()
    {
        return _gridPosition;
    }

    public Vector3 GetWorldPosition()
    {
        return transform.position;
    }
    public BaseAction[] GetBaseActionArray()
    {
        return _baseActionsArray;
    }

    public bool TrySpendActionPointsToTakeAction(BaseAction baseAction)
    {
        if (CanSpendActionPointsToTakeAction(baseAction))
        {
            SpendActionPoints(baseAction.GetActionPointsCost());
            return true;
        }

        return false;
    }
    public bool CanSpendActionPointsToTakeAction(BaseAction baseAction)
    {
        return _actionPoints >= baseAction.GetActionPointsCost();
    }

    private void SpendActionPoints(int amount)
    {
        _actionPoints -= amount;
        
        OnAnyActionPointsChanged?.Invoke();
    }

    public int GetActionPoints()
    {
        return _actionPoints;
    }
    
    private void OnTurnChanged()
    {
        if ((IsEnemy() && !TurnSystem.Instance.IsPlayerTurn()) ||
            (!IsEnemy() && TurnSystem.Instance.IsPlayerTurn())
           )
        {
            _actionPoints = ACTION_POINTS_MAX;
        
            OnAnyActionPointsChanged?.Invoke();
        }
    }

    public bool IsEnemy()
    {
        return isEnemy;
    }

    public void Damage(int damageAmount)
    {
        _healthSystem.Damage(damageAmount);
    }
    
    private void HealthSystemOnOnDead()
    {
        LevelGrid.Instance.RemoveUnitAtGridPosition(_gridPosition,this);
        OnAnyUnitDead?.Invoke(this,EventArgs.Empty);
        Destroy(gameObject);
    }

    public float GetHealthNormalized()
    {
        return _healthSystem.GetHealthNormalized();
    }
}
