using UnityEngine;

namespace Zenject.FirstPersonShooter
{
    public class InventoryCollider : MonoBehaviour
    {
        private Player _player;
        private InventoryHandler _handler;

        [Inject]
        public void Construct(
            Player player,
            InventoryHandler handler)
        {
            _player = player;
            _handler = handler;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("weapon"))
            {
                var weapon = collision.gameObject.GetComponent<WeaponSpawn>().SpawnWeapon;

                weapon.Model = Instantiate(weapon.Prefab, _player.MainCamera.transform);
                weapon.Model.SetActive(false);

                _player.WeaponsList.Add(weapon);
                _handler.UpdateInventory = true;

                Destroy(collision.gameObject);
            }
        }
    }
}
