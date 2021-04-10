using UnityEngine;
using UnityEngine.UI;

namespace Zenject.FirstPersonShooter
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField] private RectTransform _grid;

        private Player _player;
        private InventoryHandler _handler;

        [Inject]
        public void Construct(Player player,
            InventoryHandler handler)
        {
            _player = player;
            _handler = handler;
        }

        void Update()
        {
            if (_handler.UpdateInventory)
            {
                for (var i = _grid.transform.childCount; i-- > 0;)
                {
                    DestroyImmediate(_grid.transform.GetChild(0).gameObject);
                }

                foreach (var item in _player.WeaponsList)
                {
                    GameObject icon = new GameObject("Icon");
                    icon.AddComponent<Image>().sprite = item.Icon;
                    icon.transform.SetParent(_grid);
                    _handler.UpdateInventory = false;
                }
            }

        }
    }
}
