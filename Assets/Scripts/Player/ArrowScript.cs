using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameConstants;

public class ArrowScript : MonoBehaviour {

	public BUG_DIRECTION direction;
	public Sprite UP;
	public Sprite DOWN;
	public Sprite LEFT;
	public Sprite RIGHT;


	void Awake() {
		direction = BUG_DIRECTION.UP;
	}

	void Start() {
	}

	public void Direction(BUG_DIRECTION newDirection) {
		direction = newDirection;
	}

	void Update() {
		Sprite newSprite = GetSprite ();
		SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer> ();
		spriteRenderer.sprite = newSprite;

	}

	Sprite GetSprite() {
		switch (direction) {
		case BUG_DIRECTION.UP: 
			return UP;
		case BUG_DIRECTION.DOWN: 
			return DOWN;
		case BUG_DIRECTION.LEFT: 
			return LEFT;
		}
		return RIGHT;

	}

}
