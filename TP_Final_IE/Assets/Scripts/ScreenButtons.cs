using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// ScreenButtons
/// This class just takes care of the buttons pressed in the UI
/// </summary>


public class ScreenButtons
    : MonoBehaviour
{

    public const string START_LEVEL = "Level_One";
    public const string TITLE = "TitleScreen";


    public void StartGame()
    {
        Application.LoadLevel(START_LEVEL);
    }
    public void RestartGame()
    {
        Application.LoadLevel(TITLE);
    }
	
}
