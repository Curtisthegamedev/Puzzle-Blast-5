using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsticleSpawing : MonoBehaviour
{
    [SerializeField] Obsticle myObsticlePrefab;
    private int obsticleAmountSize = 5;
    private float rateOfSpawn = 3.0f;
    private float yMin = -3.0f;
    private float yMax = 7.0f;
    Obsticle[] obsticles;
    private int currentObs = 0;
    [SerializeField] Transform spawnPos;
    private float timeSinceTheLastSpawn;
    private bool ObsticleJustSpawned = false;

    private void Start()
    {
        timeSinceTheLastSpawn = 0;
        obsticles = new Obsticle[obsticleAmountSize]; 
        for (int i = 0; i< obsticleAmountSize; i++)
        {
            obsticles[i] = Instantiate(myObsticlePrefab, spawnPos.transform.position, spawnPos.transform.rotation);
        } 
    }

    private void Update()
    {
        timeSinceTheLastSpawn += Time.deltaTime; 

        if(timeSinceTheLastSpawn > rateOfSpawn)
        {
            float spawnYPosition = Random.Range(yMin, yMax);
            obsticles[currentObs].transform.position = new Vector2(spawnPos.position.x, spawnYPosition);
            obsticles[currentObs].StartMoving();
            currentObs++;
            currentObs %= obsticleAmountSize;

            timeSinceTheLastSpawn = 0; 
        }
    }
}
