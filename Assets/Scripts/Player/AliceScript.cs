using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliceScript : PlayerScript {

	public Sprite ALICE_BASE;
	public Sprite ALICE_DAMAGED;
	public Sprite ALICE_ATTACKING;
	public Sprite ALICE_DESTROYED_ENEMY;

	InputSequenceScript bugInputSequence;
	SpriteRenderer spriteRenderer;
	public override void Start () {
		base.Start ();
		isAlice = true;
		bugInputSequence = theGame.bugQueueScript.bugInputSequence;
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (justTakenDamage) {
			spriteRenderer.sprite = ALICE_DAMAGED;
		} else if (justDefeatedEnemy) {
			spriteRenderer.sprite = ALICE_DESTROYED_ENEMY;
		} else if (isAttacking) {
			spriteRenderer.sprite = ALICE_ATTACKING;
		} else {
			spriteRenderer.sprite = ALICE_BASE;
		}
	}

	public void ResetAttack () {
		isAttacking = false;
		// reset the player input sequence
		bugInputSequence.playerSequence.Clear();
	}
}
