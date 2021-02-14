using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpeed : Collectibles
{
    PlayerMovement movement;
    public float amount = 3;
    
    void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        movement = player.GetComponent<PlayerMovement>();
    }


    public override void PickUp()
    {
        movement.IncreaseSpeed(amount);
    }

}