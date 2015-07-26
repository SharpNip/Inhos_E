using UnityEngine;
using System.Collections;

public class Maya 
    : MonoBehaviour 
{
	// Status Variables
	private bool isIdle;
	private bool isSafe;
	private bool canClimb;
	private bool onGround;
	private float radLevel;

	// Movement variables
	private const float MAX_RAD = 100;
	private const float MAX_SPD = 10;
    private const float INCREMENTATION = 3;
	private float jumpHeight;
	private float speed;
	private float climbSpeed;
    private Vector2 direction;
    

	// Body components
	private Rigidbody2D body;

	// Collider Tags
	private const string GROUND = "Ground";
	private const string SAFE_AREA = "Safe";
	private const string SPIKES = "Spikes";
	private const string DANGER_AREA = "RadBarrel";
	private const string LADDER = "Ladder";
	private const string SDPTA = "Bottle";

	void Start () 
    {
		//Initialiasation of body components
        body = GetComponent<Rigidbody2D>();
        //Setting all status variables
		radLevel = 0;
        speed = 0;
		climbSpeed = .01f;
		jumpHeight = 3;
        isSafe = false;
        canClimb = false;
        isIdle = true;
		onGround = true;
	}

	void Update()
	{
		
		ManageInput();
        HandleStates();
    }

	private void HandleStates()
	{
		if (!isSafe)
		{
			Irradiate();
		}
		else
		{
			Heal();
		}

       

	}

	private void ManageInput()
	{
		if (Input.GetKey(KeyCode.A))
		{
            isIdle = false;
            if (speed > MAX_SPD)
            {
                speed = MAX_SPD;
            }
            else
            {
                speed += INCREMENTATION;
                direction = -Vector2.right;
                WalkLeft();
            }
		}
        else if (Input.GetKey(KeyCode.D))
        {
            isIdle = false;
            if (speed > MAX_SPD)
            {
                speed = MAX_SPD;
            }
            else
            {
                speed += INCREMENTATION;
                direction = Vector2.right;
                WalkRight();
            }

        }
        else
        {
            isIdle = true;

            if (speed > 0 && onGround)
            {
                body.velocity -= body.velocity / INCREMENTATION;
                speed -= INCREMENTATION;
            }
        }
		if (Input.GetKey(KeyCode.W))
		{
            isIdle = false;
			if(canClimb)
            {
                direction = Vector2.up;
                Climb();   
            }
			else
			{
				Jump();
			}
		}
		
	}
    
	void OnCollisionStay2D(Collision2D col)
	{
        if (!(col.contacts[0].normal == Vector2.up))
        {
            onGround = false;
        }
        else
        {
            onGround = true;
        }

        foreach (ContactPoint2D contact in col.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.white);
        }
	}
	void OnCollisionEnter2D(Collision2D collider)
	{
		if (collider.gameObject.CompareTag (SPIKES)) 
		{
			Debug.Log("You're Dead!!");
		}
	}

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag(SAFE_AREA))
        {
            isSafe = true;
		}
		if (other.CompareTag (DANGER_AREA)) 
		{
            
		}
        if(other.CompareTag(LADDER))
        {
            if (isIdle && canClimb)
            {
                body.velocity = Vector2.zero;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(SAFE_AREA))
        {
            isSafe = false;
        }
        if (other.CompareTag(LADDER))
        {
            canClimb = false;
            body.gravityScale = 1;
        }
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(SAFE_AREA))
        {
            isSafe = true;
        }
        if (other.CompareTag(LADDER))
        {
            canClimb = true;
            body.gravityScale = 0;
        }
        if (other.CompareTag(SDPTA))
        {
            radLevel = 0;
        }
    }

    private void Jump()
    {
		if(onGround)
		{
			body.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
			onGround = false;
		}
    }

    private void WalkRight()
    {
      	Vector2 movement = direction * speed * Time.deltaTime;
		
        body.velocity += movement;

        if (body.velocity.x > MAX_SPD)
        {
            body.velocity -= movement;
        }
    }
    private void WalkLeft()
    {
        Vector2 movement = direction * speed * Time.deltaTime;
        body.velocity += movement;
        if (body.velocity.x < -MAX_SPD)
        {
            body.velocity -= movement;
        }
    }
    private void Climb()
    {
        body.position = body.position + (direction * climbSpeed); 
 
    }

    private void Irradiate()
    {
        
    }

    private void Heal()
    {

    }
}
