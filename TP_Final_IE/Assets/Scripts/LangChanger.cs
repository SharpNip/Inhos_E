using UnityEngine;
using System.Collections;

public class LangChanger 
    : MonoBehaviour 
{
    public Localizater localizer;

    public GUIText text;
    // Insufficient time to use this class

	// Use this for initialization
	void Awake() 
    {
        localizer = GetComponent<Localizater>();	
	}

    void Start()
    {
        
    }
	
}
