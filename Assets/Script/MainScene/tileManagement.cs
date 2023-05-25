using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class tileData
{
    private string tileName;
    private int tileResource1;
    private int tileResource2;

    public tileData (string tileName)
    {
        this.tileName = tileName;

        if (tileName == "Breakable1")
        {
            this.tileResource1 = 10;
            this.tileResource2 = 0;
        }

        //else if.... Breakable234

        else
        {
            this.tileResource1 = 0;
            this.tileResource2 = 0;
        }

    }

    public string getTileName()
    {
        return this.tileName;
    }

    public int getResource1()
    {
        return this.tileResource1;
    }

    public int getResource2()
    {
        return this.tileResource2;
    }
}

public class tileManagement : MonoBehaviour
{
    public Dictionary<Vector3Int, tileData> tileInfoDictionary;
    public Grid grid;
    public Tilemap tileMapBreakable;

    public GameObject tileInfoUI;
    public Text tileInfoResource1;
    public Text tileInfoResource2;
    public Text tileInfoTileName;

    private Vector3 mouseWorldPosition;
    private Vector3Int mouseGridPosition;
    private Vector3 realPosOfmouseGridPosition;

    public GameObject level1Monster;

    private bool isUIOpen;

    // Start is called before the first frame update
    void Start()
    {
        assignInitialValue();
    }

    // Update is called once per frame
    void Update()
    {
        updatingMousePosition();
        tileControlByMouseClick();
    }
    private void assignInitialValue()
    {
        isUIOpen = false;
        tileInfoDictionary = new Dictionary<Vector3Int, tileData>();

        foreach (Vector3Int pos in tileMapBreakable.cellBounds.allPositionsWithin)
        {
            if (!tileMapBreakable.HasTile(pos))
            {
                continue;
            }
            var tile = tileMapBreakable.GetTile<TileBase>(pos);
            tileInfoDictionary[pos] = new tileData(tile.name.ToString());
        }
    }

    private void updatingMousePosition()
    {
        mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseGridPosition = grid.WorldToCell(mouseWorldPosition);
        realPosOfmouseGridPosition = mouseGridPosition;
        realPosOfmouseGridPosition.x += 0.5f;
        realPosOfmouseGridPosition.y += 0.5f;

    }

    private void tileControlByMouseClick()
    {
        if (!tileMapBreakable.HasTile(mouseGridPosition))
        {
            // Empty
        }

        // 아무데나 우클릭 시 UI창 닫음
        else if (Input.GetMouseButtonDown(1) && isUIOpen)
        {
            tileInfoUI.SetActive(false);
            isUIOpen = false;
        }

        else
        {
            if (tileInfoDictionary[mouseGridPosition].getTileName() != "Breakable0" &&
                tileInfoDictionary[mouseGridPosition].getTileName() != "Breakable1")
            {
                // Empty
            }
            else
            {
                if (Input.GetMouseButtonDown(0) && !checkingAroundTilesShut())
                {
                    if (tileInfoDictionary[mouseGridPosition].getResource1() >= 0 && tileInfoDictionary[mouseGridPosition].getResource1() < 10 &&
                        tileInfoDictionary[mouseGridPosition].getResource2() >= 0 && tileInfoDictionary[mouseGridPosition].getResource2() < 10)
                    {
                        // Disable tile
                        tileMapBreakable.SetTile(mouseGridPosition, null);
                    }

                    
                    // Level 1 Monster
                    else if (tileInfoDictionary[mouseGridPosition].getResource1() >= 10)
                    {
                        monsterGenerate.monsterGenerateInstance.generateLevel1Monster(realPosOfmouseGridPosition);
                        // Disable tile
                        tileMapBreakable.SetTile(mouseGridPosition, null);
                    }
                    

                    tileInfoUI.SetActive(false);
                    isUIOpen = false;
                }

                else if (Input.GetMouseButtonDown(1) && !isUIOpen)
                {
                    tileInfoUISetting(mouseGridPosition);
                    

                    tileInfoUI.transform.position = realPosOfmouseGridPosition;
                    Debug.Log(tileInfoUI.transform.position);
                    tileInfoUI.SetActive(true);
                    isUIOpen = true;
                }
            }
        }
    }

    private bool checkingAroundTilesShut()
    {
        Vector3Int tempUp = mouseGridPosition;
        tempUp.y += 1;

        Vector3Int tempDown = mouseGridPosition;
        tempDown.y += -1;

        Vector3Int tempLeft = mouseGridPosition;
        tempLeft.x += -1;

        Vector3Int tempRight = mouseGridPosition;
        tempRight.x += 1;

        if (tileMapBreakable.HasTile(tempUp) &&
            tileMapBreakable.HasTile(tempDown) &&
            tileMapBreakable.HasTile(tempLeft) &&
            tileMapBreakable.HasTile(tempRight))
        {
            Debug.Log(mouseGridPosition);
            Debug.Log(tempUp);
            Debug.Log(tempDown);
            Debug.Log(tempLeft);
            Debug.Log(tempRight);
            return true;
        }

        else
        {
            return false;
        }
        
    }

    private void tileInfoUISetting(Vector3Int vector3IntData)
    {
        tileInfoResource1.text = "Resource 1 : " + tileInfoDictionary[vector3IntData].getResource1().ToString();
        tileInfoResource2.text = "Resource 2 : " + tileInfoDictionary[vector3IntData].getResource2().ToString();
        tileInfoTileName.text = tileInfoDictionary[vector3IntData].getTileName().ToString();
    }
}