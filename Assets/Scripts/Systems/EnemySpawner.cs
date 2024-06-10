using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class EnemyPrefab
{
    public EnemySpawner.EnemyType enemyType;
    public GameObject prefab;
}

public class EnemySpawner : MonoBehaviour
{
    public enum EnemyType
    {
        Basic,
        Fast,
        Strong
    }
    
    private Dictionary<EnemySpawner.EnemyType, GameObject> enemyPrefabs;

    private void Awake()
    {
        enemyPrefabs = enemyPrefabsList.ToDictionary(item => item.enemyType, item => item.prefab);
    }
     
     
    [SerializeField] private List<EnemyPrefab> enemyPrefabsList = new List<EnemyPrefab>();
    [SerializeField] private float spawnRate = 1f;
    [SerializeField] private float spawnRadius = 1f;
    [SerializeField] private int maxEnemies = 10;
    [SerializeField] private int currentEnemies = 0;
    
    
    private void SpawnEnemy(EnemyType enemyType){
        if(currentEnemies < maxEnemies){
            Vector3 spawnPosition = transform.position + Random.insideUnitSphere * spawnRadius;
            spawnPosition = new Vector3(spawnPosition.x, 0, spawnPosition.z);
            Instantiate(enemyPrefabs[enemyType], spawnPosition, Quaternion.identity);
            currentEnemies++;
        }
    }

    public void GetEnemyType()
    {
        EnemyType enemyType = (EnemyType)Random.Range(0, 3);
        SpawnEnemy(enemyType);
    }
    
    private void Start()
    {
        InvokeRepeating("GetEnemyType", 0, spawnRate);
    }
}

