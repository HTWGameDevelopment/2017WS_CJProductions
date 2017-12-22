using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstMovement : MonoBehaviour {

    public Rigidbody obrb;
    public float ObstacleSpeed;
    public float RotationSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        obrb.velocity = new Vector3(0, 0, - ObstacleSpeed * Time.deltaTime);
        //obrb.transform.Rotate(new Vector3(0, 0, RotationSpeed * Time.deltaTime));
	}
}
