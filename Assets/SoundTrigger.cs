using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour {

	public int duration = 10;
	private int count = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (count >= duration) {
			Destroy (this.gameObject);
		} else {
			count += 1;
		}
	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "Creature") {
			Creature_AI ca = col.gameObject.GetComponent<Creature_AI> ();
			ca.MoveTo (transform.position);
		}
	}
}
