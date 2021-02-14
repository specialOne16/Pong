using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public float spawnTime = 4f;
    public Transform[] spawnPoints;
    int lastRespawnPoint = -1;

    [SerializeField]
    MonoBehaviour factory;
    IFactory Factory
    {
        get
        {
            return factory as IFactory;
        }
    }


    void Start()
    {
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }


    void Spawn()
    {
        if (playerHealth.currentHealth <= 0f)
        {
            return;
        }

        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        if(spawnPointIndex == lastRespawnPoint)
        {
            lastRespawnPoint = -1;
            return;
        }

        lastRespawnPoint = spawnPointIndex;

        int spawnCollectible = Random.Range(0, 2);
        Factory.FactoryMethod(spawnCollectible, spawnPoints[spawnPointIndex]);

    }
}
