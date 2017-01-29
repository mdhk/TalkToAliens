using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.LeftArrow))
			transform.Rotate (0, -25 * Time.deltaTime, 0);
		if (Input.GetKey (KeyCode.RightArrow))
			transform.Rotate (0, 25 * Time.deltaTime, 0);
		if (Input.GetKey (KeyCode.UpArrow))
			transform.Translate (0, 0, 25 * Time.deltaTime);
		if (Input.GetKey (KeyCode.DownArrow))
			transform.Translate (0, 0, -25 * Time.deltaTime);
	}
}
