using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemyPrefabs;
    public List<BoxCollider> spawnAreas;
    public int numberOfEnemies = 1;
    public float spawnInterval = 10f;
    public float spawnDelay = 0f;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(spawnDelay);
        while (true)
        {
            for (int i = 0; i < numberOfEnemies; i++)
            {
                SpawnRandomEnemy();
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnRandomEnemy()
    {
        GameObject selectedEnemyPrefab = GetRandomEnemyPrefab();
        BoxCollider selectedArea = GetRandomSpawnArea();
        Vector3 spawnPosition = GetRandomPositionWithinArea(selectedArea);
        Instantiate(selectedEnemyPrefab, spawnPosition, Quaternion.identity);
    }

    private GameObject GetRandomEnemyPrefab()
    {
        int randomIndex = Random.Range(0, enemyPrefabs.Count);
        return enemyPrefabs[randomIndex];
    }

    private BoxCollider GetRandomSpawnArea()
    {
        int randomIndex = Random.Range(0, spawnAreas.Count);
        return spawnAreas[randomIndex];
    }

    private Vector3 GetRandomPositionWithinArea(BoxCollider area)
    {
        Vector3 center = area.bounds.center;
        Vector3 size = area.bounds.size;

        float randomX = Random.Range(center.x - size.x / 2, center.x + size.x / 2);
        float randomY = Random.Range(center.y - size.y / 2, center.y + size.y / 2);
        float randomZ = Random.Range(center.z - size.z / 2, center.z + size.z / 2);

        return new Vector3(randomX, randomY, randomZ);
    }
}