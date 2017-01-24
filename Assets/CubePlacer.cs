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
		transform.position = new Vector3 (sit.Obj.Location.X, 0, sit.Obj.Location.Y);

		var subj = GetGameObject (sit.Subj.Name, true);
		var obj = GetGameObject (sit.Obj.Name, false);

		subj.SetActive (true);
		obj.SetActive (true);

		subj.transform.Translate(new Vector3 (sit.Subj.Location.X, 0, sit.Subj.Location.Y));
		obj.transform.Translate(new Vector3 (sit.Obj.Location.X, 0, sit.Obj.Location.Y));

		Debug.Log (sit.Subj.Location.X);
		Debug.Log (sit.Subj.Location.Y);
		Debug.Log (sit.Obj.Location.X);
		Debug.Log (sit.Obj.Location.Y);

	}

	// Update is called once per frame
	void Update () {
		
	}

}
