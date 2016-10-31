using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SnakeScript : MonoBehaviour {
	public Transform topBorder, leftBorder, bottomBorder, rightBorder;
	public GameObject food, tail;

	private Vector2 moveVector;

	private float speed = 0.3f;

	List<Transform> tailList = new List<Transform>();
	bool eat = false;

	// Use this for initialization
	void Start () {
		SpawnFood ();
		InvokeRepeating ("Movement", 0.3f, speed);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.RightArrow)) {
			moveVector = Vector2.right;
		} else if (Input.GetKey (KeyCode.LeftArrow)) {
			moveVector = Vector2.left;
		} else if (Input.GetKey (KeyCode.UpArrow)) {
			moveVector = Vector2.up;
		} else if (Input.GetKey (KeyCode.DownArrow)) {
			moveVector = Vector2.down;
		}
	}

	void Movement()
	{
		Vector2 sPos = transform.position;
		if (eat) {
			if (speed > 0.02)
				speed -= 0.1f;
			GameObject t = (GameObject)Instantiate (tail, sPos, Quaternion.identity);
			tailList.Insert (0, t.transform);
			Debug.Log (speed);
			eat = false;
		} else if (tailList.Count > 0) {
			tailList.Last ().position = sPos;
			tailList.Insert (0, tailList.Last ());
			tailList.RemoveAt (tailList.Count - 1);
		}
		transform.Translate (moveVector/3);
	}

	void SpawnFood()
	{
		Random.seed = System.DateTime.Now.Millisecond;
		int x = (int) Random.Range (leftBorder.position.x, rightBorder.position.x);
		int y = (int) Random.Range (topBorder.position.y, bottomBorder.position.y);

		Instantiate (food, new Vector2 (x, y), Quaternion.identity);
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.name.StartsWith ("food")) {
			eat = true;
			Destroy (collider.gameObject);
			SpawnFood ();
		} else {
			SceneManager.LoadScene ("snake");
		}
	}
}
