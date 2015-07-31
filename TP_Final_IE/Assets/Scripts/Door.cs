using UnityEngine;
using System.Collections;

public class Door 
    : MonoBehaviour
{
    private const string MAYA = "Player";
    private const string LEVEL_ONE = "Level_One";
    private const string LEVEL_TWO = "Level_Two";
    private const string TITLE = "TitleScreen";    
    private string currentLevel;

    public Sprite spaceBar;

	void Start () 
    {
        currentLevel = Application.loadedLevelName;
        spaceBar.pivot.Scale(new Vector2(0,0));
	}

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
                    Application.LoadLevel(TITLE);
                }
            }
            
        }
    }
}
