using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public int speed = 10;
	public GameObject ball;

	// Update is called once per frame
	void Update () {
		
		if (transform.position.y > 14) {
			transform.position = new Vector3 (transform.position.x, 14, transform.position.z);
		}

		if (transform.position.y < -12) {
			transform.position = new Vector3 (transform.position.x, -12, transform.position.z);
		}

		if (ball.transform.position.y > transform.position.y)
			transform.Translate (new Vector3(0, speed, 0) * Time.deltaTime);
		if (ball.transform.position.y < transform.position.y)
			transform.Translate (new Vector3 (0, -speed, 0) * Time.deltaTime);
	}
}
