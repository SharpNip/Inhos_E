using UnityEngine;
using System.Collections;
/// <summary>
/// Bottle:
///     -> This script is used for the bottle behavior.
///         - Each bottle contains DPTA (Diethylenetriaminepentaacetic Acid)
///           which is a treatement for radiation poisining!
///         @LevelResetted function-> This is a delegated function to properly restore the bottles
///                 as they had a bug in which they would not properly reset when the level was reloaded
/// </summary>


public class Bottle 
    : MonoBehaviour 
{
    // Identity tag for player
    private const string MAYA = "Player";
    
    // Personal elements
    private Collider2D myCol;
    private SpriteRenderer mySprite;
    private Maya maya;
    private bool isActive;
    
    // Amount by which the bottle can heal
    private float restoration;

    void Start()
    {
        // Gets the components to later use them in the reset method
        myCol = GetComponent<BoxCollider2D>();
        mySprite = GetComponent<SpriteRenderer>();
        isActive = true;
        restoration = 100.0f;

        // Stores a reference to the player for the delegate method
        maya = (Maya)FindObjectOfType(typeof(Maya));
        maya.reset += LevelResetted;
    }

    // The best way I could find the make sure that the object could be reinitialized
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag(MAYA))
        {
            if (isActive)
            {
                isActive = false;
                mySprite.enabled = false;
                myCol.enabled = false;
            }
        }
    }
    // Delegated method from the player that resets each instance of the bottle
    // This may seem unorthodox but this was a "fix" to an issue where
    // bottles would not properly reset upon reloading a level
    private void LevelResetted(Maya maya)
    {
        maya.reset -= LevelResetted;
        isActive = true;
        mySprite.enabled = true;
        myCol.enabled = true;
    }
    // Used by the player to get the value of the restoration
    public float GetRestored()
    {
        return restoration;
    }
}
