using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature_AI : MonoBehaviour {

	public enum State {Wandering,Fleeing,Asleep,MovingTo};
	public State state;

	public int minWanderTime = 100;
	public int maxWanderTime = 600;
	private int curWanderTime = 0;
	private int wanderCount = 0;

	public int minFleeTime = 10;
	public int maxFleeTime = 60;
	private int curFleeTime = 0;
	private int fleeCount = 0;

	public int minSleepTime = 100;
	public int maxSleepTime = 1000;
	private int curSleepTime = 0;
	private int sleepCount = 0;

	public int maxEnergy = 100;
	public int curEnergy = 100;

	public float distance = 0.3f;
	public Vector3 goToTarget;

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
				if (curEnergy - curFleeTime <= 0) {
					curEnergy = 0;
					Sleep ();
				} else { 
					curEnergy -= curFleeTime;
					Wander ();
				}
				fleeCount = 0;
			} else {
				fleeCount += 1;
			}
			break;
		case State.Asleep:
			if (sleepCount >= curSleepTime) {
				curEnergy = maxEnergy;
				Wander ();
				sleepCount = 0;
			} else {
				sleepCount += 1;
			}
			break;
		case State.MovingTo:
			Debug.DrawRay (transform.position, goToTarget, Color.yellow);
			if (Vector3.Distance(transform.position,goToTarget) <= distance) {
				Wander();
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
		curSleepTime = Random.Range (minSleepTime, maxSleepTime);
		sleepCount = 0;
	}

	public void MoveTo(Vector3 target) {
		if (state == State.Wandering || state == State.MovingTo) {
			state = State.MovingTo;
			goToTarget = new Vector3(target.x,target.y,target.z);
			cm.GoTo (target);
		}
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
			if (obj.GetComponent<Creature_AI> ().GetState () != State.Asleep) {
				cm.TurnAwayFrom (obj.transform.position);
				Flee ();
				break;
			} else {
				cm.Turn (eye_id * 2 - 1);
				break;
			}
		default:
			cm.Turn (eye_id*2-1);
			break;
		}
	}
}
