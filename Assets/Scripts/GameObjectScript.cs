using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectScript : MonoBehaviour {
	protected const float EMILY_WORLD_CEIL = 0.67f;
	protected const float EMILY_WORLD_FLOOR = -3.75f;
	protected const float EMILY_WORLD_LEFT = -5.2f;
	protected const float EMILY_WORLD_RIGHT = 5.25f;

	protected GameScript theGame;
	protected HealthPoolScript healthPool;
	protected AliceScript aliceScript;
	protected EmilyScript emilyScript;

	public virtual void Start () {
		theGame = GetComponentInParent<GameScript> ();
		healthPool = GetComponentInParent<HealthPoolScript> ();
		aliceScript = theGame.GetComponentInChildren<AliceScript> ();
		emilyScript = theGame.GetComponentInChildren<EmilyScript> ();
	}

}
