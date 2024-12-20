using UnityEngine;

public interface IHealth
{
    float CurrentHealth { get; }
    float MaxHealth { get; }
    
    public void TakeDamage(int damage);
    public void Heal(int heal);
}
