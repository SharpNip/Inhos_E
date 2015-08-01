using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Audio;


/// <summary>
/// Geiger HUD Class:
/// 
/// As the name speaks for itself, this class is the geiger counter HUD element in the playable levels
/// However, it is also so much more, as it manages music, player health as well as the ticking sound during gameplay.
/// </summary>

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


    // AudioRelated stuff
    private bool regularIsPlaying;
    private bool stressedIsPlaying;
    public AudioMixerSnapshot regularMus;
    public AudioMixerSnapshot stressMus;

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
    // Initialisation of all variables
	void Start () 
    {
        player = (Maya)FindObjectOfType(typeof(Maya));
        currentRads = 0.0f;
        currentLevel = Application.loadedLevelName;
        startDial = Quaternion.Euler(0.0f, 0.0f, 45.0f);
        endDial = Quaternion.Euler(0.0f, 0.0f, -45.0f);
        arrow.transform.rotation = startDial;
        timer = 0.0f;
        regularIsPlaying = true;
        stressedIsPlaying = false;
	}
	
    // Calling all necessary methods
	void Update () 
    {
        currentRads = player.GetRadLevel();
        ManageCounterLevel(currentRads);
        ManageVisibility(currentRads);
        ManageSound(currentRads);
        ManageMusic(currentRads);
        if (player.isDead)
        {
            ResetLevel();
        } 
	}
    // Sound management for the Ticking sound
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

    // Seeing as a part of the functionality entailed that the hud was invisible if the player was not irradiated, 
    // Visibility management had to be done
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

    // This takes care of the music to make sure that the right "soundtrack" is played when the player's health is "low" enough
    private void ManageMusic(float rads)
    {
        
        if (rads < HIGH)
        {
            if (regularIsPlaying == false)
            {
                ChangeToReg();
            }
        }
        else
        {
            if (stressedIsPlaying == false)
            {
                ChangeToStress();
            }
        }
    }
    // This makes the transition to the regular music when the player is out of trouble
    private void ChangeToReg()
    {
        regularMus.TransitionTo(1f);
        regularIsPlaying = true;
        stressedIsPlaying = false;
    }
    // To add a little stress to the player's gameplay, this music is transited to when his health is "below 70"
    private void ChangeToStress()
    {
        stressMus.TransitionTo(1f);
        regularIsPlaying = false;
        stressedIsPlaying = true;
    }
    // I was finally able to work out the way to turn the dial on the geiger counter. This was potentially one of the more challenging aspects
    private void ManageCounterLevel(float radLevel)
    {
        float currentLevel = radLevel * ((Quaternion.Angle(startDial, endDial)) / 100f);
        arrow.transform.rotation = Quaternion.RotateTowards(startDial, endDial, currentLevel);
    }

    // This is basically called to reset the current level
    private void ResetLevel()
    {
        ChangeToReg();
        Application.LoadLevel(currentLevel);
    }
}
