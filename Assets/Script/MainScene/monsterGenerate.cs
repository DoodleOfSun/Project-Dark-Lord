using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterGenerate : MonoBehaviour
{
    public static monsterGenerate monsterGenerateInstance = null;

    private int poolSize = 3;

    // Level 1 Monster
    public GameObject level1Monster;
    private GameObject[] level1MonsterObjectPool;

    // Level 2 Monster

    private void Awake()
    {
        if (monsterGenerateInstance == null)
        {
            monsterGenerateInstance = this;
        }
    }

    void Start()
    {
        generateLevel1MonsterPool();
    }

    void Update()
    {
        
    }

    private void generateLevel1MonsterPool()
    {
        level1MonsterObjectPool = new GameObject[poolSize];

        for (int i = 0; i < poolSize; i++)
        {
            GameObject monsterObject = Instantiate(level1Monster);

            level1MonsterObjectPool[i] = monsterObject;

            monsterObject.SetActive(false);
        }
    }

    public void generateLevel1Monster(Vector3 posData)
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject monster = level1MonsterObjectPool[i];
            if (!monster.activeSelf)
            {
                monster.SetActive(true);

                monster.transform.position = posData;
                break;
            }
        }
    }
}