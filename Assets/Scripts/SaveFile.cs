using System;
using UnityEngine;

namespace Zenject.FirstPersonShooter
{
    public class SaveFile : MonoBehaviour
    {
        [Serializable]
        public class PlayerInfo
        {
            public float PlayerSpeed;
            public float MouseSensibility;
            public float JumpHeight;
            public int BestResult;
            public Vector3 PlayerPosition;
            public Quaternion PlayerRotation;
        }

        private string _path;
        private PlayerInfo _info;
        private Player _player;

        [Inject]
        public void Construct(Player player)
        {
            _player = player;
        }

        private void Awake()
        {
            try
            {
                _path = System.IO.File.ReadAllText(Application.dataPath + "/Resources/Save.json");
            }
            catch (Exception)
            {
                _path = Resources.Load<TextAsset>("Save").ToString();
            }

            _info = JsonUtility.FromJson<PlayerInfo>(_path);

            _player.Options.PlayerSpeed = _info.PlayerSpeed;
            _player.Options.MouseSensibility = _info.MouseSensibility;
            _player.Options.JumpHeight = _info.JumpHeight;
            _player.Options.BestResult = _info.BestResult;
            _player.Options.PlayerPosition = _info.PlayerPosition;
            _player.Options.PlayerRotation = _info.PlayerRotation;
        }

        private void OnDestroy()
        {
            if (_player.Options.BestResult < _player.Points)
            {
                _player.Options.BestResult = _player.Points;
            }
            _player.Options.PlayerPosition = transform.position;
            _player.Options.PlayerRotation = transform.rotation;

            _info.PlayerSpeed = _player.Options.PlayerSpeed;
            _info.MouseSensibility = _player.Options.MouseSensibility;
            _info.JumpHeight = _player.Options.JumpHeight;
            _info.BestResult = _player.Options.BestResult;
            _info.PlayerPosition = _player.Options.PlayerPosition;
            _info.PlayerRotation = _player.Options.PlayerRotation;

            string text = JsonUtility.ToJson(_info, true);
            System.IO.File.WriteAllText(Application.dataPath + "/Resources/Save.json", text);
        }
    }
}