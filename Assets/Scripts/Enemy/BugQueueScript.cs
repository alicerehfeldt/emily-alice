using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameConstants;

public class BugQueueScript : GameObjectScript {
	private List<GameObject> bugs;

	public GameObject BUG_SMALL_NORMAL;

	public List<BUG_DIRECTION> currentBugSequence;
	public List<BUG_DIRECTION> currentPlayerEntry;

	void Awake() {
		bugs = new List<GameObject> { };
	}

	public override void Start () {
		base.Start ();




		// TEMP CODE
		GameObject bug = (GameObject) Instantiate(BUG_SMALL_NORMAL, this.transform);
		AddBug (bug);

	}


	public void AddBug(GameObject bug) {
		bugs.Add (bug);
		bug.transform.parent = transform;
		float positionX = 0;
		float positionY = 0;
		if (bugs.Count == 0) {
			BugScript bugScript = bug.GetComponent<BugScript> ();
			currentBugSequence = bugScript.sequence;
		} else {
			// TODO position based on existing bugs here
		}
		bug.transform.localPosition = new Vector2 (positionX, positionY);
	}





	// Update is called once per frame
	void Update () {
		CheckForKey ();
	}


	void CheckForKey() {
		BUG_DIRECTION pressed = null;
		if (Input.GetKeyUp (KeyCode.W) || Input.GetKeyUp (KeyCode.UpArrow)) {
			pressed = BUG_DIRECTION.UP;
		} else if (Input.GetKeyUp (KeyCode.A) || Input.GetKeyUp (KeyCode.LeftArrow)) {
			pressed = BUG_DIRECTION.LEFT;
		} else if (Input.GetKeyUp (KeyCode.S) || Input.GetKeyUp (KeyCode.DownArrow)) {
			pressed = BUG_DIRECTION.DOWN;
		} else if (Input.GetKeyUp (KeyCode.D) || Input.GetKeyUp (KeyCode.RightArrow)) {
			pressed = BUG_DIRECTION.RIGHT;
		}

		if (pressed) {
			BUG_DIRECTION keyNeeded = currentBugSequence [currentPlayerEntry.Count];
			if (pressed == keyNeeded) {
			}
		}
	}
}
