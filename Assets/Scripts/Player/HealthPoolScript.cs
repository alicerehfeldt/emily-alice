using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class HealthPoolScript : GameObjectScript {
	const int DEFAULT_HEALTH = 5;
	public int health;
	// Use this for initialization
	public override void Start () {
		base.Start ();

		health = DEFAULT_HEALTH;
	}

	public void GiveHearts(int amount) {
		theGame.sfx.Play ("health");
		health += amount;
	}

	public void TakeDamage(int amount) {
		if (amount >= health) {
			health = 0;
			GameOver ();
		}
		health -= amount;
		aliceScript.DamageTaken ();
		emilyScript.DamageTaken ();
	}

	void GameOver() {
		theGame.GameOver ();
	}

}
