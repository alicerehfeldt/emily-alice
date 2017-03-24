using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameConstants;

public class GameScript : MonoBehaviour
{
	System.Random rng;
	public BugQueueScript bugQueueScript;
	public BugSpawnerScript bugFarmScript;
	public SFXScript sfx;
	FinalBossScript finalBossScript;


	int wave = 0;
	bool waveComplete = false;
	public int bugsRemaining = 0;
	public string finalBossStage = "";

	AliceScript aliceScript;
	EmilyScript emilyScript;

	HealthPoolScript healthPool;
	void Awake() {
		rng = new System.Random ();
	}

	void Start() {
		Application.targetFrameRate = 60;
		sfx = GetComponent<SFXScript> ();
		bugQueueScript = GetComponentInChildren<BugQueueScript> ();
		bugFarmScript = GetComponentInChildren<BugSpawnerScript> ();
		healthPool = GetComponent<HealthPoolScript> ();
		finalBossScript = GetComponentInChildren<FinalBossScript> ();
		aliceScript = GetComponentInChildren<AliceScript> ();
		emilyScript = GetComponentInChildren<EmilyScript> ();
		NextWave (2.0f);
	}


	public int random(int max) {
		if (rng == null) {
			return 0;
		}
		return rng.Next (max);
	}

	public void GameOver() {
		SceneManager.LoadScene ("Game Over");
	}

	public void BugKilled() {
		// check if all bugs killed (and all sent in case player is fast)
		bugsRemaining--;
		if (bugsRemaining <= 0 && waveComplete) {
			NextWave (1.0f);
		}
	}

	void NextWave(float delay) {
		StartCoroutine (SendNextWave (delay));
	}

	IEnumerator SendNextWave(float delay) {
		yield return new WaitForSeconds (delay);

		wave++;
		Debug.Log("SENDING WAVE "+wave);
		switch(wave) {
		case 1:
			StartCoroutine (WaveOne ());
			break;
		case 2:
			StartCoroutine (WaveTwo ());
			break;
		case 3:
			StartCoroutine (WaveThree ());
			break;
		case 4:
			StartCoroutine (BossWaveOne ());
			break;
		case 5:
			BossWaveTwo ();
			break;
		case 6:
			BossWaveThree ();
			break;
		case 7:
			BossWaveFour ();
			break;
		case 8:
			StartCoroutine(YouWin ());
			break;
		}
	}

	// Two waves of easy bugs come in from the four corners
	IEnumerator WaveOne() {
		waveComplete = false;
		// upper right
		SpawnBug (BUG_SIZE.SMALL, BUG_DIFFICULTY.EASY, 
			new Vector3 (6f, 1.2f, 0f), new Vector3 (5f, 0.5f, 0f));

		// upper left
		SpawnBug (BUG_SIZE.SMALL, BUG_DIFFICULTY.EASY, 
			new Vector3 (-7.3f, 2.5f, 0f), new Vector3 (-5f, -0.38f, 0f));

		// lower left
		SpawnBug (BUG_SIZE.SMALL, BUG_DIFFICULTY.EASY,
			new Vector3 (-7.3f, -5.54f, 0f), new Vector3 (-4.5f, -2.3f, 0f)); 

		// lower right
		SpawnBug (BUG_SIZE.SMALL, BUG_DIFFICULTY.EASY,
			new Vector3 (7f, -6f, 0f), new Vector3 (3.8f, -3.5f, 0f));

		yield return new WaitForSeconds (10f);
		// upper right
		SpawnBug (BUG_SIZE.MEDIUM, BUG_DIFFICULTY.EASY, 
			new Vector3 (6f, 1.2f, 0f), new Vector3 (5f, 0.4f, 0f));

//		// upper left
//		SpawnBug (BUG_SIZE.SMALL, BUG_DIFFICULTY.EASY, 
//			new Vector3 (-7.3f, 2.5f, 0f), new Vector3 (-5f, -0.38f, 0f));

		// lower left
		SpawnBug (BUG_SIZE.MEDIUM, BUG_DIFFICULTY.EASY,
			new Vector3 (-7.3f, -5.54f, 0f), new Vector3 (-4.5f, -2.3f, 0f)); 

		// lower right
//		SpawnBug (BUG_SIZE.SMALL, BUG_DIFFICULTY.EASY,
//			new Vector3 (7f, -6f, 0f), new Vector3 (3.8f, -3.5f, 0f));
		waveComplete = true;
	}

