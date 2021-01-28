using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;

    public float initialForce;
    public float maxYInitialForce;

    private Vector2 trajectoryOrigin;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        trajectoryOrigin = transform.position;
        RestartGame();
    }

    void Update()
    {
        
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        trajectoryOrigin = transform.position;
    }

    public Vector2 TrajectoryOrigin
    {
        get { return trajectoryOrigin; }
    }

    void PushBall()
    {
        float yRandomInitialForce = Random.Range(-maxYInitialForce, maxYInitialForce);
        float randomDirection = Random.Range(0, 2);
        float xInitialForce = Mathf.Sqrt(Mathf.Pow(initialForce,2) - Mathf.Pow(yRandomInitialForce,2));
        if (randomDirection < 1f) rigidbody2D.AddForce(new Vector2(-xInitialForce, yRandomInitialForce));
        else rigidbody2D.AddForce(new Vector2(xInitialForce, yRandomInitialForce));
    }

    public void RestartGame()
    {
        ResetBall();
        Invoke("PushBall", 2);
    }

    void ResetBall()
    {
        transform.position = Vector2.zero;
        rigidbody2D.velocity = Vector2.zero;
    }
}
