using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class Tower
    {
        public int type;
        public float _range;
        public float _cooldown = 2;
        public float _currentCooldown = 0;
        public Sprite _spr;

        //конструктор
        public Tower(int type, float range, float cd, string path)
        {
            this.type = type;
            this._range = range;
            _currentCooldown = cd;
            _spr = Resources.Load<Sprite>(path);
        }
    }

    public enum TowerType
    {
        First_Tower,
        Second_Tower
    }


    public class GameControllerScript : MonoBehaviour
    {
        public List<Tower> AllTowers = new List<Tower>();
        public List<TowerProjectile> AllProjectiles = new List<TowerProjectile>();
        public List<Enemy> AllEnemies = new List<Enemy>();

        private void Awake()
        {
            AllTowers.Add(new Tower(0, 3, 1, "TowerSprites/FTower"));
            AllTowers.Add(new Tower(1, 5, 3, "TowerSprites/STower"));

            AllProjectiles.Add(new TowerProjectile(7, 10, "ProjectileSprites/FProjectile"));
            AllProjectiles.Add(new TowerProjectile(5, 30, "ProjectileSprites/SProjectile"));

            AllEnemies.Add(new Enemy(30, 3, "EnemySprites/enemy1"));
            AllEnemies.Add(new Enemy(30, 3, "EnemySprites/enemy2"));
        }
    }

    public class TowerProjectile
    {
        public float _speed;
        public int _damage;
        public Sprite _sprite;

        public TowerProjectile(float speed, int damage, string path)
        {
            this._speed = speed;
            _damage = damage;
            _sprite = Resources.Load<Sprite>(path);
        }
    }

    public class Enemy
    {
        public float _health;
        public float _speed;
        public float _startSpeed;
        public Sprite _sprite;

        //Конструктор
        public Enemy(float health, float speed, string spritePath)
        {
            _health = health;
            _speed = speed;
            _startSpeed = speed;

            _sprite = Resources.Load<Sprite>(spritePath);
        }

        //Перегрузка конструктора
        public Enemy(Enemy other)
        {
            _health = other._health;
            _speed = other._startSpeed;
            _startSpeed = other._startSpeed;
            _sprite = other._sprite;
        }
    }
}