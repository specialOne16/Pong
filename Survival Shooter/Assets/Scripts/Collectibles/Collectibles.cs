using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectibles : MonoBehaviour
{

    public abstract void PickUp();
    public float timeBeforeDissappear = 7;
    float spawnTime;
    

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && !other.isTrigger)
        {
            PickUp();
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        spawnTime = Time.time;
    }

    private void Update()
    {
        if(spawnTime + timeBeforeDissappear < Time.time)
        {
            Destroy(gameObject);
        }
    }
}
