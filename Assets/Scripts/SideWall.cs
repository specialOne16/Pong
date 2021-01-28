using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideWall : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    public PlayerControl player;

    void OnTriggerEnter2D(Collider2D collideObject)
    {
        if(collideObject.name == "ball")
        {
            player.AddScore();
        }

        if(player.Score < gameManager.maxScore)
        {
            collideObject.gameObject.SendMessage("RestartGame", 2f, SendMessageOptions.RequireReceiver);
        }

    }

}
