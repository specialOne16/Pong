using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideWall : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    public PlayerControl player;
    public BallControl ball;

    void OnTriggerEnter2D(Collider2D collideObject)
    {
        if (collideObject.name == "ball")
        {
            givePointToEnemy();
        }
        if (collideObject.name == "fireBall")
        {
            collideObject.gameObject.SetActive(false);
        }
    }

    public void givePointToEnemy()
    {
        player.AddScore();
        if (player.Score < gameManager.maxScore)
        {
            ball.SendMessage("RestartGame", 2f, SendMessageOptions.RequireReceiver);
        }
    } 

}
