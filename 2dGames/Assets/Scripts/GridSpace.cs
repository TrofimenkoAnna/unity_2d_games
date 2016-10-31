using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GridSpace : MonoBehaviour {

	public Button button;
	public Text buttonText;

	private GameController gameController;

	public void SetSpace()
	{
		buttonText.text = gameController.GetPlayerSide();
		button.interactable = false;
		gameController.EndTurn ();
	}

	public void SetGameControllerReference(GameController controller)
	{
		gameController = controller;
	}
}
