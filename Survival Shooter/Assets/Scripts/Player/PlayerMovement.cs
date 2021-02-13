using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;

    int floorMask;
    Vector3 movement;
    Animator anim;
    Rigidbody playerRigidBody;
    PlayerHealth health;
    float camRayLength = 100f;

    private void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
        anim = GetComponent<Animator>();
        playerRigidBody = GetComponent<Rigidbody>();
        health = GetComponent<PlayerHealth>();
    }

    private void FixedUpdate()
    { 
        Turning();
    }

    public void Move(float h, float v)
    {
        if (health.currentHealth < 0) return;
        movement.Set(h, 0, v);
        movement = movement.normalized * speed * Time.deltaTime;
        playerRigidBody.MovePosition(transform.position + movement);
    }

    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;

        if(Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0;
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerRigidBody.MoveRotation(newRotation);
        }
    }

    public void Animating(float h, float v)
    {
        bool isWalking = h != 0f || v != 0f;
        anim.SetBool("IsWalking", isWalking);
    }
}
