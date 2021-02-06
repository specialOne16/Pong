using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public KeyCode upButton = KeyCode.W;
    public KeyCode downButton = KeyCode.S;
    public float speed = 10f;
    public float yBoundary = 9f;
    public SideWall mySideWall;
    private Rigidbody2D rigidbody2D;
    private int score;
    private ContactPoint2D lastContactPoint;

    public float powerUpDuration = 10;
    private float remainingPowerUpDuration = 0;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();    
    }

    void Update()
    {
        Vector2 velocity = rigidbody2D.velocity;
        if (Input.GetKey(upButton)) velocity.y = speed;
        else if (Input.GetKey(downButton)) velocity.y = -speed;
        else velocity.y = 0;
        rigidbody2D.velocity = velocity;

        Vector3 position = rigidbody2D.position;
        position.y = Mathf.Clamp(position.y, -yBoundary, yBoundary);
        rigidbody2D.position = position;

        if(remainingPowerUpDuration > 0)
        {
            remainingPowerUpDuration -= Time.deltaTime;
            if(remainingPowerUpDuration <= 0)
            {
                transform.localScale = new Vector2(1,1);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("powerUp"))
        {
            remainingPowerUpDuration = powerUpDuration;
            transform.localScale = new Vector2(1, 2);
            collision.gameObject.SetActive(false);
        }
        if (collision.gameObject.name.Equals("fireBall"))
        {
            mySideWall.givePointToEnemy();
            collision.gameObject.SetActive(false);
        }
        if (collision.gameObject.name.Equals("ball"))
        {
            lastContactPoint = collision.GetContact(0);
        }
    }

    public void AddScore()
    {
        score++;
    }

    public void ResetScore()
    {
        score = 0;
    }

    public int Score
    {
        get
        {
            return score;
        }
    }
    public ContactPoint2D LastContactPoint
    {
        get { return lastContactPoint; }
    }
}
