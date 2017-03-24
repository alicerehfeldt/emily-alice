using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour {
	bool canProceed = false;
	// Use this for initialization
	void Start () {
		StartCoroutine(Unlock());
	}

	IEnumerator Unlock() {
		yield return new WaitForSeconds (5.0f);
		canProceed = true;
	}

	// Update is called once per frame
	void Update () {
		if (canProceed && Input.GetMouseButtonDown (0)) {
			SceneManager.LoadScene ("Title Screen");
		} else if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit ();
		}
	}
}
