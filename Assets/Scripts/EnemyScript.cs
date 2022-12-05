using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class EnemyScript : MonoBehaviour
    {
        public GameObject _wayPointsParent;

        List<GameObject> _wayPoints = new List<GameObject>();

        //Переменная для поиска GameControllerScript через камеру
        [SerializeField]
        private GameObject _mainCamera;

        int _wayIndex = 0;
        Vector3 _dir;
        int _speed = 5;
        int _health = 30;

        private void Start()
        {
            //_wayPoints = _mainCamera.GetComponent<GameControllerScript>()._wayPoints;
            StartCoroutine(MoveCoroutine());
            GetWayPoints();
        }

        private void GetWayPoints()
        {
            for (int i = 0; i < _wayPointsParent.transform.childCount; i++)
            {
                _wayPoints.Add(_wayPointsParent.transform.GetChild(i).gameObject);
            }
        }

        public void TakeDamage(int damage)
        {
            _health -= damage;
            CheckIsAlive();
        }

        private void CheckIsAlive()
        {
            if (_health <= 0)
            {
                Destroy(transform.gameObject);
            }
        }

        private IEnumerator MoveCoroutine()
        {
            while (transform.position.x < 1140)
            {
                yield return Time.deltaTime;
                _dir = _wayPoints[_wayIndex].transform.position - transform.position;
                transform.Translate(_dir.normalized * _speed * Time.deltaTime);
                if (Vector3.Distance(transform.position, _wayPoints[_wayIndex].transform.position) < 0.05f)
                {
                    if (_wayIndex < _wayPoints.Count - 1)
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
    }
}