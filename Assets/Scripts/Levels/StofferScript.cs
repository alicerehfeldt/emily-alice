using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StofferScript : MonoBehaviour {

	public float MIN_WAIT = 20f;
	public int RAND_WAIT = 20;
	public float MOVE_SPEED = 0.03f;
	float wakeAt;
	bool moving = false;



	// Use this for initialization
	void Start () {
		GoToSleep ();
	}
	
	// Update is called once per frame
	void Update () {
		float now = Time.time;
		if (!moving && now > wakeAt) {
			moving = true;
		}

		if (moving) {
			Vector3 moveTarget = new Vector3 (-8f, this.transform.localPosition.y, 0);
			if (this.transform.localPosition.Equals (moveTarget)) {
				GoToSleep ();
				return;
			}
			this.transform.localPosition = Vector3.MoveTowards (this.transform.localPosition, moveTarget, MOVE_SPEED);
		}
	}


	void GoToSleep() {
		moving = false;
		System.Random rng = new System.Random ();
		float timeToSleep = MIN_WAIT + (float) rng.Next (RAND_WAIT);
		wakeAt = Time.time + timeToSleep;
		// move to the right
		transform.localPosition = new Vector3 (8f, this.transform.localPosition.y, 0);
	}

}
