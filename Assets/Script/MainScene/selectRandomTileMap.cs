using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class selectRandomTileMap : MonoBehaviour
{
    public GameObject[] breakableTileArray;
    public int breakableTileMapNumber;

    void Start()
    {
        int temp = Random.Range(0, breakableTileMapNumber);
        breakableTileArray[temp].SetActive(true);
    }
}
