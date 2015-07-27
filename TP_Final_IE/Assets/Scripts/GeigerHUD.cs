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
        startDial = Quaternion.Euler(0.0f, 0.0f, 40.0f);
        endDial = Quaternion.Euler(0.0f, 0.0f, 320.0f);
        arrow.transform.localRotation = startDial;        
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
        float resultAngle = radLevel / Quaternion.Angle(endDial, startDial);
        Quaternion tempRot = Quaternion.Euler(0.0f, 0.0f, resultAngle);
        
        Debug.Log(resultAngle);

        arrow.transform.localRotation = tempRot;

    }
    private void ResetLevel()
    {
        Application.LoadLevel(currentLevel);
    }
}
