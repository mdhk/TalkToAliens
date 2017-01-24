using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePlacer : MonoBehaviour {

	// Objecten
	public GameObject rabbit1;
	public GameObject rabbit2;
	public GameObject chair1;
	public GameObject chair2;
	public GameObject duck1;
	public GameObject duck2;
	public GameObject frog1;
	public GameObject frog2;

	GameObject GetGameObject(string name, bool subject) {
		GameObject obj;
		switch (name) {
		case "rabbit":
			if (subject)
				obj = rabbit1;
			else
				obj = rabbit2;
			break;

		case "chair":
			if (subject)
				obj = chair1;
			else
				obj = chair2;
			break;

		case "duck":
			if (subject)
				obj = duck1;
			else
				obj = duck2;
			break;

		case "frog":
			if (subject)
				obj = frog1;
			else
				obj = frog2;
			break;

		default:
			obj = null;
			Debug.Log ("Error! Geen geldige entiteit!");
			break;
		}

		return obj;
	}


	// Use this for initialization
	void Start () {
		GrammarReader grammar = GetComponent<GrammarReader> ();
		Situation sit = grammar.GetRandomSituation ();
		Debug.Log (sit.Sentence);

		Transform transform = GetComponent<Transform> ();
		transform.position = new Vector3 (0, 0, 0);

		var subj = GetGameObject (sit.Subj.Name, true);
		var obj = GetGameObject (sit.Obj.Name, false);

		subj.SetActive (true);
		obj.SetActive (true);

		subj.transform.Translate(new Vector3 (sit.Subj.Location.X*20, 1, SkipRiver(sit.Subj.Location.Y)*10));
			obj.transform.Translate(new Vector3 (sit.Obj.Location.X*20, 1, SkipRiver(sit.Obj.Location.Y)*10));

		Debug.Log (subj.transform.position.z);
		Debug.Log (obj.transform.position.z);

	}

	int SkipRiver (int Y_loc) {
		if (Y_loc >= 5) {
			Y_loc = Y_loc + 2;
		}
		return Y_loc;
	}

	// Update is called once per frame
	void Update () {
		
	}

}
