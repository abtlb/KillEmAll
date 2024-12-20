using System;
using ScriptableObjects.Weapons;
using UnityEngine;

public class Mele1 : MonoBehaviour, IWeapon
{
    bool _isCollidingWithPlayer = false;
    PlayerHealth _playerHealth;
    [SerializeField] private WeaponData weaponData;
    [SerializeField] private bool isPlayer;

    private void Awake()
    {
        _playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        TimeToShoot = 0;
        IsPlayer = isPlayer;
    }

    private void FixedUpdate()
    {
        if (_isCollidingWithPlayer && Time.time > TimeToShoot)
        {
            Shoot();
            TimeToShoot = Time.time + weaponData.fireDelay;
        }
    }

    public void Shoot()
    {
        _playerHealth.TakeDamage(1);
    }

    public bool IsPlayer { get; private set; }
    public float TimeToShoot { get; private set; }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.CompareTag("Player"))
        {
            _isCollidingWithPlayer = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _isCollidingWithPlayer = false;
        }
    }
}
