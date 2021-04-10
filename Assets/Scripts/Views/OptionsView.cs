using UnityEngine;
using UnityEngine.UI;

namespace Zenject.FirstPersonShooter
{
    public class OptionsView : MonoBehaviour
    {
        [SerializeField] private Slider _mouseSensibilitySlider;
        [SerializeField] private Slider _playerSpeed;
        [SerializeField] private Slider _jumpHeight;
        [Space]
        [SerializeField] private Button _hideButton;

        private Player _player;

        [Inject]
        public void Construct(Player player)
        {
            _player = player;
        }

        void Awake()
        {
            _mouseSensibilitySlider.value = _player.MouseSensibility;
            _playerSpeed.value = _player.PlayerSpeed;
            _jumpHeight.value = _player.JumpHeight;

            _mouseSensibilitySlider.onValueChanged.AddListener(SetMouseSensibility);
            _playerSpeed.onValueChanged.AddListener(SetPlayerSpeed);
            _jumpHeight.onValueChanged.AddListener(SetJumpHeight);
        }


        private void SetMouseSensibility(float value)
        {
            _player.MouseSensibility = value;
        }

        private void SetPlayerSpeed(float value)
        {
            _player.PlayerSpeed = value;
        }

        private void SetJumpHeight(float value)
        {
            _player.JumpHeight = value;
        }
    }
}
