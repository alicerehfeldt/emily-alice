using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameConstants;

public class BugScript : GameObjectScript {

	const int PATIENCE_DEFAULT = 100;
	// time in milliseconds
	const float DEATH_TIME = 1.0f;

	const float LASSO_FLOAT_SPEED = 0.1f;
	const float QUEUE_MOVE_SPEED = 0.1f;

	const float CEILING_LOCAL_Y = 3.0f;

	public float MOVE_SPEED = 0.02f;

	public BUG_SIZE size = BUG_SIZE.SMALL;
	public BUG_DIFFICULTY difficulty = BUG_DIFFICULTY.EASY;
	public List<BUG_DIRECTION> sequence;
	private List<BUG_DIRECTION> directions;

	public bool dying;
	public bool lassoed;
	public bool inQueue;
	public int patience;

	public float queuePositionX;

	public Vector3 moveTarget;

	SpriteRenderer spriteRenderer;

	void Awake () {
		moveTarget = new Vector2 ();
		sequence = new List<BUG_DIRECTION> { };
		directions = new List<BUG_DIRECTION> { };
		dying = false;
		inQueue = false;
		queuePositionX = 0f;
	}
	// Use this for initialization
	public override void Start () {
		base.Start ();
		this.GenerateSequence ();
		patience = PATIENCE_DEFAULT;
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}

	private void ResetDirections() {
		directions.Clear ();
		directions.Add (BUG_DIRECTION.UP);
		directions.Add (BUG_DIRECTION.LEFT);
		directions.Add (BUG_DIRECTION.DOWN);
		directions.Add (BUG_DIRECTION.RIGHT);
		if (difficulty.Equals(BUG_DIFFICULTY.NORMAL) || difficulty.Equals(BUG_DIFFICULTY.EASY)) {
			// remove 1
			int index = theGame.random(4);
			directions.RemoveAt (index);

			if (difficulty.Equals(BUG_DIFFICULTY.EASY)) {
				// REMOVE ANOTHER
				index = theGame.random(3);
				directions.RemoveAt(index);
			}
		}
	}

	protected void GenerateSequence(int sequenceLength = -1) {
		this.ResetDirections ();
		sequence.Clear ();
		int numDirections = directions.Count;

		// TODO: Revisit this

		if (sequenceLength.Equals (-1)) {
			sequenceLength = (int)size * 3;
		}

		for (int i = 0; i < sequenceLength; i++) {
			int index = theGame.random (numDirections);	
			sequence.Add (directions [index]);
		}
	}

	public void Kill(BugQueueScript queue) {
		dying = true;

		StartCoroutine (HandleDying(queue));
	}

	void OnCollisionEnter2D(Collision2D collision) {
		Debug.Log ("BUG COLLISION");
	}


	IEnumerator HandleDying(BugQueueScript queue) {
		yield return new WaitForSeconds (DEATH_TIME);
		queue.RemoveFirstBug ();
	}

	// Update is called once per frame
	void Update () {
		if (dying || lassoed) {
			spriteRenderer.flipY = true;
		} else {
			spriteRenderer.flipY = false;
		}

		if (lassoed) {
			if (this.transform.localPosition.y >= CEILING_LOCAL_Y) {
				SendToBugQueue ();
				return;
			}

			Vector3 target = new Vector3 (0, CEILING_LOCAL_Y, 0);
			this.transform.localPosition = Vector3.MoveTowards (this.transform.localPosition, target, LASSO_FLOAT_SPEED);
		} else if (inQueue) {
			float positionY = 0f;
			if (size.Equals (BUG_SIZE.MEDIUM)) {
				positionY = 0.2f;
			}

			Vector3 target = new Vector3 (queuePositionX, positionY, 0);
			this.transform.localPosition = Vector3.MoveTowards (this.transform.localPosition, target, QUEUE_MOVE_SPEED);
			return;
		} else {
			// normal movement
			if (this.transform.localPosition.Equals (moveTarget)) {
				GenerateNewMoveTarget ();
			}

			this.transform.localPosition = Vector3.MoveTowards (this.transform.localPosition, moveTarget, MOVE_SPEED);

		}
	}

	void GenerateNewMoveTarget() {
		if (difficulty.Equals (BUG_DIFFICULTY.EASY)) {
			// Are we on the left half of the screen?
			float currentX = this.transform.localPosition.x;
			bool isOnRight = currentX > 0;
			float percent = (float)(theGame.random (50) + 50);
			// Move this percentage between us and the wall
			float distance = 0;
			if (!isOnRight) {
				distance = EMILY_WORLD_RIGHT - currentX;
			} else {
				distance = currentX - EMILY_WORLD_LEFT;
			}
			distance = distance * (percent * 0.01f);
			if (isOnRight) {
				distance = distance * -1;
			}
			Vector3 newTarget = new Vector3 (
				                    currentX + distance,
				                    transform.localPosition.y,
				                    0);

			setMoveTarget (newTarget);
		} else if (difficulty.Equals (BUG_DIFFICULTY.NORMAL)) {
			// Seek after emily
			moveTarget = emilyScript.transform.localPosition;
			setMoveTarget (emilyScript.transform.localPosition);
		}
	}

	public void BackAwayFromEmily() {
		float emilyX = emilyScript.transform.localPosition.x;
		float moveX = transform.localPosition.x;
		if (moveX > emilyX) {
			moveX += 1.5f;
		} else {
			moveX -= 1.5f;
		}
		setMoveTarget (new Vector3 (moveX, moveTarget.y, 0));
	}

	public void setMoveTarget(Vector3 target) {
		if ( target.x > EMILY_WORLD_RIGHT) {
			target.x = EMILY_WORLD_RIGHT;
		} else if (target.x < EMILY_WORLD_LEFT) {
			target.x = EMILY_WORLD_LEFT;
		}
		if ( target.y > EMILY_WORLD_CEIL) {
			target.y = EMILY_WORLD_CEIL;
		} else if (target.y < EMILY_WORLD_FLOOR) {
			target.y = EMILY_WORLD_FLOOR;
		}
		moveTarget = target;
	}

	void IsLassoed() {
		lassoed = true;
	}

	void SendToBugQueue() {
		theGame.bugFarmScript.bugs.Remove (gameObject);
		theGame.bugQueueScript.AddBug (gameObject);
		lassoed = false;
	}

	public void QueuePosition(float x) {
		inQueue = true;
		queuePositionX = x;
	}

}
