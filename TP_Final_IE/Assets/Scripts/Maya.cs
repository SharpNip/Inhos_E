using UnityEngine;
using System.Collections;

public class Maya 
    : MonoBehaviour 
{
	// Status Variables
    public bool isDead;
    private bool isSafe;
	private bool isIdle;
	private bool canClimb;
	private bool onGround;
	private float radLevel;

	// Movement variables
	private const float MAX_RAD = 100;
	private const float MAX_SPD = 10;
    private const float INCREMENTATION = 3;
    private bool goingRight;
	private float jumpHeight;
	private float speed;
	private float climbSpeed;

	// Body components
	private Rigidbody2D body;

	// Collider Tags
	private const string GROUND = "Ground";
	private const string SAFE_AREA = "SafeZone";
	private const string SPIKES = "Spikes";
	private const string DANGER_AREA = "RadBarrel";
	private const string LADDER = "Ladder";
	private const string SDPTA = "Bottle";
    private const string WALL = "Wall";

	void Start () 
    {
		//Initialiasation of body components
        body = GetComponent<Rigidbody2D>();
        //Setting all status variables
		radLevel = 0;
        speed = 0;
		climbSpeed = .05f;
		jumpHeight = 2.5f;
        canClimb = false;
        isIdle = true;
		onGround = true;
        goingRight = false;
        isSafe = true;
        isDead = false;
	}

	void Update()
	{
		ManageInput();
        ManageStates();
        Debug.Log(radLevel);
    }
    private void ManageStates()
    {
        if(!isSafe)
        {
            Irradiate(2.0f);
        }
    }
	private void ManageInput()
	{
        // Going Left
		if (Input.GetKey(KeyCode.A))
		{
            goingRight = false;
            isIdle = false;
            if (speed > MAX_SPD)
            {
                speed = MAX_SPD;
            }
            else
            {
                speed += INCREMENTATION;   
                Walk(-Vector2.right);
            }
		}
        // Going Right
        else if (Input.GetKey(KeyCode.D))
        {
            goingRight = true;
            isIdle = false;
            if (speed > MAX_SPD)
            {
                speed = MAX_SPD;
            }
            else
            {
                speed += INCREMENTATION;
                Walk(Vector2.right);
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
        // Jumping
		if (Input.GetKey(KeyCode.W))
		{
            isIdle = false;
            if (canClimb)
            {
                Climb(Vector2.up);
            }
			else
			{
				Jump();
			}
		}
        if (Input.GetKey(KeyCode.S))
        {
            isIdle = false;
            if (canClimb)
            {
                Climb(-Vector2.up);
            }
        }
	}
    
	void OnCollisionStay2D(Collision2D col)
	{
        // Checks if the collider encountered is the ground
        if (col.gameObject.CompareTag(GROUND))
        {
            // Looks through all of the contact points in the array to get the normal
            foreach (ContactPoint2D contact in col.contacts)
            {
                if (contact.normal == Vector2.up)
                {
                    onGround = true;
                }
                else
                {
                    onGround = false;
                }
            }
        }        
	}
	void OnCollisionEnter2D(Collision2D collider)
	{
		if (collider.gameObject.CompareTag (SPIKES)) 
		{
            isDead = true;
		}
	}
    void OnTriggerStay2D(Collider2D other)
    {            
        if (other.CompareTag(SAFE_AREA))
        {
            SafeZone safety = (SafeZone)other.gameObject.GetComponent(SAFE_AREA);
            Heal(safety.GetRestored());
		}
		if (other.CompareTag(DANGER_AREA)) 
		{
            RadBarrel barrel = (RadBarrel)other.gameObject.GetComponent(DANGER_AREA);
            Irradiate(barrel.GetRadiation());
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
        if (other.CompareTag(LADDER))
        {
            canClimb = true;
            body.gravityScale = 0;
        }
        if (other.CompareTag(SDPTA))
        {
            Bottle bottle = (Bottle)other.gameObject.GetComponent("Bottle");
            Heal(bottle.GetRestored());
        }
        if (other.CompareTag(SAFE_AREA))
        {
            isSafe = true;
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

    private void Walk(Vector2 dir)
    {
      	Vector2 movement = dir * speed * Time.deltaTime;
        if (onGround || canClimb)
        {
            body.velocity += movement;
        }
        if (goingRight)
        {
            if (body.velocity.x > MAX_SPD)
            {
                body.velocity -= movement;
            }
        }
        else
        {
            if (body.velocity.x < -MAX_SPD)
            {
                body.velocity -= movement;
            }
        }
    }
    
    private void Climb(Vector2 up)
    {
        body.position = body.position + (up * climbSpeed); 
 
    }

    private void Irradiate(float amount)
    {
        radLevel += amount * Time.deltaTime;
        if (radLevel > MAX_RAD)
        {
            DieAndReset();
        }
    }

    private void Heal(float amount)
    {
        radLevel -= amount * Time.deltaTime;
        if (radLevel < 0)
        {
            radLevel = 0;
        }
    }

    public float GetRadLevel()
    {
        return radLevel;
    }
    private void DieAndReset()
    {
        isDead = true;
    }
}
