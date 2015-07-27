using UnityEngine;
using System.Collections;

public class StartScreen 
{

    public const string START_LEVEL = "Level_One"; 
    private void StartGame()
    {
        Application.LoadLevel(START_LEVEL);
    }
	
}
