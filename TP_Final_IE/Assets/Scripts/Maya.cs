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
	private const float MAX_SPD = 3;
	private const float MAX_CLIMBSPD = 2;
	private Vector2 direction;
	private float jumpHeight;
	private float speed;
	private float climbSpeed;

	// Body components
	private Rigidbody2D body;
	private BoxCollider2D collider;

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
        collider = GetComponent<BoxCollider2D>();
        //Setting all status variables
		radLevel = 0;
        speed = 0;
		climbSpeed = 0;
		jumpHeight = 3;
        isSafe = false;
        canClimb = false;
        isIdle = true;
		onGround = true;
	}

	void Update()
	{
		HandleStates();
		ManageInput();    
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
		if (Input.GetKey("a"))
		{
			direction = -Vector2.right;
			Walk();
		}
		else if (Input.GetKey("d"))
		{
			direction = Vector2.right;
			Walk();
		}
		else if (Input.GetKey("w"))
		{
			if(canClimb)
			{
				Climb();
			}
			else
			{
				Jump();
			}
		}
		else
		{
			isIdle = true;
		}
	}
    
	void OnCollisionStay2D(Collision2D collider)
	{
		// Checks for ground contact
		onGround = (collider.gameObject.CompareTag (GROUND)) ? true : false;
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
        if (other.CompareTag(LADDER))
        {
            canClimb = true;
        }
        if (other.CompareTag(SDPTA))
        {
            radLevel = 0;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(SAFE_AREA))
        {
            isSafe = false;
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

    private void Walk()
    {
        if (speed < MAX_SPD)
        {
            speed += MAX_SPD * Time.deltaTime;
        }
        body.velocity = direction * speed;
    }

    private void Climb()
    {
		if(speed < MAX_CLIMBSPD)

		body.velocity = direction * speed;
    }

    private void Irradiate()
    {
        
    }

    private void Heal()
    {

    }
}
