using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// <c>LoadHandler</c> is a class to handle the loading of the initial scene of
/// the game. Eventually it will also read the state of the user's previous 
/// game in from a file.
/// </summary>

public class LoadHandler : MonoBehaviour
{
    AsyncOperation loading;
    Image loadingBar;
    // Start is called before the first frame update
    void Start()
    {
        loadingBar = GameObject.Find("LoadingBar").GetComponent<Image>();
        loading = SceneManager.LoadSceneAsync("Main");
    }

    // Update is called once per frame
    void Update()
    {
        loadingBar.fillAmount = loading.progress;
        Debug.Log(loading.progress);
    }
}
