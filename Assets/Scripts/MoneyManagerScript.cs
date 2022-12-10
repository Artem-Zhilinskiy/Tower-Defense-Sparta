using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class MoneyManagerScript : MonoBehaviour
    {
        public static MoneyManagerScript Instance;
        public Text _moneyText;
        public int _gameMoney;

        private void Awake()
        {
            Instance = this;
        }

        private void Update()
        {
            _moneyText.text = _gameMoney.ToString();
        }

    }
}