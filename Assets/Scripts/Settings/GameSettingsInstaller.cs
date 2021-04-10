using System;
using UnityEngine;

namespace Zenject.FirstPersonShooter
{
    [CreateAssetMenu(fileName = "New Options", menuName = "GameSettingsInstaller")]
    public class GameSettingsInstaller : ScriptableObjectInstaller
    {
        public WeaponSystemSettings WeaponSystem;
        public InventorySettings InventorySystem;
        public PlayerMovementSettings PlayerMovementSystem;

        [Serializable]
        public class WeaponSystemSettings
        {
            public WeaponSystem.Settings Settings;
        }

        [Serializable]
        public class InventorySettings
        {
            public InventoryInstaller.Settings Settings;
        }

        [Serializable]
        public class PlayerMovementSettings
        {
            public PlayerMovement.Settings Settings;
        }

        public override void InstallBindings()
        {
            Container.BindInstance(WeaponSystem.Settings);
            Container.BindInstance(InventorySystem.Settings);
            Container.BindInstance(PlayerMovementSystem.Settings);
        }
    }
}

