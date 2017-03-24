using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LassoScript : GameObjectScript {
	const float MAX_ROPE = 11f;
	LineRenderer lineRenderer;
	public bool isDrawing;
	List<Vector3> points;

	public bool haveBugLassoed;
	public float drawDistance;
	public bool saySomething = false;
	bool didCollide = false;

	// Use this for initialization
	public override void Start () {
		base.Start ();

		haveBugLassoed = false;
		points = new List<Vector3> ();
		lineRenderer = GetComponent<LineRenderer> ();
		lineRenderer.sortingLayerName = "Emily World Lasso";
		lineRenderer.material.color = Color.red;
		isDrawing = false;
		drawDistance = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void ClearLine() {
		points.Clear ();
		lineRenderer.numPositions = 0;
		lineRenderer.startColor = Color.yellow;
		lineRenderer.endColor = Color.yellow;
		drawDistance = 0f;
		isDrawing = false;
		didCollide = false;
	}

	public void StartDrawing() {
		// Cannot lasso when queue is full
		if (theGame.bugQueueScript.queueFull) {
			emilyScript.Say ("queueBackup", 3.0f);
			return;
		}

		ClearLine ();
		isDrawing = true;
		emilyScript.isAttacking = true;
	}

	public void IsDrawing() {
		if (!isDrawing) {
			return;
		}

		Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		mousePos.z = 0;
		if (points.Contains (mousePos)) {
			return;
		} else if (points.Count > 0) {
			Vector3 lastPoint = points [points.Count - 1];
			float added = Vector3.Distance (lastPoint, mousePos);
			drawDistance += added;
		}
			
		if (drawDistance >= MAX_ROPE) {
			emilyScript.Say ("rope", 2.0f);
			return;
		}

		points.Add (mousePos);
		lineRenderer.numPositions = points.Count;
		lineRenderer.SetPosition (points.Count - 1, mousePos);
		emilyScript.FaceDirection (mousePos);

		if (!didCollide) {
			int[] points = CheckLineForCollision ();
			if (!(points == null)) {
				didCollide = true;
				lineRenderer.startColor = Color.magenta;
				lineRenderer.endColor = Color.magenta;
			}
		}
	}

	public void StoppedDrawing() {
		if (isDrawing) {
			int[] circlePoints = CheckLineForCollision ();
			if (!(circlePoints == null)) {
				HandleCircleDrawn (circlePoints[0], circlePoints[1]);
			}
		}
		isDrawing = false;
		ClearLine ();
		emilyScript.isAttacking = false;
	}

	struct Line {
		public Vector3 StartPoint;
		public Vector3 EndPoint;
	}

	int[] CheckLineForCollision() {
		if (points.Count < 3) {
			return null;
		}

		List<Line> lines = new List<Line>();
		for (int i = 0; i < points.Count; i++) {
			int j = i + 1;
			if (j >= points.Count) {
				break;
			}
			Line line = new Line ();
			line.StartPoint = points [i];
			line.EndPoint = points [j];
			lines.Add (line);
		}

		for (int i = 0; i < lines.Count; i++) {
			Line firstLine = lines [i];
			for (int j = (lines.Count - 1); j > i; j--) {
				Line secondLine = lines [j];
				if (LinesIntersect (firstLine, secondLine)) {
					int[] val = new int[2];
					val[0] = i;
					val[1] = j+1;
					return val;
				}
			}
		}
		return null;

	}

	bool LinesIntersect(Line a, Line b) {
		if (Vector3.Equals (a.StartPoint, b.StartPoint) ||
		    Vector3.Equals (a.StartPoint, b.EndPoint) ||
		    Vector3.Equals (a.EndPoint, b.StartPoint) ||
		    Vector3.Equals (a.EndPoint, b.EndPoint)) {
			return false;
		}

		return((Mathf.Max (a.StartPoint.x, a.EndPoint.x) >= Mathf.Min (b.StartPoint.x, b.EndPoint.x)) &&
			(Mathf.Max (b.StartPoint.x, b.EndPoint.x) >= Mathf.Min (a.StartPoint.x, a.EndPoint.x)) &&
			(Mathf.Max (a.StartPoint.y, a.EndPoint.y) >= Mathf.Min (b.StartPoint.y, b.EndPoint.y)) &&
			(Mathf.Max (b.StartPoint.y, b.EndPoint.y) >= Mathf.Min (a.StartPoint.y, a.EndPoint.y))
         );
	}

	void HandleCircleDrawn (int startIndex, int endIndex) {
		int numPoints = endIndex - startIndex;

		List<Vector3> pointsInPolygon = points.GetRange (startIndex, numPoints);
		// Hide the points in the line outside our polygon
//		lineRenderer.SetPositions (pointsInPolygon.ToArray());

		// Okay we got our circle, now we see if it intersects any bugs
		// First find the centroid
		Vector3 centroid = new Vector3(0, 0, 0);
		for (int k = 0; k < pointsInPolygon.Count; k++) {
			centroid.x += pointsInPolygon [k].x;
			centroid.y += pointsInPolygon [k].y;
		}

		centroid.x = centroid.x / numPoints;
		centroid.y = centroid.y / numPoints;
		// Now we iterate through bugs and see if this centroid is contained by any bug's collider
		List<GameObject> bugs = theGame.bugFarmScript.bugs;

		for (int k = 0; k < bugs.Count; k++) {
			GameObject bug = bugs [k];
			BoxCollider2D collider = bug.GetComponent<BoxCollider2D> ();
			if (collider.bounds.Contains (centroid)) {
				HandleBugLassoed (bug);
				return;
			}
		}

		// If we get here we did not wrangle any bugs, so can hide the line
		ClearLine();
	}

	void HandleBugLassoed (GameObject bug) {
		Debug.Log ("LASSOED THIS BUG: " + bug.ToString());
		BugScript bugScript = bug.GetComponent<BugScript>();
		bugScript.lassoed = true;
		Destroy(bugScript.GetComponent<BoxCollider2D>());
		haveBugLassoed = true;
		emilyScript.DefeatedEnemy ();
		StartCoroutine(ClearLineAfterLasso ());
		if (saySomething) {
			int rand = theGame.random (100);
			if (rand > 66) {
				emilyScript.Say ("getFucked");
			} else {
				emilyScript.Say ("hokay");
			}
		}
		saySomething = !saySomething;


	}

	IEnumerator ClearLineAfterLasso() {
		yield return new WaitForSeconds (0.3f);
		if (!isDrawing) {
			ClearLine ();
		}
	}
}

