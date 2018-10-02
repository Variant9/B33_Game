using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadCameraCode : MonoBehaviour {
   public GameObject CamSphere;
    Vector3 offset;
	// Use this for initialization
	void Start () {
        offset = gameObject.transform.position - CamSphere.transform.position; 
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        CamSphere.transform.position = gameObject.transform.position + offset;
	}
}
