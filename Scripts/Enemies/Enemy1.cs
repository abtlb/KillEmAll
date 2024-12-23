using System;
using ScriptableObjects.Enemies;
using ScriptableObjects.Weapons;
using UnityEngine;
using UnityEngine.UIElements;
using Weapons.EnemyWeapons;

namespace Enemies
{
    public class Enemy1 : MonoBehaviour, IEnemy
    {
        [SerializeField] private EnemyData _enemyData;
        [SerializeField] private int moveSpeed;
        [SerializeField] private float noiseThreshold;
        [SerializeField] private float noiseRadius;//at what radius is the noise turned off
        private float movementNoise;
        Transform _playerTransform;
        [SerializeField] private float radiusTrigger;
        private bool isChasing;
        private Ranged1 rangedAttacker;
        
        public float CurrentHealth { get; private set; }
        public float MaxHealth { get; private set; }
        
        private GameManager _gameManager;
        private Animator _animator;
        private bool _isDead;
        
        private void Awake()
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            _playerTransform = player.transform;
            CurrentHealth = MaxHealth = _enemyData.maxHealth;
            movementNoise = UnityEngine.Random.Range(-1 * noiseThreshold, noiseThreshold);
            isChasing = false;
            rangedAttacker = GetComponent<Ranged1>();
            _gameManager = FindAnyObjectByType<GameManager>();
            _animator = GetComponent<Animator>();
            _isDead = false;
        }

        private void FixedUpdate()
        {
            if (_isDead)
            {
                return;
            }
            Move();
            Rotate();
        }


        public void TakeDamage(int damage)
        {
            CurrentHealth -= damage;
            if (CurrentHealth <= 0)
            {
                Kill();
            }
        }

        public void Heal(int heal)
        {
            CurrentHealth = Math.Max(MaxHealth, CurrentHealth + heal);
        }

        public void Move()
        {
            float distanceToPlayer = Vector3.Distance(_playerTransform.position, transform.position);
            if (distanceToPlayer <= radiusTrigger)
            {
                isChasing = true;
                if(rangedAttacker != null)
                    rangedAttacker.isShooting = true;
            }

            if (!isChasing)
            {
                return;
            }
            
            Vector2 direction = (_playerTransform.position - transform.position);
            if (distanceToPlayer > noiseRadius)
            {
                float noiseAmount = Mathf.Min(distanceToPlayer, movementNoise);
            
                Vector2 noise = new Vector2(
                    movementNoise, movementNoise
                );
                direction += noise;
            }
            
            direction.Normalize();
            transform.position += (Vector3)(direction * (moveSpeed * Time.deltaTime));
        }

        private void Kill()
        {
            _isDead = true;
            _gameManager.EnemyDied();
            _animator.Play("Die");
            GetComponent<Collider2D>().enabled = false;
            GetComponent<IWeapon>().Stop();
            //Destroy(gameObject);
        }
        
        private void OnDrawGizmosSelected()
        {
            // Visualize the detection radius in the editor
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, radiusTrigger);
        }
        
        private void Rotate()
        {
            var direction = (Vector2)(_playerTransform.position - transform.position);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
