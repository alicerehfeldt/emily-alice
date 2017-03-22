using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameConstants;


public class BugScript : GameObjectScript {

	const int PATIENCE_DEFAULT = 100;

	public BUG_SIZE size = BUG_SIZE.SMALL;
	public BUG_DIFFICULTY difficulty = BUG_DIFFICULTY.EASY;
	public List<BUG_DIRECTION> sequence;
	private List<BUG_DIRECTION> directions;


	public int patience;

	void Awake () {
		sequence = new List<BUG_DIRECTION> { };
		directions = new List<BUG_DIRECTION> { };

	}
	// Use this for initialization
	public override void Start () {
		base.Start ();
		this.GenerateSequence ();
		patience = PATIENCE_DEFAULT;
	}

	private void ResetDirections() {
		directions.Clear ();
		directions.Add (BUG_DIRECTION.UP);
		directions.Add (BUG_DIRECTION.LEFT);
		directions.Add (BUG_DIRECTION.DOWN);
		directions.Add (BUG_DIRECTION.RIGHT);
		if (difficulty == BUG_DIFFICULTY.NORMAL || difficulty == BUG_DIFFICULTY.EASY) {
			// remove 1
			int index = theGame.random(4);
			directions.RemoveAt (index);

			if (difficulty == BUG_DIFFICULTY.EASY) {
				// REMOVE ANOTHER
				index = theGame.random(3);
				directions.RemoveAt(index);
			}
		}
	}

	private void GenerateSequence() {
		this.ResetDirections ();
		sequence.Clear ();
		int numDirections = directions.Count;

		// TODO: Revisit this

		int sequenceLength = (int) size * 3;

		for (int i = 0; i < sequenceLength; i++) {
			int index = theGame.random (numDirections);	
			sequence.Add (directions [index]);
		}
	}

	// Update is called once per frame
	void Update () {
		
	}



}
