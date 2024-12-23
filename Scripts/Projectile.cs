using ScriptableObjects;
using ScriptableObjects.Projectiles;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private ProjectileData projectileData;
    [SerializeField] private Animation myAnimation;
    private bool _isSetup = false;
    private int deathTime;

    private void Awake()
    {
        myAnimation.clip = projectileData.AnimationClip;
        if (myAnimation.clip != null)
        {
            myAnimation.Play();
        }
        deathTime = (int)(Time.time + projectileData.lifeTime);
    }
    
    private void FixedUpdate()
    {
        if (!_isSetup)
        {
            return;
        }
        transform.Translate(Vector3.up * (projectileData.speed * Time.fixedDeltaTime));

        if (Time.time >= deathTime)
        {
            Kill();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") || 
            other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            other.GetComponent<IHealth>().TakeDamage((int)projectileData.damage);    
        }
        Destroy(gameObject);
    }

    public void Setup(bool isPlayer, Vector2 direction)
    {
        string layer = isPlayer ? "PlayerProjectile" : "EnemyProjectile";
        this.gameObject.layer = LayerMask.NameToLayer(layer);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90.0f;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        _isSetup = true;
    }

    private void Kill()
    {
        Destroy(gameObject);
    }
    
}
