using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartScreen 
    : MonoBehaviour
{

    public const string START_LEVEL = "Level_One"; 
    
    public void StartGame()
    {
        Application.LoadLevel(START_LEVEL);
    }
	
}
