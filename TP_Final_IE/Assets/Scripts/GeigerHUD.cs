using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GeigerHUD 
    : MonoBehaviour 
{
    private string currentLevel;
    private float currentRads;
    private Quaternion startDial;
    private Quaternion endDial;
    private Maya player;
    public Image arrow;
    public Image geigerCounter;
	
	void Start () 
    {
        player = (Maya)FindObjectOfType(typeof(Maya));
        currentRads = 0.0f;
        currentLevel = Application.loadedLevelName;
        startDial = Quaternion.Euler(0.0f, 0.0f, 45.0f);
        endDial = Quaternion.Euler(0.0f, 0.0f, -45.0f);
        arrow.transform.rotation = startDial;        
	}
	
	// Update is called once per frame
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
