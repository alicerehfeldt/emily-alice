using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInputScript : MonoBehaviour {

	public GameObject Emily;
	public GameObject Rope;

	EmilyScript emilyScript;
	LassoScript ropeScript;

	public bool isDraggingEmily;

	public Camera parentCamera;
	GameScript theGame;

	void Awake() {
		isDraggingEmily = false;
	}

	// Use this for initialization
	void Start () {
		emilyScript = Emily.GetComponent<EmilyScript> ();
		ropeScript = Rope.GetComponent<LassoScript> ();
		theGame = GetComponent<GameScript> ();
	}


	void MouseDown() {
		Vector3 mousePosition = parentCamera.ScreenToWorldPoint (Input.mousePosition);
		RaycastHit2D hit = Physics2D.Raycast (mousePosition, Vector2.zero);
		if (hit.collider != null) {
			BoxCollider2D emilyCollider = Emily.GetComponent<BoxCollider2D> ();
			PolygonCollider2D lassoCollider = Emily.GetComponentInChildren<PolygonCollider2D> ();

			if (hit.collider.Equals(emilyCollider) || hit.collider.Equals(lassoCollider)){
				//Debug.Log("clicked lasso");
				ropeScript.StartDrawing();
				return;
			}

				
			// Iterate through bugs
//			List<GameObject> bugs = bugFarmScript.bugs;
//			for (int i = 0; i < bugs.Count; i++) {
//				BugScript bugScript = bugs [i].GetComponent<BugScript> ();
//				if (!bugScript.lassoed) {
//					continue;
//				}
//
//
//					
//			}
		} else {
			mousePosition.z = 0;
			emilyScript.UpdateMoveTarget (mousePosition);
		}


	}

	void MouseUp() {
		if (ropeScript.isDrawing) {
			ropeScript.StoppedDrawing ();
		}
		isDraggingEmily = false;
	}

	void Update () {
		if (theGame.finalBossStage.Equals("ENTER")) {
			return;
		}
		if (Input.GetMouseButtonDown (0)) {
			MouseDown ();
		} else if (Input.GetMouseButtonUp (0)) {
			MouseUp ();
		}


		if (isDraggingEmily) {
			emilyScript.IsDragging ();
		} else if (ropeScript.isDrawing) {
			ropeScript.IsDrawing ();
		}
	
	
	}
}
