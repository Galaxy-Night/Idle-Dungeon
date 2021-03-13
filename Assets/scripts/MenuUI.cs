using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
	[SerializeField]
	private GameObject menu;
	[SerializeField]
	private GameObject open;
	[SerializeField]
	private GameObject gameHandler;
	public void OnOpenClick() {
		menu.SetActive(true);
		open.SetActive(false);
		gameHandler.GetComponent<Game>().Pause();
	}

	public void OnCloseClick() {
		menu.SetActive(false);
		open.SetActive(true);
		gameHandler.GetComponent<Game>().Unpause();
	}

	public void OnLeaveClick() {
		SceneManager.LoadScene("LeaveDungeon");
	}
}
