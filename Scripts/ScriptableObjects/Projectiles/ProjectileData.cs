using UnityEngine;

namespace ScriptableObjects.Projectiles
{
    [CreateAssetMenu(fileName = "ProjectileData", menuName = "Scriptable Objects/ProjectileData")]
    public class ProjectileData : ScriptableObject
    {
        public float speed;
        public float damage;
        public Sprite sprite;
        public float lifeTime;
        public AnimationClip AnimationClip;
    }
}