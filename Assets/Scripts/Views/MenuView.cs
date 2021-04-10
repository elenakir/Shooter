using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Zenject.FirstPersonShooter
{
    public class MenuView : MonoBehaviour
    {
        [SerializeField] private GameObject _blockPanel;
        [SerializeField] private GameObject _menuPanel;
        [Space]
        [Header("Top Menu Buttons")]
        [SerializeField] private Button _optionsButton;
        [SerializeField] private Button _backToGameButton;
        [SerializeField] private Button _menuButton;
        [Space]
        [Header("Animators")]
        [SerializeField] private Animator _topMenuAnimator;
        [SerializeField] private Animator _optionsAnimator;
        [SerializeField] private Animator _mainMenuAnimator;
        [Space]
        [Header("Options buttons")]
        [SerializeField] private Button _hideOptionsButton;
        [Header("Menu buttons")]
        [SerializeField] private Button _startGameButton;
        [SerializeField] private Button _optButton;
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _quitButton;

        private Player _player;
        private bool menuActive;

        [Inject]
        public void Construct(Player player)
        {
            _player = player;
        }

        void Awake()
        {
            _blockPanel.SetActive(false);
            _menuPanel.SetActive(false);
            _optionsAnimator.speed = 0;
            _topMenuAnimator.speed = 0;
            _mainMenuAnimator.speed = 0;

            _optionsButton.onClick.AddListener(() =>
            {
                _optionsAnimator.speed = 1;
                _optionsAnimator.SetBool("isOpened", false);
                _topMenuAnimator.SetBool("isOpened", true);
            });

            _backToGameButton.onClick.AddListener(() =>
            {
                menuActive = false;
                _blockPanel.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                _player.PlayerState = Player.State.Walk;
            });

            _menuButton.onClick.AddListener(() =>
            {
                _menuPanel.SetActive(true);
                _mainMenuAnimator.speed = 1;
                _mainMenuAnimator.SetBool("isOpened", false);
                _topMenuAnimator.SetBool("isOpened", true);
            });

            _hideOptionsButton.onClick.AddListener(() =>
            {
                _optionsAnimator.SetBool("isOpened", true);
                _topMenuAnimator.SetBool("isOpened", false);
            });

            _startGameButton.onClick.AddListener(() => { SceneManager.LoadScene(0); });

            _backButton.onClick.AddListener(() =>
            {
                _mainMenuAnimator.SetBool("isOpened", true);
                _topMenuAnimator.SetBool("isOpened", false);
            });

            _optButton.onClick.AddListener(() =>
            {
                _optionsAnimator.speed = 1;
                _optionsAnimator.SetBool("isOpened", false);
                _mainMenuAnimator.SetBool("isOpened", true);
            });

            _quitButton.onClick.AddListener(() => { Application.Quit(); });
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !menuActive)
            {
                menuActive = true;
                _player.PlayerState = Player.State.Pause;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                _blockPanel.SetActive(true);
                _optionsAnimator.speed = 0;
                _mainMenuAnimator.speed = 0;
                _topMenuAnimator.speed = 1;
                _topMenuAnimator.SetBool("isOpened", false);
            }
        }
    }
}
