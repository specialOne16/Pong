using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootCommand : Command
{
    PlayerShooting playerShooting;

    public ShootCommand(PlayerShooting playerShooting)
    {
        this.playerShooting = playerShooting;
    }

    public override void Execute()
    {
        playerShooting.Shoot();
    }

    public override void UnExecute()
    {

    }
}
