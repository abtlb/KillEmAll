using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealth
{
    [SerializeField] private float maxHealth;
    
    public event Action OnDeathEvent;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MaxHealth = CurrentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float CurrentHealth { get; private set; }
    public float MaxHealth { get; private set; }
    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        Debug.Log("HP: " + CurrentHealth);
        if (CurrentHealth <= 0)
        {
            Kill();
        }
    }

    public void Heal(int heal)
    {
        Debug.Log("HP: " + CurrentHealth);
        CurrentHealth = Math.Min(CurrentHealth + heal, MaxHealth);
    }

    public void Kill()
    {
        Debug.Log("Player is dead");
        OnDeathEvent?.Invoke();
    }
}
