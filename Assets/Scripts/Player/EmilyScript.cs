using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmilyScript : PlayerScript {
	public Camera parentCamera;

	Vector2 cameraSize;

	float leftBorder;
	float rightBorder;
	float topBorder;
	float bottomBorder;

	const float MAX_Y = 0.73f;
	const float MIN_Y = -3.85f;
	const float MAX_X = 5.75f;
	const float MIN_X = -5.75f;

	float MOVE_SPEED = 0.1f;

	bool isFacingRight;
	Vector3 moveTarget;

	public GameObject MoveTargetIndicator;

	SpriteRenderer spriteRenderer;

	public bool useSecondMove = false;

	public Sprite EMILY_IDLE_LEFT;
	public Sprite EMILY_IDLE_RIGHT;
	public Sprite EMILY_MOVE_1_LEFT;
	public Sprite EMILY_MOVE_1_RIGHT;
	public Sprite EMILY_MOVE_2_LEFT;
	public Sprite EMILY_MOVE_2_RIGHT;
	public Sprite EMILY_ATTACK_LEFT;
	public Sprite EMILY_ATTACK_RIGHT;
	public Sprite EMILY_DAMAGE_LEFT;
	public Sprite EMILY_DAMAGE_RIGHT;


	public override void Start () {
		base.Start ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
		isFacingRight = true;
		moveTarget = new Vector3 (transform.localPosition.x, transform.localPosition.y, 0);

		StartCoroutine (ToggleMoveSprite ());
	}

	IEnumerator ToggleMoveSprite() {
		yield return new WaitForSeconds (0.4f);
		useSecondMove = !useSecondMove;
		StartCoroutine (ToggleMoveSprite ());
	}

	public void IsDragging() {
		Vector3 position =  parentCamera.ScreenToWorldPoint(Input.mousePosition);
		UpdateMoveTarget (new Vector3 (position.x, position.y, 0));

	}

	public void FaceDirection(Vector3 position) {
		isFacingRight = (position.x > transform.position.x);
	}

	void OnCollisionEnter2D(Collision2D collision) {
		Debug.Log ("EMILY COLLISION ENTER");
		if (invulnerable) {
			return;
		}
		GameObject bug = collision.collider.gameObject;
		BugScript bugScript = bug.GetComponent<BugScript> ();
		if (bugScript.lassoed) {
			return;
		}


		healthPool.TakeDamage (1);
		aliceScript.ResetAttack ();


		bugScript.BackAwayFromEmily ();

	}

	// Update is called once per frame
	void Update () {
		if (theGame.finalBossStage.Equals ("ENTER")) {
			Vector3 target = new Vector3 (-5.0f, -1.0f, 0);
			moveTarget = target;
			if (this.transform.localPosition.Equals (target)) {
				isFacingRight = true;
				isMoving = false;
			} else {
				isMoving = true;
				FaceDirection (target);
				this.transform.localPosition = Vector3.MoveTowards (this.transform.localPosition, target, 0.4f);
			}
			return;
		} else {
			if (transform.localPosition.Equals (moveTarget)) {
				isMoving = false;
			} else {
				FaceDirection (moveTarget);
				this.transform.localPosition = Vector3.MoveTowards (this.transform.localPosition, moveTarget, MOVE_SPEED);
			}
		}


		if (isFacingRight) {
			if (justTakenDamage) {
				spriteRenderer.sprite = EMILY_DAMAGE_RIGHT;
			} else if (isAttacking) {
				spriteRenderer.sprite = EMILY_ATTACK_RIGHT;
			} else if (isMoving) {
				if (useSecondMove) {
					spriteRenderer.sprite = EMILY_MOVE_2_RIGHT;
				} else {
					spriteRenderer.sprite = EMILY_MOVE_1_RIGHT;
				}
			} else {
				spriteRenderer.sprite = EMILY_IDLE_RIGHT;
			}
		} else {
			if (justTakenDamage) {
				spriteRenderer.sprite = EMILY_DAMAGE_LEFT;
			} else if (isAttacking) {
				spriteRenderer.sprite = EMILY_ATTACK_LEFT;
			} else if (isMoving) {
				if (useSecondMove) {
					spriteRenderer.sprite = EMILY_MOVE_2_LEFT;
				} else {
					spriteRenderer.sprite = EMILY_MOVE_1_LEFT;
				}
			} else {
				spriteRenderer.sprite = EMILY_IDLE_LEFT;
			}
		}



	}

	public void UpdateMoveTarget(Vector3 target) {

		// Create an indicator and attach it to our parent
		GameObject indicator = Instantiate(MoveTargetIndicator, this.transform.parent);
		indicator.transform.localPosition = target;

		// offset the target y based on half the height of the sprite
		float spriteHeight = spriteRenderer.bounds.size.y;
		target.y += spriteHeight / 2;

		float max_x = MAX_X;

		// limit movement during final boss fight
		if (theGame.finalBossStage.Equals ("BUGS") || theGame.finalBossStage.Equals ("SEQUENCE")) {
			max_x = 0f;
		}

		if (target.x < MIN_X) {
			target.x = MIN_X;
		} else if (target.x > max_x) {
			target.x = max_x;
		}
		if (target.y < MIN_Y) {
			target.y = MIN_Y;
		} else if (target.y > MAX_Y) {
			target.y = MAX_Y;
		}			

		target.z = 0;
		moveTarget = target;
		isMoving = true;





	}
}
