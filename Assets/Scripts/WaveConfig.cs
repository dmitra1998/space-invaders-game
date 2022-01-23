using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]//becomes available to create option which is used while creating a script or folder

public class WaveConfig : ScriptableObject
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float timeBetweenSpawns = 0.5f;
    [SerializeField] float spawnRandomFactor = 0.3f;
    [SerializeField] int numberofEnemies = 5;
    [SerializeField] float moveSpeed = 2f;

    public GameObject GetEnemyPrefab() { return enemyPrefab; }

    public List<Transform> GetWaypoints() 
    {
        var waveWaypoints = new List<Transform>();
        foreach (Transform child in pathPrefab.transform)   //Transform datatype child
        {
            waveWaypoints.Add(child);
        }

        return waveWaypoints; 
    }

    public float GetTimeBetweenSpawns() { return timeBetweenSpawns; }

    public float GetSpawnRandomFactor() { return spawnRandomFactor; }

    public int GetNumberofEnemies() { return numberofEnemies;  }

    public float GetMoveSpeed() { return moveSpeed; }

}
