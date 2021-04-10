using UnityEngine;

namespace Zenject.FirstPersonShooter
{
    [CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
    public class Weapon : ScriptableObjectInstaller
    {
        public string Name;
        public GameObject Prefab;
        public Sprite Icon;
        public int Damage;
        public int Range;
        public BulletType Bullet;
        public bool Automatic;

        [HideInInspector]
        public GameObject Model;

        public enum BulletType
        {
            Simple,
            Explosion
        }

        public class Factory : PlaceholderFactory<Weapon>
        {
        }
    }
}