	// 3 easy bugs from the top, 2 easy from the bottom
	IEnumerator WaveTwo() {
		waveComplete = false;
		healthPool.GiveHearts (2);
		// top wave
		SpawnBug (BUG_SIZE.MEDIUM, BUG_DIFFICULTY.EASY, 
			new Vector3 (-4.4f, 2.2f, 0f), new Vector3 (-4.4f, -3.5f, 0f));
		SpawnBug (BUG_SIZE.SMALL, BUG_DIFFICULTY.EASY,
			new Vector3 (0f, 2.2f, 0f), new Vector3 (0f, -2.69f, 0f));
		SpawnBug (BUG_SIZE.SMALL, BUG_DIFFICULTY.EASY, 
			new Vector3 (3.64f, 2.2f, 0f), new Vector3 (3.64f, -1.5f, 0f));


		// bottom wave
		SpawnBug (BUG_SIZE.MEDIUM, BUG_DIFFICULTY.EASY, 
			new Vector3 (5f, -5.75f, 0f), new Vector3 (5f, 0.3f, 0f));

		SpawnBug (BUG_SIZE.SMALL, BUG_DIFFICULTY.EASY, 
			new Vector3 (-2.25f, -5.75f, 0f), new Vector3 (-2.25f, -0.2f, 0f));

		yield return new WaitForSeconds (15f);

		// Send in the homing bugs in each corner
		// upper right
		SpawnBug (BUG_SIZE.SMALL, BUG_DIFFICULTY.NORMAL, 
			new Vector3 (6f, 1.2f, 0f), new Vector3 (5f, 0.5f, 0f));

		// lower left
		SpawnBug (BUG_SIZE.SMALL, BUG_DIFFICULTY.NORMAL,
			new Vector3 (-7.3f, -5.54f, 0f), new Vector3 (-4.5f, -2.3f, 0f)); 

		yield return new WaitForSeconds (10f);

		// upper left
		SpawnBug (BUG_SIZE.SMALL, BUG_DIFFICULTY.NORMAL, 
			new Vector3 (-7.3f, 2.5f, 0f), new Vector3 (-5f, -0.38f, 0f));
		
		// lower right
		SpawnBug (BUG_SIZE.SMALL, BUG_DIFFICULTY.NORMAL,
			new Vector3 (7f, -6f, 0f), new Vector3 (3.8f, -3.5f, 0f));

		waveComplete = true;
	}

	IEnumerator WaveThree() {
		waveComplete = false;
		healthPool.GiveHearts (2);

		// left
		SpawnBug (BUG_SIZE.MEDIUM, BUG_DIFFICULTY.NORMAL, 
			new Vector3 (-8.0f, 0f, 0f), new Vector3 (-5f, -0f, 0f));
		// right
		SpawnBug (BUG_SIZE.MEDIUM, BUG_DIFFICULTY.NORMAL, 
			new Vector3 (8.0f, 0f, 0f), new Vector3 (5f, -0f, 0f));
		// top
		SpawnBug (BUG_SIZE.SMALL, BUG_DIFFICULTY.NORMAL, 
			new Vector3 (0.0f, 1.5f, 0f), new Vector3 (0f, 1f, 0f));
//		// bottom
//		SpawnBug (BUG_SIZE.SMALL, BUG_DIFFICULTY.NORMAL, 
//			new Vector3 (0.0f, -5.5f, 0f), new Vector3 (0f, -4f, 0f));
		
		yield return new WaitForSeconds (10f);
//		// left
//		SpawnBug (BUG_SIZE.SMALL, BUG_DIFFICULTY.NORMAL, 
//			new Vector3 (-8.0f, 0f, 0f), new Vector3 (-5f, -0f, 0f));
		// right
		SpawnBug (BUG_SIZE.SMALL, BUG_DIFFICULTY.NORMAL, 
			new Vector3 (8.0f, 0f, 0f), new Vector3 (5f, -0f, 0f));
		// top
		SpawnBug (BUG_SIZE.MEDIUM, BUG_DIFFICULTY.NORMAL, 
			new Vector3 (0.0f, 1.5f, 0f), new Vector3 (0f, 1f, 0f));
		// bottom
		SpawnBug (BUG_SIZE.MEDIUM, BUG_DIFFICULTY.NORMAL, 
			new Vector3 (0.0f, -5.5f, 0f), new Vector3 (0f, -4f, 0f));

		yield return new WaitForSeconds (10f);
		// upper right
		SpawnBug (BUG_SIZE.SMALL, BUG_DIFFICULTY.EASY, 
			new Vector3 (6f, 1.2f, 0f), new Vector3 (5f, 0.5f, 0f));

		// upper left
		SpawnBug (BUG_SIZE.MEDIUM, BUG_DIFFICULTY.EASY, 
			new Vector3 (-7.3f, 2.5f, 0f), new Vector3 (-5f, -0.38f, 0f));

		// lower left
		SpawnBug (BUG_SIZE.SMALL, BUG_DIFFICULTY.EASY,
			new Vector3 (-7.3f, -5.54f, 0f), new Vector3 (-4.5f, -2.3f, 0f)); 

		// lower right
		SpawnBug (BUG_SIZE.MEDIUM, BUG_DIFFICULTY.EASY,
			new Vector3 (7f, -6f, 0f), new Vector3 (3.8f, -3.5f, 0f));

		yield return new WaitForSeconds (20f);
		// left
		SpawnBug (BUG_SIZE.MEDIUM, BUG_DIFFICULTY.NORMAL, 
			new Vector3 (-8.0f, 0f, 0f), new Vector3 (-5f, -0f, 0f));
		// right
		SpawnBug (BUG_SIZE.MEDIUM, BUG_DIFFICULTY.NORMAL, 
			new Vector3 (8.0f, 0f, 0f), new Vector3 (5f, -0f, 0f));
		// top
		SpawnBug (BUG_SIZE.MEDIUM, BUG_DIFFICULTY.NORMAL, 
			new Vector3 (0.0f, 1.5f, 0f), new Vector3 (0f, 1f, 0f));
		// bottom
		SpawnBug (BUG_SIZE.MEDIUM, BUG_DIFFICULTY.NORMAL, 
			new Vector3 (0.0f, -5.5f, 0f), new Vector3 (0f, -4f, 0f));

		waveComplete = true;
	}

