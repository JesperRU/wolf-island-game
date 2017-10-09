using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewField : MonoBehaviour {

	public GameObject Rabbit;

	void OnTriggerEnter2D (Collider2D PlayerZone)
	{
		if (PlayerZone.gameObject.tag == "Food"){
			Vector3 FP = PlayerZone.transform.position;
			Vector3 RP = transform.position;
			float module = Mathf.Sqrt((FP.x-RP.x)*(FP.x-RP.x)+(FP.y-RP.y)*(FP.y-RP.y));
			Rabbit.GetComponent<Rigidbody2D> ().velocity = new Vector2 ((FP.x - RP.x) / module, (FP.y - RP.y) / module);
			Rabbit.GetComponent<Rigidbody2D> ().velocity *= Rabbit.GetComponent<Rabbit> ().speed;
			Rabbit.GetComponent<Rabbit> ().foodIsFound = true; }
		if (PlayerZone.gameObject.tag == "Wolf"){
			Vector3 FP = PlayerZone.transform.position;
			Vector3 RP = transform.position;
			float module = -Mathf.Sqrt((FP.x-RP.x)*(FP.x-RP.x)+(FP.y-RP.y)*(FP.y-RP.y));
			Rabbit.GetComponent<Rigidbody2D> ().velocity = new Vector2 ((FP.x - RP.x) / module, (FP.y - RP.y) / module);
			Rabbit.GetComponent<Rigidbody2D> ().velocity *= Rabbit.GetComponent<Rabbit> ().speed; }
	}

void OnTriggerExit2D (Collider2D PlayerZone)
{
	if (PlayerZone.gameObject.tag == "Food")
		Rabbit.GetComponent<Rabbit> ().foodIsFound = false;
}

	void Update(){
		transform.position = Rabbit.transform.position;
	}

}