using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleFactory : MonoBehaviour, IFactory
{
    [SerializeField]
    public GameObject[] collectiblePrefab;

    public GameObject FactoryMethod(int tag, Transform targetLocation)
    {
        GameObject collectible = Instantiate(collectiblePrefab[tag]);
        collectible.transform.position = targetLocation.position;
        return collectible;
    }
}
