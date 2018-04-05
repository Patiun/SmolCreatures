using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature_Contact : MonoBehaviour {

	private Creature_AI ca;

	// Use this for initialization
	void Start () {
		ca = GetComponent<Creature_AI> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision col) {
		ca.ContactWith (col.gameObject);
	}
}
