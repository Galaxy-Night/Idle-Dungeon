using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaveDungeon : MonoBehaviour
{
	public void OnRetryClick() {
		SceneManager.LoadScene("Main");
	}
}
