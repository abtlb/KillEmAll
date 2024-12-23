using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour, IHealth
{
    [SerializeField] private float maxHealth;
    
    public event Action OnDeathEvent;
    private bool _isKilled;
    public Button tryAgain;
    public TMPro.TextMeshProUGUI healthText;

    private void Awake()
    {
        _isKilled = false;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MaxHealth = CurrentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = $"HP: {CurrentHealth.ToString()}";
    }

    public float CurrentHealth { get; private set; }
    public float MaxHealth { get; private set; }
    public void TakeDamage(int damage)
    {
        if (_isKilled)
        {
            return;
        }
        
        CurrentHealth -= damage;
        Debug.Log("HP: " + CurrentHealth);
        if (CurrentHealth <= 0)
        {
            Kill();
            _isKilled = true;
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
        GetComponent<Animator>().Play("PlayerDeath");
        tryAgain.gameObject.SetActive(true);
    }
}
