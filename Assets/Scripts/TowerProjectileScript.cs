using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class TowerProjectileScript : MonoBehaviour
    {
        Transform _target;
        TowerProjectile _selfProjectile;
        public Tower _selfTower;
        GameControllerScript _gameControllerScript;

        private void Start()
        {
            _gameControllerScript = FindObjectOfType<GameControllerScript>();
            _selfProjectile = _gameControllerScript.AllProjectiles[_selfTower.type];
            GetComponent<SpriteRenderer>().sprite = _selfProjectile._sprite;
        }

        public void SetTarget(Transform enemy)
        {
            _target = enemy;
        }

        private void Move()
        {
            if (_target != null)
            {
                if (Vector2.Distance(transform.position, _target.position) < 0.1f)
                {
                    Hit();
                }
                else
                {
                    Vector2 _direction = _target.position - transform.position;
                    transform.Translate(_direction.normalized * Time.deltaTime * _selfProjectile._speed);
                }
            }
            else
            {
                Destroy(transform.gameObject);
            }
        }

        private void Update()
        {
            Move();
        }

        private void Hit()
        {
            switch(_selfTower.type)
            {
                case (int)TowerType.First_Tower:
                    _target.GetComponent<EnemyScript>().StartSlow(3, 1);
                    _target.GetComponent<EnemyScript>().TakeDamage(_selfProjectile._damage);
                    break;
                case (int)TowerType.Second_Tower:
                    _target.GetComponent<EnemyScript>().AOEDamage(2, _selfProjectile._damage);
                    break;
            }
            Destroy(transform.gameObject);
        }

    }
}
