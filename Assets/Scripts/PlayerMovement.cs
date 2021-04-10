using System;
using UnityEngine;

namespace Zenject.FirstPersonShooter
{
    public class PlayerMovement : IInitializable, ITickable
    {
        [Serializable]
        public class Settings
        {
            public LayerMask groundLayerMask;
            public Transform groundCheck;
            public float groundDistance;
        }

        [Inject]
        private Settings _settings = null;

        [Inject]
        private DiContainer _container;

        private Player _player;
        private GameObject _groundCheck;
        private Vector3 _move;
        private Vector3 _velocity;
        private float _x, _z;
        private float _ratio;
        private bool _isOnTheGround;
        private bool _isCrouched;
        private readonly float _gravity = -9.8f;

        public PlayerMovement(Player player)
        {
            _player = player;
        }

        public void Initialize()
        {
            _groundCheck = _container.InstantiatePrefab(_settings.groundCheck, _player.Transform);
        }

        public void Tick()
        {
            if (_player.PlayerState != Player.State.Pause)
            {
                _x = Input.GetAxis("Horizontal");
                _z = Input.GetAxis("Vertical");

                _isOnTheGround = Physics.CheckSphere(_groundCheck.transform.position, _settings.groundDistance, _settings.groundLayerMask);
                _isCrouched = Input.GetKey(KeyCode.LeftControl);

                if (_isOnTheGround && _velocity.y < 0)
                {
                    _velocity.y = -2f;
                }

                if (Input.GetKey(KeyCode.LeftShift) && _isOnTheGround)
                {
                    _ratio = 2f;
                    _player.PlayerState = Player.State.Run;
                }
                else if (_isCrouched)
                {
                    _ratio = 0.5f;
                    _player.PlayerState = Player.State.Crouch;

                    _player.Body.localPosition = new Vector3(_player.Body.localPosition.x, -0.5f, _player.Body.localPosition.z);
                }
                else if (Input.GetButtonDown("Jump") && _isOnTheGround && !_isCrouched)
                {
                    _velocity.y = Mathf.Sqrt(_player.Options.JumpHeight * -2f * _gravity);
                }
                else
                {
                    _ratio = 1f;
                    _player.PlayerState = Player.State.Walk;

                    _velocity.y += _gravity * Time.deltaTime * 2f;
                    _player.Controller.Move(_velocity * Time.deltaTime);
                    _player.Body.localPosition = new Vector3(_player.Body.localPosition.x, 0, _player.Body.localPosition.z);
                }

                _move = _player.Legs.right * _x + _player.Legs.forward * _z;
                _player.Controller.Move(_move * _player.Options.PlayerSpeed * _ratio * Time.deltaTime);
            }
        }
    }
}
