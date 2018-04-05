using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature_AI : MonoBehaviour {

	public enum State {Wandering,Fleeing,Asleep};
	public State state;

	public int minWanderTime = 100;
	public int maxWanderTime = 600;
	public int curWanderTime = 0;
	public int wanderCount = 0;

	public int minFleeTime = 10;
	public int maxFleeTime = 60;
	public int curFleeTime = 0;
	public int fleeCount = 0;

	public int minSleepTime = 100;
	public int maxSleepTime = 1000;
	public int curSleepTime = 0;
	public int sleepCount = 0;

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
			if (fleeCount >= curFleeTime) {
				Wander ();
				fleeCount = 0;
			} else {
				fleeCount += 1;
			}
			break;
		case State.Asleep:
			if (sleepCount >= curSleepTime) {
				Wander ();
				sleepCount = 0;
			} else {
				sleepCount += 1;
			}
			break;
		default:
			Sleep ();
			break;
		}
	}

	public void Wander() {
		state = State.Wandering;
		curWanderTime = Random.Range (minWanderTime, maxWanderTime);
		wanderCount = 0;
		cm.ChooseRandomDirection ();
	}

	public void Flee() {
		state = State.Fleeing;
		curFleeTime = Random.Range (minFleeTime, maxFleeTime);
		fleeCount = 0;
	}

	public void Sleep() {
		state = State.Asleep;
		curFleeTime = Random.Range (minSleepTime, maxSleepTime);
		sleepCount = 0;
	}

	public void ContactWith(GameObject obj){
		switch (obj.tag) {
		case "Creature":
			cm.TurnAwayFrom (obj.transform.position);
			Flee ();
			break;
		default:
			Wander ();
			break;
		}
	}

	public void See(GameObject obj,int eye_id = 0) {
		switch (obj.tag) {
		case "Creature":
			cm.TurnAwayFrom (obj.transform.position);
			Flee ();
			break;
		default:
			cm.Turn (eye_id*2-1);
			break;
		}
	}
}
