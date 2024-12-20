using ScriptableObjects.Weapons;
using UnityEngine;

namespace ScriptableObjects.Enemies
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
    public class EnemyData : ScriptableObject
    {
        public int maxHealth;
        public Sprite sprite;
        public AnimationClip moveAnimation;
        public WeaponData weaponData;
    }
}
