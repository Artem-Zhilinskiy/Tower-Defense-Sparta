using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class GameControllerScript : MonoBehaviour
    {
        public int _cellCount;

        private int[] _pathID = {26, 27, 28,29,30,31,56,81,106,127,128,129,130,131,152,177,202,227,228,229,230,231,232,233,234,209,184,159,
            134,109,84,59,34,35,36,37,38,39,40,65,90,115,140,139,138,137,136,161,186,211,236,237,238,239,240,241,242,243,218,193,168,143,118,
            93,68,43,44,45,46,47,48,49,74,99,124,149,148,147,146,145,170,195,220,245,246,247,248,249,250};

        List<CellScript> _allCells = new List<CellScript>();

        public GameObject _cellPref;
        public Transform _cellGroup;

        private void Start()
        {
            CreateCells();
            CreatePath();
        }
        private void CreateCells()
        {
            for (int i = 0; i < _cellCount; i++)
            {
                GameObject _tempCell = Instantiate(_cellPref);
                _tempCell.transform.SetParent(_cellGroup, false);
                _tempCell.GetComponent<CellScript>()._ID = i + 1;
                _tempCell.GetComponent<CellScript>().SetState(0);
                _allCells.Add(_tempCell.GetComponent<CellScript>());
            }

        }

        private void CreatePath()
        {
            for (int i =0; i< _pathID.Length; i++)
            {
                _allCells[_pathID[i] - 1].SetState(1); 
            }
        }

    }
}