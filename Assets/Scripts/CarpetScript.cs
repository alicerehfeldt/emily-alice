using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarpetScript : MonoBehaviour {
	public bool isAliceWorld = false;
	// Use this for initialization
	void Start () {
		MeshRenderer mr = GetComponent<MeshRenderer> ();
		if (isAliceWorld) {
			mr.sortingLayerName = "Alice World Background";
		} else {
			mr.sortingLayerName = "Emily World Background";
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
