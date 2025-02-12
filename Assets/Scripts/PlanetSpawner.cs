using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlanetSpawner : MonoBehaviour
{
    public GameObject planetPrefab;
    public float spawnInterval = 2f; // Time between spawns
    public float spawnHeight = 10f;  // Distance above the last planet
    public float startSpawnHeight = 1f;

    private Transform lastPlanet;

    void Start()
    {
        SpawnPlanet(Vector2.up*startSpawnHeight); // Spawn first planet at (0,0)
        InvokeRepeating(nameof(SpawnNextPlanet), spawnInterval, spawnInterval);
    }

    void SpawnNextPlanet()
    {
        if (lastPlanet != null)
        {
            Vector2 spawnPosition = new Vector2(Random.Range(-2.5f, 2.5f), lastPlanet.position.y + spawnHeight);
            SpawnPlanet(spawnPosition);
        }
    }

    void SpawnPlanet(Vector2 position)
    {
        GameObject newPlanet = Instantiate(planetPrefab, position, Quaternion.identity);
        newPlanet.AddComponent<PlanetRotator>(); // Add rotation script to each new planet
        lastPlanet = newPlanet.transform;
    }
}
