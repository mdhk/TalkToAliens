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
	public GameObject subj;
	public GameObject obj;
	public GameObject alien;
	public Camera MainCamera;
	public int counter = 0;

	public TextMesh sentence;

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
		// alien zit vast aan camera
		GenerateSituation ();
		alien.transform.parent = MainCamera.transform;
	}


	// TODO: ervoor zorgen dat entities niet in de rivier gaan staan en liefst niet uit beeld verdwijnen
	// ik wilde 2 optellen bij alle Y-coordinaten groter gelijk aan 5 (want tussen 4 en 5 zit de rivier), maar dit werkt niet
	// update: volgens mij werkt het toch wel?
	int SkipRiver (int Y_loc) {
		if (Y_loc >= 5) {
			Y_loc = Y_loc + 2;
		}
		return Y_loc;
	}

	void GenerateSituation() {
		GrammarReader grammar = GetComponent<GrammarReader> ();

		Situation sit = grammar.GetRandomSituation ();
		// TODO: zorgen dat er een logische opvolging van situaties (?) is -- iig niet twee dezelfde situaties na elkaar
		// elke zin opslaan in lijst en dan bij elke nieuwe situatie checken of ie niet al in de lijst staat ofzo?

		counter += 1;

		Transform transform = GetComponent<Transform> ();
		transform.position = new Vector3 (0, 0, 0);

		subj = GetGameObject (sit.Subj.Name, true);
		obj = GetGameObject (sit.Obj.Name, false);

		subj.SetActive (true);
		obj.SetActive (true);

		subj.transform.Translate(new Vector3 (sit.Subj.Location.X*20, 0, SkipRiver(sit.Subj.Location.Y)*10));
		obj.transform.Translate(new Vector3 (sit.Obj.Location.X*20, 0, SkipRiver(sit.Obj.Location.Y)*10));

		// TODO: update direction

		Debug.Log ("=== SITUATION " + counter + " ===");
		Debug.Log ("subj loc: (" + sit.Subj.Location.X + "," + sit.Subj.Location.Y + "), abs. pos.: (" + subj.transform.position.x 
			+ "," + subj.transform.position.y + "," + subj.transform.position.z + ")");
		Debug.Log ("obj loc: (" + sit.Obj.Location.X + "," + sit.Obj.Location.Y + "), " +
			"abs. pos.: (" + obj.transform.position.x + "," + obj.transform.position.y + "," + obj.transform.position.z + ")");


		sentence.text = sit.Sentence;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("space")) {
			// reset locatie van entities
			Vector3 pos = new Vector3(0, 3, 0);
			subj.transform.position = pos;
			obj.transform.position = pos;

			// deactiveer entities
			subj.SetActive (false);
			obj.SetActive (false);

			// maak nieuwe situatie
			GenerateSituation ();
		}
	}

}
