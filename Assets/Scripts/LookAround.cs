using UnityEngine;

namespace Zenject.FirstPersonShooter
{
    public class LookAround : IInitializable, ITickable
    {
        private Player _player;
        private float _mouseX;
        private float _mouseY;
        private float xRotation = 0f;

        [Inject]
        public void Construct(Player player)
        {
            _player = player;
        }

        public void Initialize()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public void Tick()
        {
            if (_player.PlayerState != Player.State.Pause)
            {
                _mouseX = Input.GetAxis("Mouse X") * _player.Options.MouseSensibility * Time.deltaTime;
                _mouseY = Input.GetAxis("Mouse Y") * _player.Options.MouseSensibility * Time.deltaTime;

                xRotation -= _mouseY;
                xRotation = Mathf.Clamp(xRotation, -90f, 90f);

                _player.MainCamera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
                _player.Transform.Rotate(Vector3.up * _mouseX);
            }
        }
    }
}
