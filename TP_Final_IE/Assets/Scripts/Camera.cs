using UnityEngine;
using System.Collections;

/// <summary>
/// Camera Class:
/// THis class takes care of simply following the player, adding a certain measure of difficulty (as the player cannot see beyond the edges)
/// </summary>

public class Camera 
    : MonoBehaviour
{
    public float dampTime;
    private Vector3 velocity;
    private Transform player;
    
 
    void Start () 
    {
        dampTime = 1.5f;
        if (player == null)
        {
            player = GameObject.Find("Maya").transform;
        }            
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        }

    }
}