using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Snake : MonoBehaviour {

	public GameObject tailPrefab;
	public GameObject food;
	public Transform rBorder;
	public Transform lBorder;
	public Transform tBorder;
	public Transform bBorder;

	private float speed = 0.1f;

	Vector2 vector = Vector2.up;
	Vector2 moveVector;

	List<Transform> tail = new List<Transform>();

	bool eat = false;
	bool vertical = false;
	bool horizontal = true;
	
	void Start () {
		SpawnFood ();
		InvokeRepeating("Movement", 0.3f, speed);
	
	}
	
	void Update () {
		if (Input.GetKey (KeyCode.RightArrow) && horizontal) {
			horizontal = false;
			vertical = true;
			vector = Vector2.right;
		} else if (Input.GetKey (KeyCode.UpArrow) && vertical) {
			horizontal = true;
			vertical = false;
			vector = Vector2.up;
		} else if (Input.GetKey (KeyCode.DownArrow) && vertical) {
			horizontal = true;
			vertical = false;
			vector = -Vector2.up;
		} else if (Input.GetKey (KeyCode.LeftArrow) && horizontal) {
			horizontal = false;
			vertical = true;
			vector = -Vector2.right;
		}
		moveVector = vector / 3f;
	
	}

	public void SpawnFood() {
		int x = (int)Random.Range (lBorder.position.x, rBorder.position.x);
		int y = (int)Random.Range (bBorder.position.y, tBorder.position.y);
		
		Instantiate (food, new Vector2 (x, y), Quaternion.identity);
	}

	void Movement() {

		Vector2 ta = transform.position;
		if (eat) {
			if (speed > 0.002){
				speed = speed - 0.002f;
			}
			GameObject g =(GameObject)Instantiate(tailPrefab, ta, Quaternion.identity);
			
			tail.Insert(0, g.transform);
			Debug.Log(speed);
			eat = false;
		}
		else if (tail.Count > 0) {
			tail.Last().position = ta;
			tail.Insert(0, tail.Last());
			tail.RemoveAt(tail.Count-1);
		}
		transform.Translate(moveVector);
	}

	void OnTriggerEnter2D(Collider2D c) {

		if (c.name.StartsWith("food")) {
			eat = true;
			Destroy(c.gameObject);
			SpawnFood();
		}
		else {
			Application.LoadLevel(1);
		}
	}
}
