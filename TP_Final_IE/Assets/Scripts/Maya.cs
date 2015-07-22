using UnityEngine;
using System.Collections;

public class Maya 
    : MonoBehaviour 
{
    private Rigidbody2D body;
    private BoxCollider2D collider;
    private const float MAX_RAD = 100;
    private const float MAX_SPEED = 25;
    private float radLevel;
    private float speed;
    private bool isIdle;
    private bool isSafe;
    private bool canClimb;

	void Start () 
    {
        body = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        radLevel = 0;
        speed = 0;
        isSafe = false;
        canClimb = false;
        isIdle = true;
        
	}

    void FixedUpdate()
    {
        float dt = Time.deltaTime;
        if (!isSafe)
        {
            Irradiate();
        }
        else
        {
            Heal();
        }
        if (Input.GetKey("a"))
        {
            WalkLeft(dt);
        }
        else if (Input.GetKey("d"))
        {
            WalkRight(dt);
        }
        else if (Input.GetKey("w"))
        {
            if (canClimb)
            {
                Climb();
            }
        }
        else
        {
            isIdle = true;
        }

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Safe"))
        {
            isSafe = true;
        }
        if (other.CompareTag("Ladder"))
        {
            canClimb = true;
        }
        if (other.CompareTag("Bottle"))
        {
            radLevel = 0;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Safe"))
        {
            isSafe = false;
        }
    }

    private void Jump()
    {
    }

    private void WalkLeft(float deltaTime)
    {
    }

    private void WalkRight(float deltaTime)
    {
    }

    private void Climb()
    {
    }

    private void Irradiate()
    {
        
    }

    private void Heal()
    {

    }
}
