using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : MonoBehaviour {

	public double feedness = 50;
	public int maxFeedness = 100;
	public float speed = 10;
	public bool foodIsFound=false;
	private bool Eating = false;
	private Vector3 FO;
	private GameObject vr;

	void Start () {
		ChangeDirection();
		speed = Random.Range (5f, 15f);
		gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (speed,0);
	}

	void ChangeDirection(){
		if (!Eating&&!foodIsFound){
				float direction = Random.Range (-0f, 1f);
			gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 ((1 - 2 * Random.Range ((int)0, (int)2)) *direction * speed,
				(1 - 2 * Random.Range ((int)0,(int)2)) * Mathf.Sqrt (1 - direction * direction) * speed);
		}
		Invoke ("ChangeDirection", Random.Range (1f, 2f));
	}

	void Update () {
		feedness -= 2*Time.deltaTime;
		if (feedness <= 0)
			Destroy (gameObject);
		if (Eating) {
			if (feedness > maxFeedness) {
				Eating = false;
				if (Random.Range (0f, 10f) > 2.5f)
					Instantiate (gameObject, transform.position + new Vector3 (1, 0, 0), Quaternion.identity);
			}
			feedness += 10 * Time.deltaTime;
			if (vr != null)
				vr.GetComponent<Grass> ().foodResource -= 1; 
		}
	}

	void OnTriggerEnter2D (Collider2D PlayerZone)
	{
		if (PlayerZone.gameObject.tag == "Food")
		if (feedness < maxFeedness * 0.9f) {
			gameObject.GetComponent<Animator> ().SetBool ("Eat", false); Eating = true;
			gameObject.GetComponent<Rigidbody2D> ().velocity*=0;
			vr = PlayerZone.gameObject;
		}
	}

	void OnTriggerExit2D(Collider2D PlayerZone){
		if (PlayerZone.gameObject.tag == "Food") {
			gameObject.GetComponent<Animator> ().SetBool("Eat", true);
			Eating = false;}
	}

}
