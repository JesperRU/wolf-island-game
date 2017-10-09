using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Rabbit : MonoBehaviour {
	public GameObject fieldView;
	public float feedness = 50;
	public float maxFeedness=100;
	public float metabolism = 1f;
	public float viewRange = 3;
	public float mutationSpeed = 0.05f;
	public float speed=10;
	public bool foodIsFound=false;
	public float foodRequiredForReproduction = 0.6;
	public float foodTakenDuringReproduction = 30;
	public float foodPerResource = 10;
	private bool Eating = false;
	private GameObject vr;
	public int generation = 1;

	void CreateHeir(){
		GameObject heir = Instantiate (gameObject, transform.position + new Vector3 (1, 0, 0), Quaternion.identity);
		Rabbit heirAtt = heir.GetComponent<Rabbit> ();
		heirAtt.maxFeedness = maxFeedness + Random.Range (-maxFeedness * mutationSpeed, maxFeedness * mutationSpeed);
		heirAtt.metabolism = metabolism + Random.Range (-metabolism * mutationSpeed, metabolism * mutationSpeed);
		heirAtt.speed = 20 - heirAtt.maxFeedness / 10;
		//		Избавс от констант
		fieldView.GetComponent<CircleCollider2D> ().radius = 7 - heirAtt.metabolism * 4;
		heirAtt.generation++;
	}

	void Start () {
		ChangeDirection();
		SetName ();
	}

	void ChangeDirection(){
		if (!Eating && !foodIsFound) {
			float direction = Random.Range (-0f, 1f);
			gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 ((1 - 2 * Random.Range ((int)0, (int)2)) * direction * speed,
				(1 - 2 * Random.Range ((int)0, (int)2)) * Mathf.Sqrt (1 - direction * direction) * speed);
		}
		Invoke ("ChangeDirection", Random.Range (1f, 2f));
	}

	void Update () {
		feedness -= Time.deltaTime/metabolism;
		if (feedness <= 0)
			Destroy (gameObject);
		if (Eating) {
			if (feedness >= maxFeedness * foodRequiredForReproduction) {
				Eating = false;
				if (Random.Range (0f, 10f) > 2.5f) {
					CreateHeir ();
					feedness -= foodRequiredForReproduction;
				}
			}
			feedness += foodPerResource * Time.deltaTime;
			if (vr != null)
				vr.GetComponent<Grass> ().foodResource -= 1; 
		}
	}

	void OnTriggerEnter2D (Collider2D PlayerZone)
	{
		if (PlayerZone.gameObject.tag == "Food")
		 {
			gameObject.GetComponent<Animator> ().SetBool ("Eat", false); Eating = true;
			gameObject.GetComponent<Rigidbody2D> ().velocity*=0;
			vr = PlayerZone.gameObject;
		}
	}

	void OnTriggerExit2D(Collider2D PlayerZone){
		if (PlayerZone.gameObject.tag == "Food") {
			gameObject.GetComponent<Animator> ().SetBool("Eat", true);
			Eating = false;
		}
	}

	void SetName()
	{
		try
		{
			StreamReader textRead = new StreamReader (
				new FileStream(@"Assets/Resources/TextResources/rabbit-names-en.txt",FileMode.Open,FileAccess.Read));
			var status = Resources.Load("Image/StatusBar");
			string[] names = textRead.ReadToEnd ().Split ('\n');
			name = names[Random.Range(0,names.Length)] +" ("+ generation.ToString()+')';
		}
		catch(IOException e) {
			Debug.Log (e.StackTrace);
		}
	}



}
