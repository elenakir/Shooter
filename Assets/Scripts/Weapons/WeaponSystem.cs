using NUnit.Framework;
using System;
using UnityEngine;

namespace Zenject.FirstPersonShooter
{
    public class WeaponSystem : IInitializable, ITickable
    {
        [Serializable]
        public class Settings
        {
            public GameObject hitEffect;
            public GameObject explosionEffect;
            public GameObject Rocket;
            public Camera camera;
        }

        public Weapon CurrentWeapon { get { return _current; } set { _current = value; } }
        public GameObject Explosion => _settings.explosionEffect;

        [Inject]
        private Settings _settings = null;

        [Inject]
        private DiContainer _container;

        private Player _player;
        private Weapon _current;
        private Rigidbody rb;
        private ParticleSystem ps;
        private int _selectedWeaponIndex = 0;
        private int _previousWeaponIndex = 0;
        private int _ratio;

        public WeaponSystem(Settings settings, Player player)
        {
            _settings = settings;
            _player = player;
        }

        public void Initialize()
        {
            SelectWeapon();
        }

        public void Tick()
        {
            if (Input.GetButtonDown("Fire1") && _player.PlayerState != Player.State.Pause && !_current.Automatic)
            {
                Shoot();
            }
            else if (Input.GetButton("Fire1") && _player.PlayerState != Player.State.Pause && _current.Automatic)
            {
                Shoot();
                if (!ps.isPlaying)
                {
                    ps.Play();
                    var emission = ps.emission;
                    emission.rateOverTime = 10;
                }
            }
            else
            {
                if (ps != null)
                {
                    ps.Stop();
                    var emission = ps.emission;
                    emission.rateOverTime = 0;
                }
            }

            _previousWeaponIndex = _selectedWeaponIndex;

            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                if (_selectedWeaponIndex >= _player.WeaponsList.Count - 1)
                {
                    _selectedWeaponIndex = 0;
                }
                else _selectedWeaponIndex++;
            }

            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                if (_selectedWeaponIndex <= 0)
                {
                    _selectedWeaponIndex = _player.WeaponsList.Count - 1;
                }
                else _selectedWeaponIndex--;
            }

            if (_previousWeaponIndex != _selectedWeaponIndex)
            {
                SelectWeapon();
            }
        }

        private void Shoot()
        {
            if (Physics.Raycast(_player.MainCamera.transform.position, _player.MainCamera.transform.forward, out RaycastHit hit, _current.Range))
            {
                if (hit.transform.gameObject.layer == 9)
                {
                    if (_player.PlayerState == Player.State.Crouch) _ratio = 2;
                    else _ratio = 1;

                    if (_current.Bullet == Weapon.BulletType.Explosion)
                    {
                        int count = _current.Model.transform.childCount;
                        if (_current.Model.transform.childCount > 0)
                        {
                            rb = _current.Model.transform.GetChild(count - 1).gameObject.AddComponent<Rigidbody>();
                            rb.AddForce(_current.Model.transform.forward * 4000f, ForceMode.Acceleration);
                            GameObject.Destroy(rb.gameObject, 1f);
                            _container.InstantiatePrefab(_settings.Rocket, _current.Model.transform);
                        }
                        hit.rigidbody.AddForce(-hit.normal * 100f);
                    }

                    hit.transform.GetComponent<Enemy>().ApplyDamage(_current.Damage * _ratio);
                }

                if (_current.Bullet != Weapon.BulletType.Explosion || _current.Model.transform.childCount == 0)
                {
                    GameObject go = new GameObject();
                    GameObject particles = _container.InstantiatePrefab(_settings.hitEffect, hit.point, Quaternion.LookRotation(hit.normal), go.transform);
                    GameObject.Destroy(particles, 2.5f);
                    GameObject.Destroy(go, 2.5f);
                }
            }
        }

        private void SelectWeapon()
        {
            for (int i = 0; i < _player.WeaponsList.Count; i++)
            {
                if (i == _selectedWeaponIndex)
                {
                    _player.WeaponsList[i].Model.SetActive(true);
                    _current = _player.WeaponsList[i];
                }
                else
                {
                    _player.WeaponsList[i].Model.SetActive(false);
                }
            }

            if (_current.Model.GetComponentInChildren<ParticleSystem>() != null)
            {
                ps = _current.Model.GetComponentInChildren<ParticleSystem>();
            }
        }
    }
}
