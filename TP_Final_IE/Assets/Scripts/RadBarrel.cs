using UnityEngine;
using System.Collections;

/// <summary>
/// RadBarrel
/// 
/// This class is the "Danger zone" depicted in the UML, when the player gets close it it, he takes more damage than he normally does
/// </summary>


public class RadBarrel 
    : MonoBehaviour 
{

    private float radiation;
	
	void Start () 
    {
        radiation = 6.0f;
	}


    public float GetRadiation()
    {
        return radiation;
    }
}
