using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlanetSpawner : MonoBehaviour
{
    public GameObject planetPrefab;
    public float spawnHeight = 10f;  // Distance above the last planet
    public float startSpawnHeight = 1f;
    public float rotationSpeedMultiplier = 2f;
    public int maxPlanets = 10;
    public int minScoreForPlanetDestroy = 4;
    public Sprite[] planetSprites;

    private List<GameObject> spawnedPlanets = new List<GameObject>();
    private Transform lastPlanet;
    public Transform planetParent;


    [SerializeField] GameObject meteor;
    [SerializeField] GameObject firstPlanet;
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
        if (scoreManager.score > lastCheckedScore && scoreManager.score > minScoreForPlanetDestroy)
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
        if (spawnedPlanets.Count < maxPlanets)
        {
            meteor.SetActive(false);
            firstPlanet.SetActive(false);
        }

        Vector2 spawnPosition = new Vector2(Random.Range(-2f, 2f), lastPlanet.position.y + spawnHeight);
        SpawnPlanet(spawnPosition);
    }

    void SpawnPlanet(Vector2 position)
    {
        GameObject newPlanet = Instantiate(planetPrefab, position, Quaternion.identity, planetParent); // Assign parent

        PlanetRotator rotator = newPlanet.AddComponent<PlanetRotator>();

        // Assign a random sprite
        SpriteRenderer sr = newPlanet.GetComponent<SpriteRenderer>();
        if (sr != null && planetSprites.Length > 0)
        {
            sr.sprite = planetSprites[Random.Range(0, planetSprites.Length)];
        }

        // Increase rotation speed slightly based on score
        float newRotationSpeed = rotator.rotationSpeed + (scoreManager.score * rotationSpeedMultiplier); // Adjust multiplier as needed
        rotator.SetRotationSpeed(newRotationSpeed);


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
