using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusHealth : Collectibles
{
    PlayerHealth health;
    public int amount = 15;

    void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        health = player.GetComponent<PlayerHealth>();
    }

    public override void PickUp()
    {
        health.IncreaseHealth(amount);
    }
}
