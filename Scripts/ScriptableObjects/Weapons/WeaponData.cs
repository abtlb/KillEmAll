using UnityEngine;
using UnityEngine.Serialization;

namespace ScriptableObjects.Weapons
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/WeaponData")]
    public class WeaponData : ScriptableObject
    {
        [FormerlySerializedAs("fireRate")] public float fireDelay;
        public bool hasReload;
        public float reloadRate;
        public float reloadTime;
        public Sprite sprite;
        public Projectile projectilePrefab;
    }
}
