using System;
using ScriptableObjects;
using ScriptableObjects.Weapons;
using UnityEditorInternal;
using UnityEngine;

public class RegularWeapon : MonoBehaviour
{
    public bool IsPlayer { get; private set; }
    public float TimeToShoot { get; private set; }
    [SerializeField]
    AudioSource audioSource;
    
    [SerializeField]
    private WeaponData weaponData;

    private bool canShoot;
    
    private bool isShooting;

    private void Awake()
    {
        IsPlayer = (gameObject.layer == LayerMask.NameToLayer("Player"));
        TimeToShoot = 0;
        canShoot = true;
        GetComponent<PlayerHealth>().OnDeathEvent += DisableShooting;
    }
    
    private void Update()
    {
        if (!canShoot)
        {
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TimeToShoot = 0;
            isShooting = true;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isShooting = false;
        }
        
        if (isShooting && TimeToShoot <= Time.time)
        {
            Shoot();
            TimeToShoot = Time.time + weaponData.fireDelay;
        }
    }

    public void Shoot()
    {
        Projectile bullet = Instantiate(weaponData.projectilePrefab, transform.position, Quaternion.identity);
        bullet.Setup(IsPlayer, GetShootingDirection().normalized);
        audioSource.Play();
    }

    private Vector2 GetShootingDirection()
    {
        var direction = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position;
        
        return direction;
    }

    private void DisableShooting()
    {
        canShoot = false;
    }
    
}
