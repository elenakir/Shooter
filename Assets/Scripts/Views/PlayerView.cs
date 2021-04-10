using TMPro;
using UnityEngine;

namespace Zenject.FirstPersonShooter
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _pointsCount;
        [SerializeField] private TextMeshProUGUI _bestResult;
        [SerializeField] private TextMeshProUGUI _weaponName;
        [SerializeField] private TextMeshProUGUI _weaponType;
        [SerializeField] private TextMeshProUGUI _bulletType;

        private Player _player;
        private WeaponSystem _weaponSystem;

        [Inject]
        public void Construct(
            Player player,
            WeaponSystem weaponSystem)
        {
            _player = player;
            _weaponSystem = weaponSystem;
        }

        void Update()
        {
            _pointsCount.text = _player.Points.ToString();
            _bestResult.text = _player.Options.BestResult.ToString();
            _weaponName.text = _weaponSystem.CurrentWeapon.name;
            if (_weaponSystem.CurrentWeapon.Automatic)
            {
                _weaponType.text = "Yes";
            } 
            else _weaponType.text = "No";
            _bulletType.text = _weaponSystem.CurrentWeapon.Bullet.ToString();
        }
    }
}
