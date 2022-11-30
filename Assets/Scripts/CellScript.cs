using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class CellScript : MonoBehaviour
    {

        public int _state;
        public int _ID;
        public Color _normColor, _pathColor;

        public void SetState(int i)
        {
            _state = i;

            if (i == 0)
            {
                GetComponent<Image>().color = _normColor;
            }
            if (i == 1)
            {
                GetComponent<Image>().color = _pathColor;
            }
        }
    }
}