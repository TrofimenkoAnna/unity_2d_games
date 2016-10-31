using UnityEngine;
using System.Collections;

public class PuzzleScript : MonoBehaviour {

	public int ID;

	void ReplaceBlocks(int x, int y, int XX, int YY)
	{
		GameControlScript.grid[x,y].transform.position = GameControlScript.position[XX,YY];
		GameControlScript.grid[XX,YY] = GameControlScript.grid[x,y];
		GameControlScript.grid[x,y] = null;
		GameControlScript.GameFinish();
	}

	void OnMouseDown()
	{
		for(int y = 0; y < 4; y++)
		{
			for(int x = 0; x < 4; x++)
			{
				if(GameControlScript.grid[x,y])
				{
					if(GameControlScript.grid[x,y].GetComponent<PuzzleScript>().ID == ID)
					{
						if(x > 0 && GameControlScript.grid[x-1,y] == null)
						{
							ReplaceBlocks(x,y,x-1,y);
							return;
						}
						else if(x < 3 && GameControlScript.grid[x+1,y] == null)
						{
							ReplaceBlocks(x,y,x+1,y);
							return;
						}
					}
				}
				if(GameControlScript.grid[x,y])
				{
					if(GameControlScript.grid[x,y].GetComponent<PuzzleScript>().ID == ID)
					{
						if(y > 0 && GameControlScript.grid[x,y-1] == null)
						{
							ReplaceBlocks(x,y,x,y-1);
							return;
						}
						else if(y < 3 && GameControlScript.grid[x,y+1] == null)
						{
							ReplaceBlocks(x,y,x,y+1);
							return;
						}
					}
				}
			}
		}
	}
}
