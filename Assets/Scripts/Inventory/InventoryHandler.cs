
namespace Zenject.FirstPersonShooter
{
    public class InventoryHandler : IInitializable
    {
        public bool UpdateInventory { get { return _update; } set { _update = value; } }

        [Inject]
        private DiContainer _container;

        private readonly InventoryInstaller.Settings _settings;
        private readonly Weapon.Factory _inventoryFactory;
        private readonly WeaponSystem _weaponSystem;
        private readonly Player _player;
        private bool _update;

        public InventoryHandler(
            InventoryInstaller.Settings settings,
            Weapon.Factory inventoryFactory,
            Player player,
            WeaponSystem weaponSystem)
        {
            _settings = settings;
            _inventoryFactory = inventoryFactory;
            _player = player;
            _weaponSystem = weaponSystem;
        }

        public void Initialize()
        {
            _update = true;

            for (int i = 0; i < _settings.startWeaponsList.Count; i++)
            {
                var go = _inventoryFactory.Create();
                go = _settings.startWeaponsList[i];
                go.Model = _container.InstantiatePrefab(go.Prefab);

                go.Model.transform.SetParent(_player.MainCamera.transform);
                go.Model.transform.localPosition = go.Prefab.transform.localPosition;
                go.Model.transform.localRotation = go.Prefab.transform.localRotation;

                if (i != 0) go.Model.SetActive(false);

                AddWeaponToInventory(go);
            }

            _weaponSystem.CurrentWeapon = _settings.startWeaponsList[0];
        }

        private void AddWeaponToInventory(Weapon weapon)
        {
            _player.WeaponsList.Add(weapon);
        }
    }
}
