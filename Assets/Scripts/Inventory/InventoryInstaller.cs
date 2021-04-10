using System;
using System.Collections.Generic;
using UnityEngine;

namespace Zenject.FirstPersonShooter
{
    public class InventoryInstaller : MonoInstaller
    {
        [Serializable]
        public class Settings
        {
            public List<Weapon> startWeaponsList;
        }

        public override void InstallBindings()
        {
            Container.BindFactory<Weapon, Weapon.Factory>();
            Container.BindInterfacesAndSelfTo<InventoryHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<WeaponSystem>().AsSingle();
        }
    }
}
