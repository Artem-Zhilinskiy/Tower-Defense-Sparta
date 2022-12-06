using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class TowerProjectileScript : MonoBehaviour
    {
        Transform _target;
        public TowerProjectile _selfProjectile;

        private void Start()
        {
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
                    _target.GetComponent<EnemyScript>().TakeDamage(_selfProjectile._damage);
                    Destroy(transform.gameObject);
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

    }
}
