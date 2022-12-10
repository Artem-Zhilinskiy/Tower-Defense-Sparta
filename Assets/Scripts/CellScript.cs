using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class CellScript : MonoBehaviour
    {
        public bool _isGround;
        public bool _hasTower = false;

        public Color _baseColor;
        public Color _currentColor;

        private void OnMouseEnter()
        {
            if (!_isGround)
            {
                Debug.Log("Is something going on?");
                GetComponent<SpriteRenderer>().color = _currentColor;
            }
        }

        private void OnMouseExit()
        {
            GetComponent<SpriteRenderer>().color = _baseColor;
        }
    }
}