using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class TowerScript : MonoBehaviour
    {
        [SerializeField]
        private GameObject _stonePrefab;
        [SerializeField]
        GameControllerScript _gameControllerScript;

        Tower _selfTower;

        [SerializeField]
        TowerType selfType;

        private void Start()
        {
            _gameControllerScript = FindObjectOfType<GameControllerScript>();
            _selfTower = _gameControllerScript.AllTowers[(int)selfType];
            GetComponent<SpriteRenderer>().sprite = _selfTower._spr;
        }
        private void Update()
        {
            if (CanShoot())
            {
                SearchTarget();
            }

            if (_selfTower._currentCooldown > 0)
            {
                _selfTower._currentCooldown -= Time.deltaTime;
            }
        }

        private bool CanShoot()
        {
            if (_selfTower._currentCooldown <= 0)
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
                    _currentDistance <= _selfTower._range)
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
            _selfTower._currentCooldown = _selfTower._cooldown;

            GameObject _stone = Instantiate(_stonePrefab);
            _stone.GetComponent<TowerProjectileScript>()._selfProjectile = _gameControllerScript.AllProjectiles[(int)selfType];
            _stone.transform.position = transform.position;
            _stone.GetComponent<TowerProjectileScript>().SetTarget(_enemy);
        }
    }
}