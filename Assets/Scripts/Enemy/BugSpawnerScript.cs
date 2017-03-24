using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameConstants;

public class BugSpawnerScript : MonoBehaviour {

	public GameObject BUG_SMALL_EASY;
	public GameObject BUG_MEDIUM_EASY;
	public GameObject BUG_LARGE_EASY;
	public GameObject BUG_SMALL_NORMAL;
	public GameObject BUG_MEDIUM_NORMAL;
	public GameObject BUG_LARGE_NORMAL;
	public GameObject BUG_SMALL_HARD;
	public GameObject BUG_MEDIUM_HARD;
	public GameObject BUG_LARGE_HARD;


	public List<GameObject> bugs;

	void Awake() {
		bugs = new List<GameObject> ();
	}

	// Use this for initialization
	void Start () {
//		GameObject firstBug = SpawnBug (BUG_SIZE.SMALL, BUG_DIFFICULTY.EASY);
//		firstBug.transform.localPosition = new Vector3(-4, -1, 0);
////
////
////		GameObject secondBug = SpawnBug (BUG_SIZE.SMALL, BUG_DIFFICULTY.NORMAL);
////		secondBug.transform.localPosition = new Vector3 (2, 2, 0);
////
//		GameObject third = SpawnBug (BUG_SIZE.MEDIUM, BUG_DIFFICULTY.NORMAL);
//		third.transform.localPosition = new Vector3 (-1, 3, 0);
	}


	public GameObject SpawnBug(BUG_SIZE size, BUG_DIFFICULTY difficulty) {
		
		if (size.Equals (BUG_SIZE.SMALL)) {
			if (difficulty.Equals (BUG_DIFFICULTY.EASY)) {
				return SpawnBug (BUG_SMALL_EASY);
			} else if (difficulty.Equals (BUG_DIFFICULTY.NORMAL)) {
				return SpawnBug (BUG_SMALL_NORMAL);
			} else if (difficulty.Equals (BUG_DIFFICULTY.HARD)) {
				return SpawnBug (BUG_SMALL_HARD);
			}
		} else if (size.Equals (BUG_SIZE.MEDIUM)) {
			if (difficulty.Equals (BUG_DIFFICULTY.EASY)) {
				return SpawnBug (BUG_MEDIUM_EASY);
			} else if (difficulty.Equals (BUG_DIFFICULTY.NORMAL)) {
				return SpawnBug (BUG_MEDIUM_NORMAL);
			} else if (difficulty.Equals (BUG_DIFFICULTY.HARD)) {
				return SpawnBug (BUG_MEDIUM_HARD);
			}			
		} else if (size.Equals (BUG_SIZE.LARGE)) {
			if (difficulty.Equals (BUG_DIFFICULTY.EASY)) {
				return SpawnBug (BUG_LARGE_EASY);
			} else if (difficulty.Equals (BUG_DIFFICULTY.NORMAL)) {
				return SpawnBug (BUG_LARGE_NORMAL);
			} else if (difficulty.Equals (BUG_DIFFICULTY.HARD)) {
				return SpawnBug (BUG_LARGE_HARD);
			}
		}
		return null;
	}

	GameObject SpawnBug(GameObject bugType) {
		GameObject newBug = Instantiate (bugType, this.transform);
		SpriteRenderer sr = newBug.GetComponent<SpriteRenderer> ();
		sr.sortingLayerName = "Emily World Bugs";
		bugs.Add (newBug);
		return newBug;
	}
}
