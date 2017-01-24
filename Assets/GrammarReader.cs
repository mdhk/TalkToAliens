using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Entity {
	string name;
	Point location;
	Point direction;

	public Point Location { get { return location; } }
	public Point Direction { get { return direction; } }
	public string Name { get { return name; } }

	public Entity(string name, int locX, int locY, int dirX, int dirY) {
		this.name = name;
		location = new Point(locX, locY);
		direction = new Point(dirX, dirY);
	}
}


public class Point {
	int x; int y;

	public int X { get { return x; } }
	public int Y { get { return y; } }

	public Point(int x, int y) {
		this.x = x;
		this.y = y;
	}
}

public class Situation {
	string sentence;
	Entity subj;
	Entity obj;

	public string Sentence { get { return sentence; } }
	public Entity Subj { get { return subj; } }
	public Entity Obj { get { return obj; } }

	public Situation(string[] csvEntry) {
		sentence = csvEntry [0];
		subj = new Entity (
			csvEntry [1], 
			int.Parse (csvEntry [2]), 
			int.Parse (csvEntry [3]),
			int.Parse (csvEntry [4]),
			int.Parse (csvEntry [5])
		);
		obj = new Entity (
			csvEntry [6], 
			int.Parse (csvEntry [7]), 
			int.Parse (csvEntry [8]),
			int.Parse (csvEntry [9]),
			int.Parse (csvEntry [10])
		);
	}
}


public class GrammarReader : MonoBehaviour {

	public TextAsset grammar;
	public List<Situation> situations;

	// Use this for initialization
	void Start () {
		string[] lines = grammar.text.Split (new char[] { '\n' });
		situations = new List<Situation> ();
		for (int i = 1; i < lines.Length; i++) { 
			string[] columns = lines[i].Split (new char[] { ',' });
			if (columns.Length == 11) {
				situations.Add (new Situation (columns));
			}
		}
	}

	public Situation GetRandomSituation() {
		System.Random rand = new System.Random ();
		int r = rand.Next (situations.Count);
		return situations [r];
	}

	// Update is called once per frame
	void Update () {
		
	}
}
