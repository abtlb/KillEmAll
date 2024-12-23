using System;
using ScriptableObjects.Enemies;
using UnityEngine;
using Weapons.EnemyWeapons;

namespace Enemies
{
    public class RandomDirectionEnemy : MonoBehaviour, IEnemy
    {
        [SerializeField] private EnemyData _enemyData;
        [SerializeField] private int moveSpeed;
        [SerializeField] private float changeDirInterval;
        private float nextChangeDirTime;
        Transform _playerTransform;
        [SerializeField] private float radiusTrigger;
        private bool playerDetected;
        private Ranged1 rangedAttacker;
        private Vector2 direction;
        private GameManager _gameManager;
        private AudioSource _audioSource;
        private Animator _animator;
        private bool _isDead;
        
        private void Awake()
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            _playerTransform = player.transform;
            CurrentHealth = MaxHealth = _enemyData.maxHealth;
            playerDetected = false;
            rangedAttacker = GetComponent<Ranged1>();
            direction = Vector2.zero;
            nextChangeDirTime = 0;
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
        
        public float CurrentHealth { get; private set; }
        public float MaxHealth { get; private set; }
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
                playerDetected = true;
                if(rangedAttacker)
                    rangedAttacker.isShooting = true;
            }

            if (!playerDetected)
            {
                return;
            }

            if (Time.time >= nextChangeDirTime)
            {
                direction = new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
                nextChangeDirTime = Time.time + changeDirInterval;
            }
             
            
            transform.position += (Vector3)(direction * (moveSpeed * Time.deltaTime));
        }
        
        private void OnDrawGizmosSelected()
        {
            // Visualize the detection radius in the editor
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, radiusTrigger);
        }
        
        private void Kill()
        {
            _gameManager.EnemyDied();
            _animator.Play("Die");
            _isDead = true;
            GetComponent<Collider2D>().enabled = false;
            GetComponent<IWeapon>().Stop();
            //Destroy(gameObject);
        }
        
        private void Rotate()
        {
            var direction = (Vector2)(_playerTransform.position - transform.position);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
