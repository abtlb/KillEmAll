using ScriptableObjects.Weapons;
using UnityEngine;

namespace Weapons.EnemyWeapons
{
    public class Ranged1 : MonoBehaviour, IWeapon
    {
        public float TimeToShoot { get; private set; }
        public void Stop()
        {
            canShoot = false;
        }

        private bool isPlayer;
        public bool isShooting;
    
        [SerializeField]
        private WeaponData weaponData;
        private AudioSource audioSource;
        private bool canShoot;

        private void Awake()
        {
            TimeToShoot = 0;
            isPlayer = false;
            isShooting = false;
            audioSource = GetComponent<AudioSource>();
            canShoot = true;
        }
    
        private void Update()
        {
            if (canShoot && isShooting && TimeToShoot <= Time.time)
            {
                Shoot();
                TimeToShoot = Time.time + weaponData.fireDelay;
            }
        }

        public void Shoot()
        {
            Projectile bullet = Instantiate(weaponData.projectilePrefab, transform.position, Quaternion.identity);
            bullet.Setup(isPlayer, GetShootingDirection().normalized);
            audioSource.Play();
        }

        public bool IsPlayer { get; }

        private Vector2 GetShootingDirection()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            var direction = player.transform.position - transform.position;
            direction.z = 0;
        
            return (Vector2)direction;
        }
    }
}
