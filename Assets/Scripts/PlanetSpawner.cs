using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlanetSpawner : MonoBehaviour
{
    public GameObject planetPrefab;
    public Transform player;
    public float spawnDistance = 10f;
    public int maxPlanets = 5;
    private Queue<GameObject> planets = new Queue<GameObject>();

    void Start()
    {
        for (int i = 0; i < maxPlanets; i++)
        {
            SpawnPlanet(i * spawnDistance);
        }
    }

    void Update()
    {
        if (player.position.y > planets.Peek().transform.position.y + spawnDistance)
        {
            RecyclePlanet();
        }
    }

    void SpawnPlanet(float yOffset)
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-2f, 2f), yOffset, 0);
        GameObject newPlanet = Instantiate(planetPrefab, spawnPosition, Quaternion.identity);
        planets.Enqueue(newPlanet);
    }

    void RecyclePlanet()
    {
        GameObject oldPlanet = planets.Dequeue();
        float newY = planets.Last().transform.position.y + spawnDistance;
        oldPlanet.transform.position = new Vector3(Random.Range(-2f, 2f), newY, 0);
        planets.Enqueue(oldPlanet);
    }
}
