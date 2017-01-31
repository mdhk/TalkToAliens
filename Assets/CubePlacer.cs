using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePlacer : MonoBehaviour {

    /* --- Class variables --- */
	// Cartoon figures
	public GameObject rabbit1;
	public GameObject rabbit2;
	public GameObject chair1;
	public GameObject chair2;
	public GameObject duck1;
	public GameObject duck2;
	public GameObject frog1;
	public GameObject frog2;

    // Game entities
    public GameObject subj;
	public GameObject obj;
	public GameObject alien;
	public Camera MainCamera;
	public int counter = 0;

    // GUI
	public TextMesh sentence;

    // Grammar and semantics
    GrammarReader grammar;
    List<Situation> situationList;
    int situationId;
    Entity subjEntity;
    Entity objEntity;

    /* --- Helper methods --- */
    // Select game entities
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

	// TODO: ervoor zorgen dat entities niet in de rivier gaan staan en liefst niet uit beeld verdwijnen
	// ik wilde 2 optellen bij alle Y-coordinaten groter gelijk aan 5 (want tussen 4 en 5 zit de rivier), maar dit werkt niet
	// update: volgens mij werkt het toch wel?
	int SkipRiver (int Y_loc) {
		if (Y_loc >= 5) {
			Y_loc = Y_loc + 2;
		}
		return Y_loc;
	}

    private void Turn(GameObject entity, Point loc, Point dir)
    {
        // Get child object
        Transform child = entity.transform.GetChild(0);
        if (dir.Y < loc.Y) child.Rotate(0, 180, 0);
        if (dir.X > loc.X) child.Rotate(0, 90, 0);
        if (dir.X < loc.X) child.Rotate(0, 270, 0);
    }

    private void UndoTurn(GameObject entity)
    {
        // Get child object
        Transform child = entity.transform.GetChild(0);

        // Set rotation to (0, 0, 0)
        child.rotation = Quaternion.identity;
    }

    void GenerateSituation(Situation sit) {

        // Ruim oude entities op
        if (subj != null && obj != null)
        {
            // Deactiveer
            subj.SetActive(false);
            obj.SetActive(false);
        }

        // Nieuwe game entities
        subj = GetGameObject(sit.Subj.Name, true);
        obj = GetGameObject(sit.Obj.Name, false);
        subjEntity = sit.Subj;
        objEntity = sit.Obj;

        // Verhoog counter
		counter += 1;

        // Pas transforms aan
		subj.transform.position = new Vector3 (0, 3, 0);
        obj.transform.position = new Vector3(0, 3, 0);
		subj.SetActive (true);
		obj.SetActive (true);
		subj.transform.Translate(new Vector3 (sit.Subj.Location.X*20, 0, SkipRiver(sit.Subj.Location.Y)*10));
		obj.transform.Translate(new Vector3 (sit.Obj.Location.X*20, 0, SkipRiver(sit.Obj.Location.Y)*10));

        // Update oriëntatie
        UndoTurn(subj);
        UndoTurn(obj);
        Turn(subj, subjEntity.Location, subjEntity.Direction);
        Turn(obj, objEntity.Location, objEntity.Direction);

        // Schrijf gegevens naar console
        Debug.Log ("=== SITUATION " + counter + " ===");
		Debug.Log (
            "subj loc: (" + sit.Subj.Location.X + "," + sit.Subj.Location.Y + "), " +
            "abs. pos.: (" + subj.transform.position.x + "," + subj.transform.position.y + "," + subj.transform.position.z + "), " +
            "dir: (" + sit.Subj.Direction.X + " , " + sit.Subj.Direction.Y + ")");
		Debug.Log ("obj loc: (" + sit.Obj.Location.X + "," + sit.Obj.Location.Y + "), " +
			"abs. pos.: (" + obj.transform.position.x + "," + obj.transform.position.y + "," + obj.transform.position.z + "), " +
            "dir: (" + sit.Obj.Direction.X + " , " + sit.Obj.Direction.Y + ")");

        // Stel tekst in
		sentence.text = sit.Sentence;
	}

    // Maak lijst met 1000 nieuwe situaties
    void MakeSituationList()
    {
        situationList = new List<Situation>();
        for (int i = 0; i <= 1000; i++)
        {
            situationList.Add(grammar.GetRandomSituation());
        }
    }

    /* --- Standard methods --- */
    // Use this for initialization
    void Start()
    {
        // Initialiseer grammarticaobject en situatielijst
        grammar = GetComponent<GrammarReader>();
        situationList = new List<Situation>();

        // Initialiseer situatielijst
        MakeSituationList();

        foreach(Situation sit in situationList)
        {
            Debug.Log(sit.Sentence);
        }

        // Maak eerste situatie
        GenerateSituation(situationList[situationId]);

        // Maak alien vast aan camera
        alien.transform.parent = MainCamera.transform;
    }


    // Update is called once per frame
    void Update () {

        //Debug.Log(situationId);

        // PUNT: ga naar volgende situatie
		if (Input.GetKeyDown (KeyCode.Period)) {
            // Verhoog situatieteller
            situationId++;

            // Situaties op?
            if (situationId >= 999)
            {
                MakeSituationList();
                situationId = 0;
            }

            // Ga naar situatie
            GenerateSituation(situationList[situationId]);
        }

        // KOMMA: ga naar vorige situatie
        if (Input.GetKeyDown(KeyCode.Comma))
        {
            // Verlaag teller
            situationId--;

            // Aan het begin van de lijst?
            if (situationId <= 0)
                situationId = 0;
            
            // Ga naar betreffende situatie
            GenerateSituation(situationList[situationId]);
        }
	}
}
