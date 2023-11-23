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


    private void Start()
    {
        AssignButton();
        UpdateText();
    }

    private void AssignButton()
    {
        endTurnButton.onClick.AddListener(EndTurn);
    }

    private void EndTurn()
    {
        TurnSystem.Instance.NextTurn();
        UpdateText();
    }
    
    private void UpdateText()
    {
        turnNumberTextMesh.text = "TURN " + TurnSystem.Instance.GetTurnNumber();
    }
}
