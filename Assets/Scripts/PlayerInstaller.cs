using System;
using UnityEngine;

namespace Zenject.FirstPersonShooter
{
    public class PlayerInstaller : MonoInstaller
    {
        [Serializable]
        public class Settings
        {
            public Transform PlayerTransform;
            public Transform Body;
            public Transform Legs;
            public Camera MainCamera;
            public CharacterController Controller;
            public Options GameOptions;
        }

        [SerializeField] private Settings _settings;

        public override void InstallBindings()
        {
            Container.Bind<Player>().AsSingle()
                .WithArguments(_settings.PlayerTransform, _settings.Body, _settings.Legs, _settings.MainCamera, _settings.Controller, _settings.GameOptions);
            Container.BindInterfacesAndSelfTo<LookAround>().AsSingle();
            Container.BindInterfacesTo<PlayerMovement>().AsSingle();
        }
    }
}
