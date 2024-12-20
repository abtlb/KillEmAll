using System;
using ScriptableObjects;
using ScriptableObjects.Weapons;
using UnityEditorInternal;
using UnityEngine;

public class RegularWeapon : MonoBehaviour, IWeapon
{
    public bool IsPlayer { get; private set; }
    public float TimeToShoot { get; private set; }
    
    [SerializeField]
    private WeaponData weaponData;

    private void Awake()
    {
        IsPlayer = (gameObject.layer == LayerMask.NameToLayer("Player"));
        TimeToShoot = 0;
    }
    
    private void Update()
    {
        if (TimeToShoot <= Time.time)
        {
            Shoot();
            TimeToShoot = (int)(Time.time + weaponData.fireDelay);
        }
    }

    public void Shoot()
    {
        Projectile bullet = Instantiate(weaponData.projectilePrefab, transform.position, Quaternion.identity);
        bullet.Setup(IsPlayer, GetShootingDirection().normalized);
    }

    private Vector2 GetShootingDirection()
    {
        var direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        direction.z = 0;
        
        return (Vector2)direction;
    }
    
}
