using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public GameObject creature_prefab;
	public List<GameObject> creatures;

	// Use this for initialization
	void Start () {
		creatures = new List<GameObject> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("p")) {
			GameObject c = Instantiate (creature_prefab);
			creatures.Add (c);
		}
	}
}
