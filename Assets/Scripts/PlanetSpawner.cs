using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlanetSpawner : MonoBehaviour
{
    public GameObject planetPrefab;
    public float spawnHeight = 10f;  // Distance above the last planet
    public float startSpawnHeight = 1f;
    public int maxPlanets = 10;

    private List<GameObject> spawnedPlanets = new List<GameObject>();
    private Transform lastPlanet;
    public Transform planetParent;

    [SerializeField] private ScoreManager scoreManager;
    private int lastCheckedScore = 0;

    void Start()
    {
        // Spawn the initial 10 planets immediately
        for (int i = 0; i < maxPlanets; i++)
        {
            Vector2 spawnPosition = new Vector2(Random.Range(-2f, 2f), startSpawnHeight + (i * spawnHeight));
            SpawnPlanet(spawnPosition);
        }
    }

    void Update()
    {
        if (scoreManager.score > lastCheckedScore && scoreManager.score > 3)
        {
            lastCheckedScore = scoreManager.score;
            ManagePlanets();
        }
    }
    
    void ManagePlanets()
    {
        if (spawnedPlanets.Count >= maxPlanets)
        {
            DestroyOldestPlanet();
        }

        Vector2 spawnPosition = new Vector2(Random.Range(-2f, 2f), lastPlanet.position.y + spawnHeight);
        SpawnPlanet(spawnPosition);
    }

    void SpawnPlanet(Vector2 position)
    {
        GameObject newPlanet = Instantiate(planetPrefab, position, Quaternion.identity, planetParent); // Assign parent
        newPlanet.AddComponent<PlanetRotator>(); // Add rotation script
        spawnedPlanets.Add(newPlanet);
        lastPlanet = newPlanet.transform;
    }
    void DestroyOldestPlanet()
    {
        if (spawnedPlanets.Count > 0)
        {
            GameObject oldestPlanet = spawnedPlanets[0];
            spawnedPlanets.RemoveAt(0);
            Destroy(oldestPlanet);
        }
    }
}
