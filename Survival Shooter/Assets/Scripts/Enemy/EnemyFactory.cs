using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour, IFactory
{
    [SerializeField]
    public GameObject[] enemyPrefab;

    public GameObject FactoryMethod(int tag, Transform targetLocation)
    {
        GameObject enemy = Instantiate(enemyPrefab[tag]);
        enemy.transform.position = targetLocation.position;
        return enemy;
    }
}
