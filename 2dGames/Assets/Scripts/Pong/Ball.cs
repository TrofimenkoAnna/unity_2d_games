using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	public float sSpeed = 10;
	public float factor = 10;

	public static int playerScore = 0;
	public static int enemyScore = 0;

	private Rigidbody rb;
	Rect pos_label;

	void Start()
	{
		rb = GetComponent<Rigidbody> ();
		rb.AddForce (new Vector3(-10, 0, 0));
		pos_label = new Rect (Screen.width/2 - 5, 10, 100, 30);
	}

	void Update()
	{
		Vector3 cVel = rb.velocity;
		Vector3 tVel = cVel.normalized * sSpeed;
		rb.velocity = Vector3.Lerp (cVel, tVel, Time.deltaTime * factor);

		if (transform.position.x > 22) {
			playerScore++;
			Debug.Log ("pl" + playerScore);
			transform.position = new Vector3 (0, 0, 0);
		}
		if (transform.position.x < -22) {
			enemyScore++;
			Debug.Log ("en" + enemyScore);
			transform.position = new Vector3 (0, 0, 0);
		}
	}

	void OnGUI(){
		GUI.Label (pos_label, playerScore + " : " + enemyScore);
	}
}
