using UnityEngine;
using System.Collections;

public class SafeZone 
    : MonoBehaviour 
{
 
    // amount by which the bottle can heal
    private float restoration;
    
    // Use this for initialization
    void Start()
    {
        restoration = 5.0f;
    }
    
   
    public float GetRestored()
    {
        return restoration;
    }
}
