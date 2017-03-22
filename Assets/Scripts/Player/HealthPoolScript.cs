using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class HealthPoolScript : GameObjectScript {
	const int DEFAULT_HEALTH = 100;
	private int health;

	// Use this for initialization

	public override void Start () {
		base.Start ();
		ResetToDefault ();
	}

	public void ResetToDefault() {
		health = DEFAULT_HEALTH;
	}

	public void TakeDamage(int amount) {
		if (amount > health) {
			health = 0;
			GameOver ();
		}
		health -= amount;
	}

	void GameOver() {
		theGame.GameOver ();
	}

}
