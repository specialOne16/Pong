using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerControl player1;
    public PlayerControl player2;
    public BallControl ball;
    public Trajectory trajectory;
    public BallControl fireBall;

    private Rigidbody2D rigidBodyPlayer1;
    private Rigidbody2D rigidBodyPlayer2;
    private Rigidbody2D rigidBodyBall;
    private CircleCollider2D ballCollider;

    private bool isDebugWindowShown = false;

    public int maxScore;

    public float timeUntilNextFireball = 100;
    private float remainingTimeUntilNextFireball;

    void Start()
    {
        rigidBodyPlayer1 = player1.GetComponent<Rigidbody2D>();
        rigidBodyPlayer2 = player2.GetComponent<Rigidbody2D>();
        rigidBodyBall= ball.GetComponent<Rigidbody2D>();
        ballCollider = ball.GetComponent<CircleCollider2D>();
        remainingTimeUntilNextFireball = timeUntilNextFireball;
        fireBall.gameObject.SetActive(false);
    }

    void Update()
    {
        remainingTimeUntilNextFireball -= Time.deltaTime;
        if(remainingTimeUntilNextFireball < 0)
        {
            remainingTimeUntilNextFireball = timeUntilNextFireball;
            throwFireBall();
        }
    }

    void throwFireBall()
    {
        fireBall.gameObject.SetActive(true);
        fireBall.SendMessage("RestartGame", null, SendMessageOptions.RequireReceiver);
    }

    void OnGUI()
    {
        GUI.Label(new Rect(Screen.width / 2 - 150 - 12, 20, 100, 100), player1.Score.ToString());
        GUI.Label(new Rect(Screen.width / 2 + 150 + 12, 20, 100, 100), player2.Score.ToString());
        if(GUI.Button(new Rect(Screen.width/2 - 60,35,120,53), "RESTART"))
        {
            player1.ResetScore();
            player2.ResetScore();
            ball.SendMessage("RestartGame", 0.5f, SendMessageOptions.RequireReceiver);
        }

        if (player1.Score == maxScore)
        {
            GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 10, 2000, 1000), "PLAYER ONE WINS");
            ball.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
        }
        if (player2.Score == maxScore)
        {
            GUI.Label(new Rect(Screen.width / 2 + 30, Screen.height / 2 - 10, 2000, 1000), "PLAYER TWO WINS");
            ball.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
        }
        
        if (GUI.Button(new Rect(Screen.width / 2 - 60, Screen.height - 73, 120, 53), "TOOGLE\nDEBUG INFO"))
        {
            isDebugWindowShown = !isDebugWindowShown;
            trajectory.enabled = !trajectory.enabled;
        }

        if (isDebugWindowShown)
        {
            Color oldCOlor = GUI.backgroundColor;
            GUI.backgroundColor = Color.red;

            float ballMass = rigidBodyBall.mass;
            Vector2 ballVelocity = rigidBodyBall.velocity;
            float ballSpeed = rigidBodyBall.velocity.magnitude;
            Vector2 ballMomentum = ballMass * ballVelocity;
            float ballFriction = ballCollider.friction;

            float impulsePlayer1X = player1.LastContactPoint.normalImpulse;
            float impulsePlayer1Y = player1.LastContactPoint.tangentImpulse;
            float impulsePlayer2X = player2.LastContactPoint.normalImpulse;
            float impulsePlayer2Y = player2.LastContactPoint.tangentImpulse;

            string debugText = 
                "Ball mass = " + ballMass + "\n" +
                "Ball velocity = " + ballVelocity + "\n" +
                "Ball speed = " + ballSpeed + "\n" +
                "Ball momentum = " + ballMomentum + "\n" +
                "Ball friction = " + ballFriction + "\n" +
                "Last impulse from P1 = " + "(" + impulsePlayer1X + "," + impulsePlayer1Y + ")" + "\n" +
                "Last impulse from P2 = " + "(" + impulsePlayer2X + "," + impulsePlayer2Y + ")";

            GUIStyle gUIStyle = new GUIStyle(GUI.skin.textArea);
            gUIStyle.alignment = TextAnchor.UpperCenter;
            GUI.TextArea(new Rect(Screen.width / 2 - 200, Screen.height - 200, 400, 110), debugText, gUIStyle);
            GUI.backgroundColor = oldCOlor;
        }
    }
}
