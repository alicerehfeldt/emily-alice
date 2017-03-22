using System;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour
{
	System.Random rng;

	public GameScript ()
	{



	}

	void Awake() {
		rng = new System.Random ();
	}


	public int random(int max) {
		return rng.Next (max);
	}

	public void GameOver() {
	}

}


