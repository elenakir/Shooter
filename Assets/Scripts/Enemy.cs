using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Zenject.FirstPersonShooter
{
    public class Enemy : MonoBehaviour, IDamage
    {
        public int Health { get { return _health; } set { _health = value; } }

        [Header("HealthLine")]
        [SerializeField] private Image _healthWholeLine;
        [SerializeField] private Image _healthRemainsLine;

        private int _health = 50;
        private Player _player;
        private WeaponSystem _weaponSystem;

        private float _distance;
        private float _force = 400f;
        private float _ratio = 10f;
        private float _radius = 5f;

        [Inject]
        public void Construct(
            Player player,
            WeaponSystem weaponSystem)
        {
            _player = player;
            _weaponSystem = weaponSystem;
        }

        private void Awake()
        {
            _healthRemainsLine.fillAmount = _health;
        }

        private void Update()
        {
            _healthWholeLine.transform.LookAt(_player.Transform);
        }

        public void ApplyDamage(int amount)
        {
            _health -= amount;
            _healthRemainsLine.fillAmount = _health / 50f;
            if (_health <= 0)
            {
                Kill();
            }
        }

        public void UpdateHealthLine()
        {
            _healthRemainsLine.fillAmount = _health / 50f;
        }

        private void Kill()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, _radius);
            foreach (Collider hit in colliders)
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();
                if (rb != null) 
                    rb.AddExplosionForce(_force, transform.position, _radius);

                //damage for other enemies
                Enemy enemy = hit.gameObject.GetComponent<Enemy>();
                if (enemy != null)
                { 
                    _distance = Vector3.Distance(hit.transform.position, transform.position);

                    if (_distance > 0)
                    {
                        enemy.Health -= Mathf.Clamp(Mathf.RoundToInt(_ratio / _distance), 1, 20);
                        enemy.UpdateHealthLine();
                        if (enemy.Health <= 0)
                            StartCoroutine(enemy.Explose(enemy.gameObject, .1f));
                    }
                }
            }
            StartCoroutine(Explose(gameObject, .1f));
        }

        IEnumerator Explose(GameObject destroyed, float delay)
        {
            yield return new WaitForSeconds(delay);
            GameObject go = Instantiate(_weaponSystem.Explosion, destroyed.transform.position, destroyed.transform.rotation);
            Destroy(destroyed);
            Destroy(go, 1.5f);

            if (_weaponSystem.CurrentWeapon.Bullet == Weapon.BulletType.Explosion)
            {
                _player.Points += 2;
            }
            else _player.Points++;
        }
    }
}
