using UnityEngine;
using System.Collections;

public class RadBarrel 
    : MonoBehaviour 
{

    private float radiation;
	
	void Start () 
    {
        radiation = 3.0f;
	}


    public float GetRadiation()
    {
        return radiation;
    }
}
