using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class TowerScript : MonoBehaviour
    {
        private float _range = 2;

        public float _currentCooldown = 1;
        public float _cooldown = 0.5f;

        [SerializeField]
        private GameObject _stonePrefab;

        private void Update()
        {
            if (CanShoot())
            {
                SearchTarget();
            }

            if (_currentCooldown > 0)
            {
                _currentCooldown -= Time.deltaTime;
            }
        }

        private bool CanShoot()
        {
            if (_currentCooldown <= 0)
            {
                return true;
            }
            return false;
        }

        private void SearchTarget()
        {
            Transform _nearestEnemy = null;
            float _nearestEnemyDistance = Mathf.Infinity;

            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                float _currentDistance = Vector2.Distance(transform.position, enemy.transform.position);

                if (_currentDistance < _nearestEnemyDistance &&
                    _currentDistance <= _range)
                {
                    _nearestEnemy = enemy.transform;
                    _nearestEnemyDistance = _currentDistance;
                } 
            }

            if (_nearestEnemy != null)
            {
                Shoot(_nearestEnemy);
            }

        }

        private void Shoot(Transform _enemy)
        {
            _currentCooldown = _cooldown;

            GameObject _stone = Instantiate(_stonePrefab);
            _stone.transform.position = transform.position;
            _stone.GetComponent<TowerProjectileScript>().SetTarget(_enemy);
        }
    }
}