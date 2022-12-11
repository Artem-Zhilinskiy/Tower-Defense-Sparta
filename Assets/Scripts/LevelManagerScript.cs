using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace TowerDefense
{
    public class LevelManagerScript : MonoBehaviour
    {
        public int fieldWidth; //ширина
        public int fieldHeiht; //высота

        public GameObject cellPref;

        public Transform cellParent;

        public Sprite[] tileSprites = new Sprite[2];
        /*
        string[] path = {"0000000000000000000000",
                         "1111101111110111111000",
                         "0000101000010100001000",
                         "0000101000010100001000",
                         "0111101011110101111000",
                         "0100001010000101000000",
                         "0100001010000101000000",
                         "0100001010000101000000",
                         "0111111011111101111111",
                         "0000000000000000000000",
                };
        */

        private void Start()
        {
            CreateLevel();
        }

        private void CreateLevel()
        {
            Vector3 worldVec = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));

            for (int i = 0; i < fieldWidth; i++)
            {
                for (int k = 0; k < fieldHeiht; k++)
                {
                    int sprIndex = int.Parse(LoadLevelText(1)[i].ToCharArray()[k].ToString());
                    //int sprIndex = int.Parse(path[i].ToCharArray()[k].ToString());
                    Sprite spr = tileSprites[sprIndex];

                    CreateCell(spr, k, i, worldVec);
                }
            }
        }
        //

        private void CreateCell(Sprite spr, int x, int y, Vector3 wV)
        {
            GameObject tmpCell = Instantiate(cellPref);
            tmpCell.transform.SetParent(cellParent, false);

            tmpCell.GetComponent<SpriteRenderer>().sprite = spr;

            float sprSizeX = tmpCell.GetComponent<SpriteRenderer>().bounds.size.x;
            float sprSizeY = tmpCell.GetComponent<SpriteRenderer>().bounds.size.y;
             
            tmpCell.transform.position = new Vector3(wV.x + (sprSizeX * x), wV.y + (sprSizeY * -y));
        }

        private string[] LoadLevelText(int i)
        {
            TextAsset tmpText = Resources.Load<TextAsset>("Level" + i + "Ground");
            string tmpStr = tmpText.text.Replace(Environment.NewLine, string.Empty);
            return tmpStr.Split('!');
        }
    }
}