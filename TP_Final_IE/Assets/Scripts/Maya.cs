using UnityEngine;
using System.Collections;
/// <summary>
/// Maya
/// No this is not the 3D animation tool, its the player class
/// </summary>


public class Maya 
    : MonoBehaviour 
{
    // Gameplay Variables
    private const float RADIATION = 3.0f;

    // Temp value for sound volume
    public float soundVolume;

    // Status Variables
    public bool isDead;
    private bool died;
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
    private bool hasJumped;
    private float jumpHeight;
    private float speed;
    private float climbSpeed;

    // Body components
    private Rigidbody2D body;
    private Animator anim;

    // Collider Tags
    private const string GROUND = "Ground";
    private const string SAFE_AREA = "SafeZone";
    private const string SPIKES = "Spikes";
    private const string DANGER_AREA = "RadBarrel";
    private const string LADDER = "Ladder";
    private const string SDPTA = "Bottle";
    private const string WALL = "Wall";

    // Sound Effects
    public AudioClip jumpSound;
    public AudioClip landSound;
    public AudioClip deathSound;
    private AudioSource soundSource;

    // Animation enum
    private enum state
    {
        IDLE  = 0, 
        CLIMB = 1, 
        LEFT  = 2, 
        RIGHT = 3 
    }

    // Delegate
    // Intentionally being dangerous with this (not using event in front of it)
    // because I want the propagation of the stored "event" to go everywhere
    public delegate void Reset(Maya maya);
    public Reset reset;

    void Awake()
    {
        soundSource = GetComponent<AudioSource>();
    }

	void Start () 
    {
		// Initialiasation of body components
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        // Setting all status variables
        radLevel = 0;
        // If playing in debug, make the character "invincible"
#if DEBUG
        radLevel = -10000;
#endif
        
        speed = 0;
		climbSpeed = .05f;
        jumpHeight = 2f;
// If playing in debug, make the character "invincible"
#if DEBUG
        jumpHeight = 20f;
#endif
        canClimb = false;
        isIdle = true;
		onGround = true;
        goingRight = false;
        isSafe = true;
        isDead = false;
        died = false;
        soundVolume = 0.5f;
        anim.SetInteger("State", (int)state.IDLE);
	}

    public float GetRadLevel()
    {
        return radLevel;
    }

	void Update()
	{
		ManageInput();
        ManageStates();
    }
    
    private void ManageStates()
    {
        if(!isSafe)
        {
            Irradiate(RADIATION);
        }
    }
	
    private void ManageInput()
	{
        // Going Left
		if (Input.GetKey(KeyCode.A))
		{
            goingRight = false;
            isIdle = false;
            anim.SetInteger("State", (int)state.LEFT);
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
            anim.SetInteger("State", (int)state.RIGHT);
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
            anim.SetInteger("State", (int)state.IDLE);
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
                anim.SetInteger("State", (int)state.CLIMB);
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
                anim.SetInteger("State", (int)state.CLIMB);
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
    
    void OnCollisionExit2D(Collision2D collider)
    {
        if (collider.gameObject.CompareTag(GROUND))
        {
            onGround = false;
        }
    }
	
    void OnCollisionEnter2D(Collision2D collider)
	{
		if (collider.gameObject.CompareTag (SPIKES)) 
		{
            StartCoroutine(DieAndReset());   
		}
        if (collider.gameObject.CompareTag(GROUND))
        {
            if (hasJumped && !canClimb)
            {
                soundSource.PlayOneShot(landSound, soundVolume);
                hasJumped = false;
            }
        }
	}
   
    void OnTriggerStay2D(Collider2D other)
    {            
        if (other.CompareTag(SAFE_AREA))
        {
            SafeZone safety = (SafeZone)other.gameObject.GetComponent(SAFE_AREA);
            Heal(safety.GetRestored(), false);
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
            Bottle bottle = (Bottle)other.gameObject.GetComponent(SDPTA);
            Heal(bottle.GetRestored(), true);
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
            if (!hasJumped)
            {
                hasJumped = true;
                soundSource.PlayOneShot(jumpSound, soundVolume);
            }
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
        if (radLevel > MAX_RAD)
        {
            if(!died)
            {
                died = true;
                StartCoroutine(DieAndReset());
            }
        }
        radLevel += amount * Time.deltaTime;
    }

    private void Heal(float amount, bool isMeds)
    {
        if (isMeds)
        {
            radLevel -= amount;   
        }
        radLevel -= amount * Time.deltaTime;
        if (radLevel < 0)
        {
            radLevel = 0;
        }  
    }
   
    private IEnumerator DieAndReset()
    {
        soundSource.PlayOneShot(deathSound);
        yield return new WaitForSeconds(deathSound.length);
        if (reset != null)
        {
            reset(this);
        }
        isDead = true;
    }
}
