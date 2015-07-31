using UnityEngine;
using System.Collections;

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
    // amount by which the bottle can heal
    private float restoration;

    // Use this for initialization
    void Start()
    {
        myCol = GetComponent<BoxCollider2D>();
        mySprite = GetComponent<SpriteRenderer>();
        isActive = true;
        restoration = 100.0f;
        maya = (Maya)FindObjectOfType(typeof(Maya));
        maya.reset += LevelResetted;
    }

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
    private void LevelResetted(Maya maya)
    {
        maya.reset -= LevelResetted;
        isActive = true;
        mySprite.enabled = true;
        myCol.enabled = true;
    }
    public float GetRestored()
    {
        return restoration;
    }
}
