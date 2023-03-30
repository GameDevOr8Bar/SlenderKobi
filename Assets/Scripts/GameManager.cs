using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   private static GameManager instance;

    public static GameManager Instance { get; private set; }

    public int pgaeCounter = 0;
    public GameObject[] spawnLocations = new GameObject[15];
    public int currentSpawnIndex = Random.Range(0, 15);

    public GameObject PagePrefab;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    int ChooseSpawnLocation()
    {
        int index = currentSpawnIndex;
        while (index == currentSpawnIndex)
        {
            index = Random.Range(0, spawnLocations.Length);
        }

        return index;
    }

    void SpawnPage()
    {
       currentSpawnIndex = ChooseSpawnLocation();

       if (currentSpawnIndex > spawnLocations.Length || currentSpawnIndex < 0)
       {
            return;
       }

        Instantiate(PagePrefab, spawnLocations[currentSpawnIndex].transform);
    }
}
