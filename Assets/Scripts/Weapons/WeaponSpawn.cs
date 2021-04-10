using UnityEngine;

namespace Zenject.FirstPersonShooter
{
    public class WeaponSpawn : MonoBehaviour
    {
        public Weapon SpawnWeapon => _weapon;

        [SerializeField] private Weapon _weapon;
    }
}
