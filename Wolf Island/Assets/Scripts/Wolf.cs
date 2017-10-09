using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : MonoBehaviour {

	public float feedness = 50;
	public float maxFeedness = 100;
	public float speed = 6;
	private bool isPregnant = false;
	private bool child=true;
	public bool isMale = true;
	private Color stand,change;
	private GameObject vr;

	void Birthness(){
		isPregnant = false; gameObject.transform.localScale /= 1.2f;
		vr = Instantiate (gameObject, transform.position+ new Vector3(1,0,0), Quaternion.identity);
		vr.transform.localScale *= 0.5f;
	}

	void Start () {
		Invoke ("ChangeDirection", Random.Range (1f, 2f));
		speed = Random.Range (7.5f, 12.5f);
		stand = gameObject.GetComponent<SpriteRenderer> ().color;
		if (Random.Range (0f, 2f) > 1) {
			isMale = true;
			change = stand;
			change.r = 0.5f; 
			gameObject.GetComponent<SpriteRenderer> ().color = change;
		} else isMale = false;		
		
		//gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (speed,0);
	}

	void ChangeDirection(){
			float direction = Random.Range (0f, 1f);
			gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 ((1 - 2 * Random.Range ((int)0, (int)2)) *direction * speed,
				(1 - 2 * Random.Range ((int)0,(int)2)) * Mathf.Sqrt (1 - direction * direction) * speed);
		Invoke ("ChangeDirection", Random.Range (1f, 2f));
	}

	void Update () {
		if (child) {
			gameObject.transform.localScale *= 1.001f;
			if (gameObject.transform.localScale.x > 4)
				child = false;
		}
		maxFeedness -= Time.deltaTime / 25;
		feedness -= Time.deltaTime/5;
		if (feedness <= 0)
			Destroy (gameObject);
	}

	void OnTriggerEnter2D(Collider2D PlayerZone){
		if (PlayerZone.gameObject.tag == "Rabbit")
		if (feedness < maxFeedness * 0.9f) {
			feedness += 40; 
			Destroy (PlayerZone.gameObject);
			if (feedness > maxFeedness) {
				feedness = maxFeedness; }
		}
		if (PlayerZone.gameObject.tag == "Wolf") {
			if (PlayerZone.gameObject.GetComponent<Wolf> ().isMale == !isMale)
			if (PlayerZone.gameObject.GetComponent<Wolf> ().feedness > 60 && feedness > 60)
			if (!isMale&&!isPregnant) {
				Invoke ("Birthness", 10);
				feedness -= 40;
				PlayerZone.gameObject.GetComponent<Wolf> ().feedness -= 40;
				gameObject.transform.localScale *= 1.2f;
			}
		} }
		
}
