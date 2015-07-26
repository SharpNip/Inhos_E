using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GeigerHUD 
    : MonoBehaviour 
{
    private string currentLevel;
    private float currentRads;
    private Maya player;
    public Image arrow;
    public Image geigerCounter;
	
	void Start () 
    {
        player = (Maya)FindObjectOfType(typeof(Maya));
        currentRads = 0.0f;
        currentLevel = Application.loadedLevelName;
	}
	
	// Update is called once per frame
	void Update () 
    {
        currentRads = player.GetRadLevel();
        ManageCounterLevel(currentRads);
        ManageVisibility(currentRads);
        if (player.isDead)
        {
            ResetLevel();
        }
        
	}
    private void ManageVisibility(float radLevel)
    {
        if (radLevel > 0)
        {
 
        }
    }
    private void ManageCounterLevel(float radLevel)
    {
        Vector3 rotation = new Vector3(0, 0, -radLevel);
        arrow.transform.Rotate(rotation);
    }
    private void ResetLevel()
    {
        Application.LoadLevel(currentLevel);
    }
}
