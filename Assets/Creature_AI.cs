using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature_AI : MonoBehaviour {

	public enum State {Wandering,Running,Fleeing};
	public State state;

	public int minWanderTime = 100;
	public int maxWanderTime = 600;
	public int curWanderTime = 0;
	public int wanderCount = 0;

	private Creature_Movement cm;

	public State GetState() {
		return state;
	}

	// Use this for initialization
	void Start () {
		cm = GetComponent<Creature_Movement> ();
		Wander ();
	}
	
	// Update is called once per frame
	void Update () {
		switch (state) {
		case State.Wandering:
			if (wanderCount >= curWanderTime) {
				Wander ();
				wanderCount = 0;
			} else {
				wanderCount += 1;
			}
			break;
		case State.Fleeing:
			break;
		default:
			Wander ();
			break;
		}
	}

	public void Wander() {
		state = State.Wandering;
		curWanderTime = Random.Range (minWanderTime, maxWanderTime);
		cm.ChooseRandomDirection ();
	}

	public void Flee() {
		state = State.Fleeing;

	}

	public void ContactWith(GameObject obj){

	}
}
