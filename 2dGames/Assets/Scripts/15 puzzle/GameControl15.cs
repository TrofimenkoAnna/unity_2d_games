using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameControl15 : MonoBehaviour {

	public GameObject[] puzzle;
	public static Vector3[,] position;
	public static GameObject[,] grid;
	private GameObject[] puzzleRandom;

	public float startPosX = 0f;
	public float startPosY = 0f;
	public float outX = 2f;
	public float outY = 2f;

	public static bool win = false;

	// Use this for initialization
	void Start () {
		puzzleRandom = new GameObject[puzzle.Length];
		FillArrayOfPositions ();
		RandomPuzzle ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FillArrayOfPositions()
	{
		position = new Vector3[4, 4];
		float resetPosX = startPosX;

		for (int y = 0; y < 4; y++) {
			startPosY -= outY;
			for (int x = 0; x < 4; x++) {
				startPosX += outX;
				position [x, y] = new Vector3 (startPosX, startPosY, 0);
			}
			startPosX = resetPosX;
		}
	}

	void RandomPuzzle()
	{
		int[] tmp = new int[puzzle.Length];
		for (int i = 0; i < puzzle.Length; i++) {
			tmp [i] = 1;
		}
		int t = 0;
		while(t < puzzle.Length)
		{
			int r = Random.Range (0, puzzle.Length);
			if (tmp [r] == 1) {
				puzzleRandom [t] = Instantiate (puzzle [r], new Vector3 (0, 10, 0), Quaternion.identity) as GameObject;
				tmp [r] = 0;
				t++;
			}
		}
		CreatePazzle ();
	}

	void CreatePazzle()
	{
		int i = 0;
		grid = new GameObject[4, 4];
		int t = Random.Range (0, 3);
		int p = Random.Range (0, 3);
		GameObject nullObj = new GameObject ();
		grid [t, p] = nullObj;

		for(int y = 0; y < 4; y++)
		{
			for (int x = 0; x < 4; x++) {
				if (grid [x, y] == null) {
					grid [x, y] = Instantiate (puzzleRandom [i], position [x, y], Quaternion.identity) as GameObject;
					i++;
				}
			}
		}
	}

	public static void GameFinish()
	{

	}
}
