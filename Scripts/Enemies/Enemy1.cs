using System;
using ScriptableObjects.Enemies;
using ScriptableObjects.Weapons;
using UnityEngine;

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
        
        public float CurrentHealth { get; private set; }
        public float MaxHealth { get; private set; }
        
        private void Awake()
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            _playerTransform = player.transform;
            CurrentHealth = MaxHealth = _enemyData.maxHealth;
            GetComponent<SpriteRenderer>().sprite = _enemyData.sprite;
            movementNoise = UnityEngine.Random.Range(-1 * noiseThreshold, noiseThreshold);
        }

        private void FixedUpdate()
        {
            Move();
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
            Destroy(gameObject);
        }
    }
}
