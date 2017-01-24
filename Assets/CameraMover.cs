using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.LeftArrow))
			transform.Rotate (0, -5, 0);
		if (Input.GetKeyDown (KeyCode.RightArrow))
			transform.Rotate (0, 5, 0);
		if (Input.GetKeyDown (KeyCode.UpArrow))
			transform.Translate (0, 0, 5);
		if (Input.GetKeyDown (KeyCode.DownArrow))
			transform.Translate (0, 0, -5);
	}
}
