using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class Tower
    {
        public float _range;
        public float _cooldown = 2;
        public float _currentCooldown = 0;
        public Sprite _spr;

        //конструктор
        public Tower(float range, float cd, string path)
        {
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

        private void Awake()
        {
            AllTowers.Add(new Tower(3, 3, "TowerSprites/FTower"));
            AllTowers.Add(new Tower(5, 5, "TowerSprites/STower"));

            AllProjectiles.Add(new TowerProjectile(7, 10, "ProjectileSprites/FProjectile"));
            AllProjectiles.Add(new TowerProjectile(3, 30, "ProjectileSprites/SProjectile"));
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
}