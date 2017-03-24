using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StofferWobbleScript : MonoBehaviour {
	float endTime;
	float animationDuration = 1f;
	float minZ, maxZ;
	bool isTiltingForward = false;

	// Use this for initialization
	void Start () {
		minZ = -0.1f;
		maxZ = 0.1f;
		SwapAnimation ();
	}

	void SwapAnimation() {
		endTime = Time.time + animationDuration;
		isTiltingForward = !isTiltingForward;
	}

	// Update is called once per frame
	void Update () {
		float now = Time.time;

		if (now > endTime) {
			SwapAnimation ();
		}

		float newValue = 0f;
		float howFar = 1 - ((endTime - now) / animationDuration);
		if (isTiltingForward) {
			newValue = EasingFunction.EaseInOutQuad (minZ, maxZ, howFar);
		} else {
			newValue = EasingFunction.EaseInOutQuad (maxZ, minZ, howFar);
		}
		//Debug.Log ("Stoffer " + transform.localRotation.ToString ());
		this.transform.localRotation = new Quaternion (0f, 0f, newValue, 1.0f);

	}
}

