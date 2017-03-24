using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarScript : GameObjectScript {

	List<GameObject> children;

	public GameObject Heart;

	public override void Start () {
		base.Start ();
		children = new List<GameObject> ();
	}

	void Update () {
		int numKids = children.Count;
		int health = healthPool.health;
		if (numKids.Equals (health) || health.Equals(0)) {
			return;
		} else if (numKids < healthPool.health) {
			int difference = health - numKids;
			for (int i = 0; i < difference; i++) {
				GameObject newArrow = (GameObject)Instantiate (Heart, this.transform);
				newArrow.transform.localScale = new Vector3 (1f, 1f, 0);
				SpriteRenderer sr = newArrow.GetComponent<SpriteRenderer> ();
				sr.sortingLayerName = "GUI";
				children.Add (newArrow);
			}
		} else if (numKids > health && numKids > 0) {
			int difference = numKids - health;
			for (int i = 0; i < difference; i++) {
				if (children.Count == 0) {
					break;
				}
				GameObject isaac = children [0];
				children.RemoveAt (0);
				Destroy (isaac);
			}
		}
		LineUpHearts ();
	}
	void LineUpHearts () {
		float positionX = 0f;
		for (int i = 0; i < children.Count; i++) {
			children [i].transform.localPosition = new Vector3 (positionX, 0, 0);
			positionX += 1f;
		}
	}
}
