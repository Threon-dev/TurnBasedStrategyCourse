using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TurnSystem : MonoBehaviour
{
    public static TurnSystem Instance { get; private set; }

    public Action OnTurnChanged;
    
    private int _turnNumber = 1;

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

    public void NextTurn()
    {
        _turnNumber++;
        
        OnTurnChanged?.Invoke();
    }

    public int GetTurnNumber()
    {
        return _turnNumber;
    }
}
