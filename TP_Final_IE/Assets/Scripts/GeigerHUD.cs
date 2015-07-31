using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GeigerHUD 
    : MonoBehaviour 
{
    // Constants for the sound
    private const float MILD = 10.0F;
    private const float ELEVATED = 40.0F;
    private const float HIGH = 70.0F;
    private const float LOWTICK = 3.0F;
    private const float MEDTICK = 1.5F;
    private const float HIGHTICK = 0.3F;

    // Homemade timer to manage the tick sound
    private float timer;

    // Stores the current level's name for easy reloading
    private string currentLevel;
    
    // Value taken from player to check on her "health"
    private Maya player;
    private float currentRads;
    
    // These are all used to move the arrow on the counter in the hud
    private Quaternion startDial;
    private Quaternion endDial;
    public Image arrow;
    public Image geigerCounter;

    // Sound related variables
    private AudioSource sound;
    public AudioClip tickSound;

    void Awake()
    {
        sound = GetComponent<AudioSource>();
    }

	void Start () 
    {
        player = (Maya)FindObjectOfType(typeof(Maya));
        currentRads = 0.0f;
        currentLevel = Application.loadedLevelName;
        startDial = Quaternion.Euler(0.0f, 0.0f, 45.0f);
        endDial = Quaternion.Euler(0.0f, 0.0f, -45.0f);
        arrow.transform.rotation = startDial;
        timer = 0.0f;
	}
	
	void Update () 
    {
        currentRads = player.GetRadLevel();
        ManageCounterLevel(currentRads);
        ManageVisibility(currentRads);
        ManageSound(currentRads);
        if (player.isDead)
        {
            ResetLevel();
        } 
	}
    private void ManageSound(float radLevel)
    {
        if (radLevel > MILD)
        {
            timer += Time.deltaTime;
            if (timer > LOWTICK)
            {
                sound.PlayOneShot(tickSound);
                timer = 0.0f;
            }
        }
        if (radLevel > ELEVATED)
        {
            timer += Time.deltaTime;
            if (timer > MEDTICK)
            {
                sound.PlayOneShot(tickSound);
                timer = 0.0f;
            }
        }
        if (radLevel > HIGH)
        {
            timer += Time.deltaTime;
            if (timer > HIGHTICK)
            {
                sound.PlayOneShot(tickSound);
                timer = 0.0f;
            }
        }
    }
    private void ManageVisibility(float radLevel)
    {
        if (radLevel > 0)
        {
            arrow.enabled = true;
            geigerCounter.enabled = true;
        }
        else
        {
            arrow.enabled = false;
            geigerCounter.enabled = false;
        }
    }
    private void ManageCounterLevel(float radLevel)
    {
        float currentLevel = radLevel * ((Quaternion.Angle(startDial, endDial)) / 100f);
        arrow.transform.rotation = Quaternion.RotateTowards(startDial, endDial, currentLevel);
    }
    private void ResetLevel()
    {
        Application.LoadLevel(currentLevel);
    }
}
