using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace TowerDefense
{
    public class LevelManagerScript : MonoBehaviour
    {
        public int fieldWidth; //ширина
        public int fieldHeight; //высота

        public GameObject cellPref;

        public Transform cellParent;

        public Sprite[] tileSprites = new Sprite[2];

        public List<GameObject> wayPoints = new List<GameObject>();
        GameObject[,] allCells = new GameObject[10, 22];

        public int currWayX;
        public int currWayY;
        GameObject firstCell;
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
            LoadWayPoints();
        }

        private void CreateLevel()
        {
            Vector3 worldVec = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));

            for (int i = 0; i < fieldWidth; i++)
            {
                for (int k = 0; k < fieldHeight; k++)
                {
                    int sprIndex = int.Parse(LoadLevelText(1)[i].ToCharArray()[k].ToString());
                    //int sprIndex = int.Parse(path[i].ToCharArray()[k].ToString());
                    Sprite spr = tileSprites[sprIndex];

                    bool isGround = spr == tileSprites[1] ? true : false;

                    CreateCell(isGround, spr, k, i, worldVec);
                }
            }
        }
        //

        private void CreateCell(bool isGroung, Sprite spr, int x, int y, Vector3 wV)
        {
            GameObject tmpCell = Instantiate(cellPref);
            tmpCell.transform.SetParent(cellParent, false);

            tmpCell.GetComponent<SpriteRenderer>().sprite = spr;

            float sprSizeX = tmpCell.GetComponent<SpriteRenderer>().bounds.size.x;
            float sprSizeY = tmpCell.GetComponent<SpriteRenderer>().bounds.size.y;
             
            tmpCell.transform.position = new Vector3(wV.x + (sprSizeX * x), wV.y + (sprSizeY * -y));
            
            if (isGroung)
            {
                tmpCell.GetComponent<CellScript>()._isGround = true;
                
                if (firstCell ==null)
                {
                    firstCell = tmpCell;
                    currWayX = x;
                    currWayY = y;
                }
            }
            
            allCells[y, x] = tmpCell;
        }

        private string[] LoadLevelText(int i)
        {
            TextAsset tmpText = Resources.Load<TextAsset>("Level" + i + "Ground");
            string tmpStr = tmpText.text.Replace(Environment.NewLine, string.Empty);
            return tmpStr.Split('!');
        }

        private void LoadWayPoints()
        {
            GameObject currWayGO;
            wayPoints.Add(firstCell);

            while(true)
            {
                currWayGO = null;

                if(currWayX > 0 && allCells[currWayY, currWayX -1].GetComponent<CellScript>()._isGround && !wayPoints.Exists(x=> x == allCells[currWayY, currWayX - 1]))
                {
                    currWayGO = allCells[currWayY, currWayX - 1];
                    currWayX--;
                    //Debug.Log("Next cell is left");
                }
                else if (currWayX < (fieldWidth - 1) && allCells[currWayY, currWayX + 1].GetComponent<CellScript>()._isGround && !wayPoints.Exists(x => x == allCells[currWayY, currWayX + 1]))
                {
                    currWayGO = allCells[currWayY, currWayX + 1];
                    currWayX++;
                    //Debug.Log("Next cell is right");
                }
                else if (currWayY > 0 && allCells[currWayY-1, currWayX].GetComponent<CellScript>()._isGround && !wayPoints.Exists(x => x == allCells[currWayY-1, currWayX]))
                {
                    currWayGO = allCells[currWayY-1, currWayX];
                    currWayY--;
                    //Debug.Log("Next cell is up");
                }
                else if (currWayY < (fieldHeight -1) && allCells[currWayY + 1, currWayX].GetComponent<CellScript>()._isGround && !wayPoints.Exists(x => x == allCells[currWayY + 1, currWayX]))
                {
                    currWayGO = allCells[currWayY + 1, currWayX];
                    currWayY++;
                    //Debug.Log("Next cell is down");
                }
                else
                    break;
                wayPoints.Add(currWayGO);
            }

        }
    }
}