using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassRander : MonoBehaviour {
	public GameObject grass;
	public int quantity = 0;
	public int maxQuantity=1800;
	public int timeGeneration=5;
	private GameObject vr;
	void Generate(){
		if (quantity <= maxQuantity) {
			vr = Instantiate (grass, new Vector3 (Random.Range (-51f, 51f), Random.Range (-33f, 33f), 0), Quaternion.identity);
			vr.transform.parent = transform;
			quantity++;} Invoke ("Generate", Random.Range ((float)(timeGeneration/10),(float)(timeGeneration/5)));
	}

	void Start () {
		Invoke ("Generate", Random.Range (0.05f, 0.15f));
		Time.timeScale = 10;
	}

}
