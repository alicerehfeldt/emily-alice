using System.Collections;
using System.Collections.Generic;
using GameConstants;
using UnityEngine;

public class FinalBossScript : BugScript {

	public bool isEntering = false;
	float endEnterTime;
	float enterDuration = 5.0f;
	float inputAnimationDuration = 2.0f;
	bool isAnimationSequence = false;
	float endInputTime;

	InputSequenceScript inputSequenceScript;

	public override void Start() {
		base.Start ();

		// wipe out sequence for now
		sequence.Clear();
		inputSequenceScript = GetComponentInChildren<InputSequenceScript> ();
	}

	// Update is called once per frame
	void Update () {
		if (theGame.finalBossStage.Equals ("ENTER")) {
			float now = Time.time;
			if (now > endEnterTime) {
				isEntering = false;
				return;
			}
			float howMuch = (endEnterTime - now) / enterDuration;
			float positionY = EasingFunction.EaseOutQuad (-1.5f, 4f, howMuch);
			this.transform.localPosition = new Vector3 (this.transform.localPosition.x, positionY, 0);
		} else if (theGame.finalBossStage.Equals ("BUGS") || theGame.finalBossStage.Equals ("SEQUENCE")) {
			this.transform.localPosition = new Vector3 (this.transform.localPosition.x, -1.5f, 0);

			if (theGame.finalBossStage.Equals ("SEQUENCE") && !isAnimationSequence) {
				CheckForKey ();
			}
		} else if (theGame.finalBossStage.Equals ("DYING")) {
			SpriteRenderer sr = GetComponent<SpriteRenderer> ();
			sr.flipY = true;
			Vector3 target = this.transform.localPosition;
			target.y = -7.4f;
			this.transform.localPosition = Vector3.MoveTowards (this.transform.localPosition, target, 0.05f);
		}

		if (isAnimationSequence) {
			float now = Time.time;

			Vector3 target = new Vector3 (-6.5f, 5.5f, 0f);
			if (now > endInputTime) {
				isAnimationSequence = false;
				inputSequenceScript.transform.localPosition = target;
			} else {
				float howMuch = (endInputTime - now) / inputAnimationDuration;
				float positionX = EasingFunction.EaseOutQuad (-6.5f, 0f, howMuch);
				float positionY = EasingFunction.EaseOutQuad (5.5f, 0f, howMuch);
				inputSequenceScript.transform.localPosition = new Vector3 (positionX, positionY, 0f);
			}
		}


	}

	public void GrandEntrance() {
		endEnterTime = Time.time + enterDuration;
		isEntering = true;
	}

	public void SequenceTime(int count) {
		GenerateSequence (count);
		inputSequenceScript.ReplaceBugSequence (sequence);
		inputSequenceScript.playerSequence.Clear ();
		isAnimationSequence = true;
		endInputTime = Time.time + inputAnimationDuration;
	}

	void CheckForKey() {
		if (Input.GetKeyUp (KeyCode.W) || Input.GetKeyUp (KeyCode.UpArrow)) {
			NewDirection(BUG_DIRECTION.UP);
		} else if (Input.GetKeyUp (KeyCode.A) || Input.GetKeyUp (KeyCode.LeftArrow)) {
			NewDirection(BUG_DIRECTION.LEFT);
		} else if (Input.GetKeyUp (KeyCode.S) || Input.GetKeyUp (KeyCode.DownArrow)) {
			NewDirection(BUG_DIRECTION.DOWN);
		} else if (Input.GetKeyUp (KeyCode.D) || Input.GetKeyUp (KeyCode.RightArrow)) {
			NewDirection(BUG_DIRECTION.RIGHT);
		}
	}

	void NewDirection(BUG_DIRECTION pressed) {
		BUG_DIRECTION keyNeeded = inputSequenceScript.bugSequence[inputSequenceScript.playerSequence.Count];
		if (pressed == keyNeeded) {
			aliceScript.isAttacking = true;
			inputSequenceScript.playerSequence.Add (pressed);
			if (inputSequenceScript.playerSequence.Count.Equals (inputSequenceScript.bugSequence.Count)) {
				aliceScript.Say ("getFucked");
				theGame.BugKilled ();
				StartCoroutine (ClearSequence ());
			}
		} else {
			aliceScript.isAttacking = false;
			inputSequenceScript.playerSequence.Clear ();


		}
	}


	IEnumerator ClearSequence() {
		yield return new WaitForSeconds (0.5f);
		inputSequenceScript.playerSequence.Clear ();
		inputSequenceScript.ReplaceBugSequence (new List<BUG_DIRECTION> ());
	}

}
