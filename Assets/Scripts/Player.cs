using System.Collections.Generic;
using UnityEngine;

namespace Zenject.FirstPersonShooter
{
    public class Player
    {
        public Transform Transform => _transform;
        public Transform Body => _body.transform;
        public Transform Legs => _legs.transform;
        public Camera MainCamera => _camera;
        public CharacterController Controller => _controller;
        public List<Weapon> WeaponsList => _weaponsList;
        public Options Options => _options;
        public float MouseSensibility { get { return _options.MouseSensibility; } set { _options.MouseSensibility = value; } }
        public float PlayerSpeed { get { return _options.PlayerSpeed; } set { _options.PlayerSpeed = value; } }
        public float JumpHeight { get { return _options.JumpHeight; } set { _options.JumpHeight = value; } }
        public State PlayerState { get { return _state; } set { _state = value; } }
        public int Points { get { return _points; } set { _points = value; } }

        public enum State
        {
            Walk,
            Run,
            Crouch,
            Pause
        }

        public Player(
            Transform PlayerTransform,
            Transform body,
            Transform legs,
            Camera mainCamera,
            CharacterController controller,
            Options options)
        {
            _transform = PlayerTransform;
            _body = body;
            _legs = legs;
            _camera = mainCamera;
            _controller = controller;
            _options = options;
            _weaponsList = new List<Weapon>();
        }

        private CharacterController _controller;
        private List<Weapon> _weaponsList;
        private Transform _transform;
        private Transform _body;
        private Transform _legs;
        private Options _options;
        private Camera _camera;
        private State _state;
        private int _points;
    }
}
