using UnityEngine;
using System.Collections;

/// <summary>
/// SafeZone:
/// THis is the safeazone from which the player can "regain" health when she's hurt
/// 
/// 
/// </summary>
public class SafeZone 
    : MonoBehaviour 
{
    // Amount by which the bottle can heal
    public float restoration;
    
    // Use this for initialization
    void Start()
    {
        restoration = 10.0f;
    }
    
   
    public float GetRestored()
    {
        return restoration;
    }
}
