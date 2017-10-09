using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour {

	public int foodResource = 400;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (foodResource < 0) {
			GameObject.Find ("Square").GetComponent<GrassRander> ().quantity--;
			Destroy (gameObject);
		}
	}
}
