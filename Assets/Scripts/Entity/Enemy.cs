using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : ENT_Parent {
    public Vector3 NextPosition;
    public Vector3 FinalDestination;
    bool seeking;

    void seek()
    {
        if(seeking)
        {

        }
    }
	// Use this for initialization
	void Start () {
        Initialize();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void FixedUpdate()
    {

    }
}
