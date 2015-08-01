using UnityEngine;
using System.Collections;

/// <summary>
/// Door Class:
/// While this class was not originally in my UML, I found that it was actually very useful, first to get an animation in,
/// second, to give a "direction" to my levels
/// 
/// It takes care of finishing off the level for the player when she gets in range of the door, and shows a sign for the player to know what to do
/// </summary>


public class Door 
    : MonoBehaviour
{
    // Several const strings just to have easy references to different items
    private const string MAYA = "Player";
    private const string LEVEL_ONE = "Level_One";
    private const string LEVEL_TWO = "Level_Two";
    private const string END = "EndScreen";
    private const string TITLE = "TitleScreen";    
    
    private string currentLevel;

    // To simplify getting the desired object, this was made public to
    // put the SR directly into the inspector.
    public SpriteRenderer spaceBar;

    // Decided to add a small animation to the door so it would open when the player got close to it
    private Animator anim;

	void Start () 
    {
        // Initializes the name of the level to the currently loaded level
        currentLevel = Application.loadedLevelName;   
        // Hides the spacebar picture above the door
        spaceBar.enabled = false;
        
        // Gets the door's animator to be able to do animations!
        anim = GetComponentInChildren<Animator>();
	}
    // While this is not the most optimized way to switching levels
    // this was the simplest way of doing it with a small project
	void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag(MAYA))
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                if (currentLevel == LEVEL_ONE)
                {
                    Application.LoadLevel(LEVEL_TWO);
                }
                else if (currentLevel == LEVEL_TWO)
                {
                    Application.LoadLevel(END);
                }
            }
        }
    }
    // Displays the SpaceBar to indicate that the user must
    // press it to change levels
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag(MAYA))
        {
            anim.SetInteger("State", 1);
            spaceBar.enabled = true;
        }
    }
    // Hides the Spacebar when the user leaves the trigger area
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag(MAYA))
        {
            anim.SetInteger("State", 0);
            spaceBar.enabled = false;
        }
    }
}
