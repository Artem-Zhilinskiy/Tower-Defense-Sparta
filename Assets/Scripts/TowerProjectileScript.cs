using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class TowerProjectileScript : MonoBehaviour
    {

        float _speed = 10;
        int _damage = 10;

        Transform _target;

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
                    _target.GetComponent<EnemyScript>().TakeDamage(_damage);
                    Destroy(transform.gameObject);
                }
                else
                {
                    Vector2 _direction = _target.position - transform.position;
                    transform.Translate(_direction.normalized * Time.deltaTime * _speed);
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
