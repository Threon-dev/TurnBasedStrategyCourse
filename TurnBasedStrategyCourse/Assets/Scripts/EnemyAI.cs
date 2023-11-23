using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private float timer;

    private void Start()
    {
        TurnSystem.Instance.OnTurnChanged += OnTurnChanged;
    }
    private void Update()
    {
        if(TurnSystem.Instance.IsPlayerTurn()) return;
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            TurnSystem.Instance.NextTurn();
        }
    }
    
    private void OnTurnChanged()
    {
        timer = 2f;
    }
}
