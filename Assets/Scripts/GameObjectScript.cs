using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectScript : MonoBehaviour {

	protected GameScript theGame;
	protected HealthPoolScript healthPool;

	public virtual void Start () {
		theGame = GetComponentInParent<GameScript> ();
		healthPool = GetComponentInParent<HealthPoolScript> ();
	}

}
