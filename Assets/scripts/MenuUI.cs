using System.Collections;
using System.Collections.Generic;
using System.IO;

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

	public void OnClearClick() {
		if (File.Exists(Application.persistentDataPath + "/dungeonstate.save"))
			File.Delete(Application.persistentDataPath + "/dungeonstate.save");
		SceneManager.LoadScene("Main");
	}
}
