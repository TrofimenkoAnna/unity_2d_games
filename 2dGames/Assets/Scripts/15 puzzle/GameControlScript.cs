using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameControlScript : MonoBehaviour {
	
	public GameObject[] puzzle;

	public float startPosX = -6f;
	public float startPosY = 6f;

	public float outX = 1.1f;
	public float outY = 1.1f;

	public static GameObject[,] grid;
	public static Vector3[,] position;
	private GameObject[] puzzleRandom;
	// Use this for initialization
	void Start () {
		puzzleRandom = new GameObject[puzzle.Length];

		float posXreset = startPosX;

		position = new Vector3[4,4];
		for(int y = 0; y < 4; y++)
		{
			startPosY -= outY;
			for(int x = 0; x < 4; x++)
			{
				startPosX += outX;
				position[x,y] = new Vector3(startPosX, startPosY, 0);
			}
			startPosX = posXreset;
		}
		RandomPuzzle();
	}


	void RandomPuzzle()
	{
		int[] tmp = new int[puzzle.Length];
		for(int i = 0; i < puzzle.Length; i++)
		{
			tmp[i] = 1;
		}
		int c = 0;
		while(c < puzzle.Length)
		{
			int r = Random.Range(0, puzzle.Length);
			if(tmp[r] == 1)
			{ 
				puzzleRandom[c] = Instantiate(puzzle[r], new Vector3(0, 10, 0), Quaternion.identity) as GameObject;
				tmp[r] = 0;
				c++;
			}
		}
		CreatePuzzle();
	}

	void CreatePuzzle()
	{
		int i = 0;
		grid = new GameObject[4,4];
		int h = Random.Range(0,3);
		int v = Random.Range(0,3);
		GameObject clone = new GameObject();
		grid[h,v] = clone; // размещаем пустой объект в случайную клетку
		for(int y = 0; y < 4; y++)
		{
			for(int x = 0; x < 4; x++)
			{
				// создание дубликатов на основе временного массива
				if(grid[x,y] == null)
				{
					grid[x,y] = Instantiate(puzzleRandom[i], position[x,y], Quaternion.identity) as GameObject;
					grid[x,y].name = "ID-"+i;
					//grid[x,y].transform.parent = transform;
					i++;
				}
			}
		}
		Destroy(clone); // очистка случайной клетки
		for(int q = 0; q < puzzle.Length; q++)
		{
			Destroy(puzzleRandom[q]); // очистка временного массива
		}
	}

	static public void GameFinish()
	{
		int i = 1;
		for(int y = 0; y < 4; y++)
		{
			for(int x = 0; x < 4; x++)
			{
				if(grid[x,y]) 
				{ if(grid[x,y].GetComponent<PuzzleScript>().ID == i) i++; } else i--;
			}
		}
		if(i == 15)
		{
			for(int y = 0; y < 4; y++)
			{
				for(int x = 0; x < 4; x++)
				{
					if(grid[x,y]) Destroy(grid[x,y].GetComponent<PuzzleScript>());
				}
			}
			Debug.Log("Finish!");
		}
	}
}
