using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameConstants;

public class InputSequenceScript : GameObjectScript {

	public List<BUG_DIRECTION> bugSequence;
	public List<BUG_DIRECTION> playerSequence;
	public GameObject arrow;
	public GameObject highlight;

	List<GameObject> arrowChildren;
	List<GameObject> highlightChildren;

	void Awake() {
		bugSequence = new List<BUG_DIRECTION> ();
		playerSequence = new List<BUG_DIRECTION> ();
		arrowChildren = new List<GameObject> ();
		highlightChildren = new List<GameObject> ();
	}

	public void ReplaceBugSequence(List<BUG_DIRECTION> newSequence) {
		bugSequence = newSequence;
	}

	public void AddPlayerInput(BUG_DIRECTION direction) {
		playerSequence.Add (direction);
	}

	public void UpdateKids() {
		if (arrowChildren.Count < bugSequence.Count) {
			int difference = bugSequence.Count - arrowChildren.Count;
			for (int i = 0; i < difference; i++) {
				GameObject newArrow = (GameObject)Instantiate (arrow, this.transform);
				newArrow.transform.localScale = new Vector3 (1f, 1f, 0);
				SpriteRenderer sr = newArrow.GetComponent<SpriteRenderer> ();
				sr.sortingLayerName = "Alice World Arrows";
				sr.sortingOrder = 1;
				arrowChildren.Add (newArrow);
			}
		} else if (arrowChildren.Count > bugSequence.Count) {
			int difference = arrowChildren.Count - bugSequence.Count;
			for (int i = 0; i < difference; i++) {
				GameObject isaac = arrowChildren [0];
				arrowChildren.RemoveAt (0);
				Destroy (isaac);
			}
		}

		if (highlightChildren.Count < playerSequence.Count) {
			int difference = playerSequence.Count - highlightChildren.Count;
			for (int i = 0; i < difference; i++) {
				GameObject newChild = (GameObject)Instantiate (highlight, this.transform);
				newChild.transform.localScale = new Vector3 (1f, 1f, 0f);
				SpriteRenderer sr = newChild.GetComponent<SpriteRenderer> ();
				sr.sortingLayerName = "Alice World Arrows";
				sr.sortingOrder = 0;
				highlightChildren.Add (newChild);
			}
		} else if (highlightChildren.Count > playerSequence.Count) {
			int difference = highlightChildren.Count - playerSequence.Count;
			for (int i = 0; i < difference; i++) {
				GameObject isaac = highlightChildren [0];
				highlightChildren.RemoveAt (0);
				Destroy (isaac);
			}
		}
	}

	void Update () {
		UpdateKids ();
		float positionX = 0f;
		for (int i = 0; i < bugSequence.Count; i++) {
			// Position the arrow
			arrowChildren [i].transform.localPosition = new Vector3 (positionX, 0f, 0f);

			// Update its direction
			ArrowScript arrowScript = arrowChildren[i].GetComponent<ArrowScript>();
			arrowScript.Direction (bugSequence [i]);

			if (i < playerSequence.Count) {
				highlightChildren [i].transform.localPosition = new Vector3 (positionX, 0f, 0f);
			}

			positionX += 1f;
		}
	}
}
