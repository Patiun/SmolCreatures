using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature_Movement : MonoBehaviour {

	public float walk_speed = 1.5f;
	public float flee_scale = 2.0f;
	public float turn_speed = 15f;

	private Creature_AI ca;
	private Rigidbody rb;

	private Vector3 targetLocation;

	// Use this for initialization
	void Start () {
		ca = GetComponent<Creature_AI> ();
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		switch (ca.GetState ()) {
		case Creature_AI.State.Wandering:
			Move (1.0f);
			break;
		case Creature_AI.State.MovingTo:
			GoTo (targetLocation);
			Move (1.0f);
			break;
		case Creature_AI.State.Fleeing:
			Move (flee_scale);
			break;
		case Creature_AI.State.Asleep:
			Stop ();
			break;
		default:
			Stop ();
			break;
		}
	}

	public void Move(float scale) {
		rb.velocity = transform.up * scale * walk_speed;
	}

	public void Stop() {
		Move (0);
	}

	public void ChooseRandomDirection() {
		int angle = Random.Range (-360, 360);
		transform.RotateAround (transform.position, transform.forward, angle);
	}

	public void TurnAwayFrom(Vector3 pos) {
		transform.RotateAround (transform.position, transform.forward, 180);
	}

	public void Turn(int direction) { //-1 is left, 1 is right
		transform.RotateAround (transform.position, transform.forward, direction*turn_speed);
	}

	public void GoTo(Vector3 pos) {
		targetLocation = pos;
		float angle = Vector3.Angle (transform.up-transform.position, pos-transform.position);
		transform.RotateAround (transform.position, transform.forward, angle);
	}
}
