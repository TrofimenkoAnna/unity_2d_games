using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class TetrisScript : MonoBehaviour {

	public GameObject cube;
	public float speed = 1;
	public Color[] color;

	public KeyCode right = KeyCode.D;
	public KeyCode left = KeyCode.A;
	public KeyCode down = KeyCode.S;
	public KeyCode rotateLeft = KeyCode.Q;
	public KeyCode rotateRight = KeyCode.E;

	private int width = 12;
	private int height = 20;
	private string[] shapeName = {"I", "O", "L", "J", "S", "Z", "T"};
	private GameObject[,] grid = new GameObject[0, 0];
	private int xGrid;
	private GameObject current;
	private List<GameObject> shape = new List<GameObject>();
	private float curSpeed;

	// Use this for initialization
	void Start () {
		grid = new GameObject[width, height];
		xGrid = width / 2;
		CreateShape ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (right) && RightLeft (1))
			Move (1);
		else if (Input.GetKeyDown (left) && RightLeft (-1))
			Move (-1);
		else if (Input.GetKeyDown (rotateLeft))
			Rotation (90);
		else if (Input.GetKeyDown (rotateRight))
			Rotation (-90);
		else if (Input.GetKey (down))
			curSpeed = 0;
		else
			curSpeed = speed;
	}

	void Move(int index)
	{

	}

	bool RightLeft(int index)
	{
		return true;
	}

	void Rotation(int index)
	{

	}

	void CreateCube(Color cubeColor) {
		current = Instantiate (cube) as GameObject;
		current.GetComponent<MeshRenderer> ().material.color = cubeColor;
		shape.Add (current);
	}

	void CreateShape()
	{
		Random.seed = System.DateTime.Now.Millisecond;
		int i = Random.Range (0, shapeName.Length);
		switch (shapeName [i]) {
		case "I": 
			for (int j = 0; j < 4; j++) {
				CreateCube (color [0]);
				switch (j) {
				case 0:
					current.transform.position = new Vector2 (xGrid, -3);
					break;
				case 1:
					current.transform.position = new Vector2 (xGrid, -2);
					break;
				case 2:
					current.transform.position = new Vector2 (xGrid, -1);
					break;
				case 3:
					current.transform.position = new Vector2 (xGrid, 0);
					break;
				}
			}
			break;
		case "O":
			for (int j = 0; j < 4; j++) {
				CreateCube (color [1]);
				switch (j) {
				case 0:
					current.transform.position = new Vector2 (xGrid, 0);
					break;
				case 1:
					current.transform.position = new Vector2 (xGrid + 1, 0);
					break;
				case 2:
					current.transform.position = new Vector2 (xGrid, -1);
					break;
				case 3:
					current.transform.position = new Vector2 (xGrid + 1, -1);
					break;
				}
			}
			break;
		case "L":
			for (int j = 0; j < 4; j++) {
				CreateCube (color [2]);
				switch (j) {
				case 0:
					current.transform.position = new Vector2 (xGrid, -2);
					break;
				case 1:
					current.transform.position = new Vector2 (xGrid, -1);
					break;
				case 2:
					current.transform.position = new Vector2 (xGrid, 0);
					break;
				case 3:
					current.transform.position = new Vector2 (xGrid + 1, 0);
					break;
				}
			}
			break;
		case "J":
			for (int j = 0; j < 4; j++) {
				CreateCube (color [3]);
				switch (j) {
				case 0:
					current.transform.position = new Vector2 (xGrid, -2);
					break;
				case 1:
					current.transform.position = new Vector2 (xGrid, -1);
					break;
				case 2:
					current.transform.position = new Vector2 (xGrid, 0);
					break;
				case 3:
					current.transform.position = new Vector2 (xGrid - 1, 0);
					break;
				}
			}
			break;
		case "S":
			for (int j = 0; j < 4; j++) {
				CreateCube (color [4]);
				switch (j) {
				case 0:
					current.transform.position = new Vector2 (xGrid, 0);
					break;
				case 1:
					current.transform.position = new Vector2 (xGrid + 1, 0);
					break;
				case 2:
					current.transform.position = new Vector2 (xGrid, -1);
					break;
				case 3:
					current.transform.position = new Vector2 (xGrid - 1, -1);
					break;
				}
			}
			break;
		case "Z": 
			for (int j = 0; j < 4; j++) {
				CreateCube (color [5]);
				switch (j) {
				case 0:
					current.transform.position = new Vector2 (xGrid, 0);
					break;
				case 1:
					current.transform.position = new Vector2 (xGrid - 1, 0);
					break;
				case 2:
					current.transform.position = new Vector2 (xGrid, -1);
					break;
				case 3:
					current.transform.position = new Vector2 (xGrid + 1, -1);
					break;
				}
			}
			break;
		case "T":
			for (int j = 0; j < 4; j++) {
				CreateCube (color [6]);
				switch (j) {
				case 0:
					current.transform.position = new Vector2 (xGrid, 0);
					break;
				case 1:
					current.transform.position = new Vector2 (xGrid + 1, 0);
					break;
				case 2:
					current.transform.position = new Vector2 (xGrid -1, 0);
					break;
				case 3:
					current.transform.position = new Vector2 (xGrid, -1);
					break;
				}
			}
			break;
		}
		if (GameOver ())
			SceneManager.LoadScene ("tetris");
	}

	bool GameOver()
	{
		for (int i = 0; i < shape.Count; i++) {
			Transform tr = shape [i].transform;
			int x = Mathf.RoundToInt (tr.position.x);
			int y = Mathf.Abs (Mathf.RoundToInt (tr.position.x));

			if(y < height -1)
			{
				if(grid[x, y + 1]) {
					return true;
				}
			}
		}
		return false;
	}
}
