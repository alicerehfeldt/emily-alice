using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTargetScript : MonoBehaviour {

	float startTime;
	float dieAt;
	float animationDuration = 0.3f;
	float remainDuration = 0.01f;
	// Use this for initialization
	void Start () {
		startTime = Time.time;
		dieAt = startTime + animationDuration + remainDuration;
	}
	
	// Update is called once per frame
	void Update () {
		float now = Time.time;
		if (now > dieAt) {
			Destroy (gameObject);
		} else if (now > (startTime + animationDuration)) {
			return;
		}

		float percent = (now - startTime) / animationDuration;

		float newValue = EasingFunction.Linear (1f, 0.2f, percent);

		transform.localScale = new Vector3 (newValue, newValue, 1);
	}
}
