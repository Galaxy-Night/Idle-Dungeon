using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaveDuneonUI : MonoBehaviour
{
	private void Start()
	{
		if (File.Exists(Application.persistentDataPath + "/dungeonstate.save"))
			File.Delete(Application.persistentDataPath + "/dungeonstate.save");
	}
	public void OnRetryClick() {
		SceneManager.LoadScene("Main");
	}
}
