using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject EnemyDrone;
    float maximumSpawnRate = 5f;

    private void Start()
    {
        Invoke("SpawnEnemy", maximumSpawnRate); 
    }
    private void SpawnEnemy()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        GameObject enemy = (GameObject)Instantiate(EnemyDrone);
        enemy.transform.position = new Vector2(Random.Range(min.y, max.y), max.x); 
    }
}
