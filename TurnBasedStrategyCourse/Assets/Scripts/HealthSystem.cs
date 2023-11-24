using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private int health = 100;
    private int _healthMax;
    
    public event Action OnDead;
    public event Action OnDamaged;

    private void Start()
    {
        _healthMax = health;
    }

    public void Damage(int damageAmount)
    {
        health -= damageAmount;

        if (health < 0)
        {
            health = 0;
        }

        OnDamaged?.Invoke();
        
        if (health == 0)
        {
            Die();
        }
    }

    private void Die()
    {
        OnDead?.Invoke();
    }

    public float GetHealthNormalized()
    {
        return (float)health / _healthMax;
    }
}
