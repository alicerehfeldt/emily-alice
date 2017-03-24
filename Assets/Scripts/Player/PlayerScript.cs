using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : GameObjectScript {
	public bool invulnerable = false;
	public bool justTakenDamage = false;
	public bool justDefeatedEnemy = false;
	public bool isAttacking = false;
	public bool isMoving = false;
	protected bool isAlice = false;

	const float TIME_DAMAGE_TAKEN = 1.0f;
	const float TIME_DEFEATED_ENEMY = 1.0f;


	public GameObject SpeechBubble;

	void TakeDamage(int amount) {
		healthPool.TakeDamage (amount);
	}

	public void DamageTaken() {
		invulnerable = true;
		justTakenDamage = true;
		StartCoroutine (ResetDamageTaken ());
		int rand = theGame.random (100);
		if (isAlice) {
			if (rand > 80) {
				Say ("jesus", 1.0f);
			} else if (rand > 50) {
				Say ("fuck", 1.0f);
			} else {
				Say ("sob", 1.0f);
			}
		} else {
			if (rand > 50) {
				Say ("fuck", 1.0f);
			}
		}
	}

	IEnumerator ResetDamageTaken() {
		yield return new WaitForSeconds (TIME_DAMAGE_TAKEN);
		justTakenDamage = false;
		invulnerable = false;
	}

	public void DefeatedEnemy() {
		isAttacking = false;
		justDefeatedEnemy = true;
		StartCoroutine (ResetDefeatedEnemy ());
	}

	IEnumerator ResetDefeatedEnemy() {
		yield return new WaitForSeconds (TIME_DEFEATED_ENEMY);
		justDefeatedEnemy = false;
	}

	public void Say(string name, float life = 2.0f) {
		// check if there is an exist speech bubble, if so destroy it

		SpeechBubbleScript existing = GetComponentInChildren<SpeechBubbleScript> ();
		if (!(existing == null)) {
			Destroy (existing.gameObject);
		}

		GameObject bubble = Instantiate (SpeechBubble, transform);
		SpeechBubbleScript bubbleScript = bubble.GetComponent<SpeechBubbleScript> ();
		bubbleScript.SetSortingLayer (isAlice);
		bubbleScript.Show (name, life);
		this.PositionSpeechBubble(bubbleScript);
		float positionY = 0.6f;
		if (name.Equals ("queueBackup")) {
			positionY = 1.3f;
		}

		bubbleScript.transform.localPosition = new Vector3 (1.1f, positionY, 0f);
	}

	void PositionSpeechBubble(SpeechBubbleScript bubbleScript) {

	}

}
