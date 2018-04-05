using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature_Sight : MonoBehaviour {

	public List<GameObject> eyes;
	public bool eyes_open = true;
	public LayerMask layerMask;
	public float sight_range = 1.8f;
	public float detection_range = 3.5f;

	private Creature_AI ca;

	// Use this for initialization
	void Start () {
		ca = GetComponent<Creature_AI> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (ca.GetState () != Creature_AI.State.Asleep) {
			if (!eyes_open) {
				OpenEyes ();
			}
			CheckVision ();
		} else {
			if (eyes_open) {
				CloseEyes ();
			}
		}
	}

	void CheckVision() {
		int count = 0;
		foreach(GameObject eye in eyes) {
			RaycastHit hit;
			if (Physics.Raycast (eye.transform.position, eye.transform.up, out hit, sight_range, layerMask.value)) {
				Debug.DrawRay (eye.transform.position, eye.transform.up * sight_range, Color.green);
				ca.See (hit.collider.gameObject,count);
				break;
			} else {
				Debug.DrawRay (eye.transform.position, eye.transform.up * sight_range, Color.white);
			}

			count += 1;
		}
	}

	void CloseEyes() {
		foreach(GameObject eye in eyes) {
			eye.SetActive (false);
		}
		eyes_open = false;
	}

	void OpenEyes() {
		foreach(GameObject eye in eyes) {
			eye.SetActive (true);
		}
		eyes_open = true;
	}
}
