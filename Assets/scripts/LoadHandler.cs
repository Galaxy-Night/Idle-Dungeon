using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
