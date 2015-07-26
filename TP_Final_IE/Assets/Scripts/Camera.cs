using UnityEngine;
using System.Collections;

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