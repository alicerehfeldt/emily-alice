using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : GameObjectScript {

	void TakeDamage(int amount) {
		healthPool.TakeDamage (amount);
	}
}
