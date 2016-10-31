using UnityEngine;
using System.Collections;

public class Puzzles : MonoBehaviour {

	public int ID;

	void ReplaceBlocks(int x, int y, int xx, int yy)
	{
		GameControl15.grid [x, y].transform.position = GameControl15.position [xx, yy];
		GameControl15.grid [xx, yy] = GameControl15.grid [x, y];
		GameControl15.grid [x, y] = null;
		GameControl15.GameFinish ();
	}

	void OnMouseDown()
	{
		for (int x = 0; x < 4; x++) {
			for (int y = 0; y < 4; y++) {
				if (GameControl15.grid[x,y]) {
					if (GameControl15.grid [x, y].GetComponent<Puzzles> () == null)
						Debug.Log ("null");
					if (GameControl15.grid [x, y].GetComponent<Puzzles> ().ID == ID) {
						
						if (x > 0 && GameControl15.grid [x - 1, y] == null) {
							ReplaceBlocks (x, y, x - 1, y);
							return;
						} else if (x < 3 && GameControl15.grid [x + 1, y] == null) {
							ReplaceBlocks (x, y, x + 1, y);
							return;
						}
						if (y > 0 && GameControl15.grid [x, y - 1] == null) {
							ReplaceBlocks (x, y, x, y - 1);
							return;
						} else if (y < 3 && GameControl15.grid [x, y + 1] == null) {
							ReplaceBlocks (x, y, x, y + 1);
							return; 
						}
					}
				}
			}
		}
	}
}
