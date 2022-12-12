using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class EnemyScript : MonoBehaviour
    {
        List<GameObject> wayPoints = new List<GameObject>();

        //Переменная для поиска GameControllerScript через камеру
        [SerializeField]
        private GameObject _mainCamera;

        public Enemy _selfEnemy;

        Vector2 _direction;

        int _wayIndex = 0;
        public int speed = 10;

        private void Start()
        {
            //_wayPoints = _mainCamera.GetComponent<GameControllerScript>()._wayPoints;
            StartCoroutine(MoveCoroutine());
            GetWayPoints();
            //GetComponent<SpriteRenderer>().sprite = _selfEnemy._sprite;
        }

        private void GetWayPoints()
        {
            wayPoints = GameObject.Find("LevelGroup").GetComponent<LevelManagerScript>().wayPoints;
        }

        public void TakeDamage(float damage)
        {
            _selfEnemy._health -= damage;
            CheckIsAlive();
        }

        private void CheckIsAlive()
        {
            if (_selfEnemy._health <= 0)
            {
                Destroy(transform.gameObject);
            }
        }

        private IEnumerator MoveCoroutine()
        {
            while (transform.position.x < 1140)
            {
                yield return Time.deltaTime;
                _direction = wayPoints[_wayIndex].transform.position - transform.position;
                transform.Translate(_direction.normalized * _selfEnemy._speed * Time.deltaTime);
                if (Vector3.Distance(transform.position, wayPoints[_wayIndex].transform.position) < 0.05f)
                {
                    if (_wayIndex < wayPoints.Count - 1)
                    {
                        _wayIndex++;
                    }
                    else
                    {
                        Destroy(transform.gameObject);
                    }
                }
            }
        }

        public void StartSlow(float duration, float slowValue)
        {
            StopCoroutine("GetSlow");
            _selfEnemy._speed = _selfEnemy._startSpeed;
            StartCoroutine(GetSlow(duration, slowValue));
        }

        IEnumerator GetSlow(float duration, float slowValue)
        {
            _selfEnemy._speed -= slowValue;
            yield return new WaitForSeconds(duration);
            _selfEnemy._speed = _selfEnemy._startSpeed;
        }

        public void AOEDamage(float range, float damage)
        {
            List<EnemyScript> _enemies = new List<EnemyScript>();
            foreach (GameObject _gameObject in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                if (Vector2.Distance(transform.position, _gameObject.transform.position) <+ range)
                {
                    _enemies.Add(_gameObject.GetComponent<EnemyScript>());
                }
                foreach(EnemyScript _enemyScript in _enemies)
                {
                    _enemyScript.TakeDamage(damage);
                }
            }
        }
    }
}