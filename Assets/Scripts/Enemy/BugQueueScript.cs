using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameConstants;

public class BugQueueScript : GameObjectScript {
	public List<GameObject> bugs;

	public GameObject BugInputSequence;

	public InputSequenceScript bugInputSequence;

	public bool queueFull;
	const float BUG_MARGIN_RIGHT = 0.1f;
	const int QUEUE_LIMIT = 4;

	bool bugDying;

	void Awake() {
		bugs = new List<GameObject> { };
		bugDying = false;
		queueFull = false;
	}

	public override void Start () {
		base.Start ();

		if (BugInputSequence != null) {
			bugInputSequence = BugInputSequence.GetComponent<InputSequenceScript> ();
		}


//		GameObject testBug = Instantiate (theGame.bugFarmScript.BUG_MEDIUM_EASY, this.transform);
//		AddBug (testBug);
//		testBug = Instantiate (theGame.bugFarmScript.BUG_MEDIUM_EASY, this.transform);
//		AddBug (testBug);
//		GameObject testBug = Instantiate (theGame.bugFarmScript.BUG_MEDIUM_NORMAL, this.transform);
//		AddBug (testBug);
//		testBug = Instantiate (theGame.bugFarmScript.BUG_MEDIUM_EASY, this.transform);
//		AddBug (testBug);
	}


	public void AddBug(GameObject bug) {
		bugs.Add (bug);
		bug.transform.parent = transform;
		SpriteRenderer sr = bug.GetComponent<SpriteRenderer> ();
		sr.sortingLayerName = "Alice World Bugs";
		SetFirstBugSequence ();
		SetBugPositionsX ();
		// Put it in the ceiling
		BugScript bugScript = bug.GetComponent<BugScript>();
		bug.transform.localPosition = new Vector2 (bugScript.queuePositionX, 2.0f);
		bug.transform.localScale = new Vector3(1f, 1f, 1f);
	}

	public void RemoveFirstBug() {
		bugDying = false;
		GameObject deadBug = bugs [0];
		bugs.Remove (deadBug);
		Destroy (deadBug);
		theGame.BugKilled ();
		SetFirstBugSequence ();
		bugInputSequence.playerSequence.Clear ();
		SetBugPositionsX ();
		theGame.sfx.Play ("defeated");
		int rand = theGame.random (100);
		if (rand > 60) {
			aliceScript.Say("getFucked");
		}
	}

	void SetFirstBugSequence() {
		if (bugs.Count > 0) {
			BugScript bugScript = bugs [0].GetComponent<BugScript> ();
			bugInputSequence.ReplaceBugSequence (bugScript.sequence);
		} else {
			bugInputSequence.bugSequence = new List<BUG_DIRECTION> ();
		}
	}

	void SetBugPositionsX() {
		float positionX = 0;
		for (int i = 0; i < bugs.Count; i++) {
//			Debug.Log ("Bug " + i + " position x: " + positionX);
			BugScript bugScript = bugs [i].GetComponent<BugScript> ();
			// Get sprite renderer from this bug
			SpriteRenderer thisSprite = bugs[i].GetComponent<SpriteRenderer>();
			float halfOfThisSprite = thisSprite.bounds.size.x / 2;
			if (i > 0) {
				positionX += halfOfThisSprite;
			}

			bugScript.QueuePosition (positionX);

			positionX += halfOfThisSprite + BUG_MARGIN_RIGHT;
		}
		queueFull = bugs.Count >= QUEUE_LIMIT;
	}



	// Update is called once per frame
	void Update () {
		CheckForKey ();
	}


	void CheckForKey() {
		if (theGame.finalBossStage.Equals ("SEQUENCE")) {
			return;
		}

		if (bugDying || bugs.Count.Equals(0)) {
			return;
		}
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
		BUG_DIRECTION keyNeeded = bugInputSequence.bugSequence [bugInputSequence.playerSequence.Count];
		if (pressed == keyNeeded) {
			aliceScript.isAttacking = true;
			bugInputSequence.playerSequence.Add (pressed);

			if (bugInputSequence.playerSequence.Count.Equals (bugInputSequence.bugSequence.Count)) {
				BugScript bugScript = bugs [0].GetComponent<BugScript> ();
				bugScript.Kill (this);
				aliceScript.DefeatedEnemy ();
				bugDying = true;
			}
		} else {
			// Player entered wrong arrow, reset input
			aliceScript.ResetAttack();
			// TODO: play bad input sound here
		}
	}
}
