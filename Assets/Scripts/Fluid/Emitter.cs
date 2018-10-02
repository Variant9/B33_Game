using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emitter : MonoBehaviour {
    public GameObject part;
    public float timer;
    public bool emitting;
    public Vector3 Velocity = new Vector3(1,1,1);
    public Vector3 offset  = new Vector3(0,0,0);
    public float accuracy;
    float time;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (emitting)
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                time = timer;
                GameObject newobj = GameObject.Instantiate(part);
                newobj.transform.position = transform.position + offset;
                newobj.GetComponent<Rigidbody>().velocity = Velocity + new Vector3(Random.Range(-accuracy, accuracy), Random.Range(-accuracy, accuracy), Random.Range(-accuracy, accuracy));
            }
        }
    }

}
