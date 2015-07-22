using UnityEngine;
using System.Collections;

public class Maya : MonoBehaviour 
{
    private Rigidbody2D body;
    private BoxCollider2D collider;

    private const float MAX_RAD = 100;
    private const float MAX_SPEED = 25;
    private float radLevel;
    private float speed;

 //   private const Vector3 LEFT(-1.0, 1, 0);
 //   private const Vector3 RIGHT(1.0, 1, 0);


    private bool isSafe;
    private bool canClimb;

	// Use this for initialization
	void Start () 
    {
        body = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        radLevel = 0;
        speed = 0;
        isSafe = false;
	}

    void FixedUpdate()
    {
        if (!isSafe)
        {
            radLevel++;
        }
        if (canClimb)
        {
            if (Input.GetKey("w"))
            {
                Climb();
            }
        }

        if (Input.GetKey("a"))
        {
            //Walk(LEFT);
        }
        if (Input.GetKey("d"))
        {
            //Walk(RIGHT);
        }

        

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.name == "Safe")
        {
            isSafe = true;
        }
        if (other.name == "Door")
        {
            canClimb = true;
        }
        if (other.name == "Bottle")
        {
            radLevel = 0;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Safe")
        {
            isSafe = false;
        }
    }


    private void Jump()
    {
        //body.AddForce();
    }
    private void Walk(Vector3 direction)
    {
        //body.
    }
    private void Climb()
    {
       
    }


	

}
