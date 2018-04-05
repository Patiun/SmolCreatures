using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature_Movement : MonoBehaviour {

	public float walk_speed = 1.5f;
	public float turn_speed = 15f;

	private Creature_AI ca;
	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		ca = GetComponent<Creature_AI> ();
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (ca.GetState () == Creature_AI.State.Wandering) {
			Move ();
		} else {
			Stop ();
		}
	}

	public void Move() {
		rb.velocity = transform.up * walk_speed;
	}

	public void Stop() {
		rb.velocity = Vector3.zero;
	}

	public void ChooseRandomDirection() {
		int angle = Random.Range (-360, 360);
		transform.RotateAround (transform.position, transform.forward, angle);
	}

	public void TurnAwayFrom(Vector3 pos) {
		transform.LookAt (pos);
		transform.RotateAround (transform.position, transform.forward, 180);
	}
}
