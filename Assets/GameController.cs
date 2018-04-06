using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public GameObject creature_prefab;
	public List<GameObject> creatures;

	public GameObject sound_prefab;

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
		if (Input.GetMouseButtonDown (1)) {
			GameObject s = Instantiate (sound_prefab);
			Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3 (Input.mousePosition.x,Input.mousePosition.y,0));
			s.transform.position = new Vector3 (point.x, point.y, 0);
		}
	}
}