	IEnumerator BossWaveOne() {
		waveComplete = false;
		finalBossStage = "ENTER";
		finalBossScript.GrandEntrance ();
		sfx.Play ("boss");
		yield return new WaitForSeconds (1f);
		emilyScript.Say ("hereItComes");
		yield return new WaitForSeconds (3f);
		aliceScript.Say ("jesus");
		yield return new WaitForSeconds (2f);
		finalBossStage = "BUGS";
		healthPool.GiveHearts (3);
		// Start spawning bugs
		Debug.Log("Spawning first wave of bugs");
		// Top
		SpawnBug (BUG_SIZE.SMALL, BUG_DIFFICULTY.NORMAL, 
			new Vector3 (3.3f, 0.25f, 0f), new Vector3 (-0f, 0.76f, 0f));
		// middle
		SpawnBug (BUG_SIZE.MEDIUM, BUG_DIFFICULTY.NORMAL, 
			new Vector3 (3.3f, -1.4f, 0f), new Vector3 (0f, -1.4f, 0f));
		// bottom
		SpawnBug (BUG_SIZE.SMALL, BUG_DIFFICULTY.NORMAL, 
			new Vector3 (3.3f, -3.1f, 0f), new Vector3 (0f, -3.1f, 0f));
		waveComplete = true;
	}

	void BossWaveTwo() {
		finalBossStage = "SEQUENCE";
		finalBossScript.SequenceTime (9);
		bugsRemaining++;
		waveComplete = true;
	}

	void BossWaveThree() {
		waveComplete = false;
		finalBossStage = "BUGS";
		SpawnBug (BUG_SIZE.MEDIUM, BUG_DIFFICULTY.NORMAL, 
			new Vector3 (3.3f, 0.25f, 0f), new Vector3 (-0f, 0.76f, 0f));
		// middle
		SpawnBug (BUG_SIZE.MEDIUM, BUG_DIFFICULTY.NORMAL, 
			new Vector3 (3.3f, -1.4f, 0f), new Vector3 (0f, -1.4f, 0f));
		// bottom
		SpawnBug (BUG_SIZE.MEDIUM, BUG_DIFFICULTY.NORMAL, 
			new Vector3 (3.3f, -3.1f, 0f), new Vector3 (0f, -3.1f, 0f));
		waveComplete = true;
	}

	void BossWaveFour() {
		finalBossStage = "SEQUENCE";
		finalBossScript.SequenceTime (15);
		bugsRemaining++;
		waveComplete = true;
	}

	void SpawnBug(BUG_SIZE size, BUG_DIFFICULTY diff, Vector3 start, Vector3 target) {
		bugsRemaining++;
		GameObject bugOne = bugFarmScript.SpawnBug(size, diff);
		BugScript scriptOne = bugOne.GetComponent<BugScript> ();
		bugOne.transform.localPosition =start;
		scriptOne.setMoveTarget(target);
	}

	IEnumerator YouWin() {
		finalBossStage = "DYING";
		sfx.Play ("victory");
		yield return new WaitForSeconds (3.0f);
		SceneManager.LoadScene ("Good End");
	}
}



