using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class EnemyRespawnerScript : MonoBehaviour
    {
        /*
        int wayIndex = 0;
        public int speed = 10;

        public float _timeToSpawn = 1;
        int _spawnCount = 0;

        private float _delay = 0.3f;
        private float _waveDelay = 3;

        [SerializeField]
        private GameObject _enemyPrefab;
        [SerializeField]
        private Text _spawnCountText;
        [SerializeField]
        GameControllerScript _gameControllerScript;

        private IEnumerator SpawnEnemy(int _enemyCount)
        {
            _spawnCount++;
            for (int i = 0; i < _enemyCount; i++)
            {
                GameObject _tmpEnemy = Instantiate(_enemyPrefab);
                _tmpEnemy.transform.SetParent(gameObject.transform, false);
                _tmpEnemy.GetComponent<EnemyScript>()._selfEnemy = new Enemy (_gameControllerScript.AllEnemies[Random.Range(0, _gameControllerScript.AllEnemies.Count)]);
                yield return new WaitForSeconds(_delay);
            }
        }

        private void Update()
        {
            if (_timeToSpawn <=0)
            {
                StartCoroutine(SpawnEnemy(_spawnCount + 1));
                _timeToSpawn = _waveDelay;
            }
            _timeToSpawn -= Time.deltaTime;
            //_spawnCountText.text = Mathf.Round(_timeToSpawn).ToString();
        }
        */

        public float _timeToSpawn = 1;
        int _spawnCount = 0;

        [SerializeField]
        private GameObject _enemyPrefab;

        private void Update()
        {
            if (_timeToSpawn <= 0)
            {
                StartCoroutine(SpawnEnemy(_spawnCount + 1));
                _timeToSpawn = 4;
            }
            _timeToSpawn -= Time.deltaTime;
        }

        private IEnumerator SpawnEnemy(int _enemyCount)
        {
            _spawnCount++;
            for (int i = 0; i < _enemyCount; i++)
            {
                GameObject _tmpEnemy = Instantiate(_enemyPrefab);
                _tmpEnemy.transform.SetParent(gameObject.transform, false);
                yield return new WaitForSeconds(0.3f);
            }
        }
    }
}