using ScriptableObjects.Weapons;
using UnityEngine;

public class TheBMF : MonoBehaviour
{
    [SerializeField]
    private WeaponData weaponData;

    [SerializeField] private float noise;
    [SerializeField]
    private AudioSource gunSource;
    private bool isShooting;
    public Vector2 bulletOffset;
    
    public bool IsPlayer { get; private set; }
    public float TimeToShoot { get; private set; }
    private bool canShoot;

    
    void Awake()
    {
        IsPlayer = true;
        TimeToShoot = 0;
        canShoot = true;
        GetComponent<PlayerHealth>().OnDeathEvent += DisableShooting;
    }
    
    private void Update()
    {
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
        if (!canShoot)
        {
            return;
        }
        
        Projectile bullet = Instantiate(weaponData.projectilePrefab, (Vector2)transform.position + bulletOffset, Quaternion.identity);
        bullet.Setup(IsPlayer, GetShootingDirection().normalized);
        gunSource.Play();
    }
    
    private Vector2 GetShootingDirection()
    {
        var direction = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position;
        var noiseDir = new Vector2(UnityEngine.Random.Range(-1 * noise, noise), UnityEngine.Random.Range(-1 * noise, noise));
        
        return direction + noiseDir;
    }

    private void DisableShooting()
    {
        canShoot = false;
    }
}
