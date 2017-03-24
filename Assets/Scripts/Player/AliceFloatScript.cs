using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliceFloatScript : MonoBehaviour {

	float endTime;
	float animationDuration = 2f;
	float floatDistance = 0.15f;
	float baseY, maxY;
	bool isFloatingUp = false;

	// Use this for initialization
	void Start () {
		baseY = this.transform.localPosition.y;
		maxY = baseY + floatDistance;
		SwapAnimation ();
	}

	void SwapAnimation() {
		endTime = Time.time + animationDuration;
		isFloatingUp = !isFloatingUp;
	}

	// Update is called once per frame
	void Update () {
		float now = Time.time;

		if (now > endTime) {
			SwapAnimation ();
		}

		float newValue = 0f;
		float howFar = 1 - ((endTime - now) / animationDuration);
		if (isFloatingUp) {
			newValue = EasingFunction.EaseInOutQuad (baseY, maxY, howFar);
		} else {
			newValue = EasingFunction.EaseInOutQuad (maxY, baseY, howFar);
		}
		this.transform.localPosition = new Vector3 (this.transform.localPosition.x, newValue, 0);

	}
}
