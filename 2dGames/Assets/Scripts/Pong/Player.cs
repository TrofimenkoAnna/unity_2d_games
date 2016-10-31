using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public int speed = 10;

	void Update()
	{
		if (transform.position.y > 14) {
			transform.position = new Vector3 (transform.position.x, 14, transform.position.z);
		}

		if (transform.position.y < -12) {
			transform.position = new Vector3 (transform.position.x, -12, transform.position.z);
		}

		if (Input.GetButton ("UP")) {
			transform.Translate (new Vector3 (0, speed, 0) * Time.deltaTime);
		}

		if (Input.GetButton ("DOWN")) {
			transform.Translate (new Vector3 (0, -speed, 0) * Time.deltaTime);
		}
	}
}
