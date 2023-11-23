using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnSystemUI : MonoBehaviour
{
    [SerializeField] private Button endTurnButton;
    [SerializeField] private TextMeshProUGUI turnNumberTextMesh;
    [SerializeField] private GameObject enemyTurnVisualGO;


    private void Start()
    {
        AssignButton();
        UpdateText();
        UpdateEnemyTurnVisual();
        UpdateEndTurnButtonVisibility();
        TurnSystem.Instance.OnTurnChanged += EndTurn;
    }

    private void AssignButton()
    {
        endTurnButton.onClick.AddListener(()=> TurnSystem.Instance.NextTurn());
    }

    private void EndTurn()
    {
        UpdateEnemyTurnVisual();
        UpdateEndTurnButtonVisibility();
        UpdateText();
    }
    
    private void UpdateText()
    {
        turnNumberTextMesh.text = "TURN " + TurnSystem.Instance.GetTurnNumber();
    }

    private void UpdateEnemyTurnVisual()
    {
        enemyTurnVisualGO.SetActive(!TurnSystem.Instance.IsPlayerTurn());
    }

    private void UpdateEndTurnButtonVisibility()
    {
        endTurnButton.gameObject.SetActive(TurnSystem.Instance.IsPlayerTurn());
    }
}
