using UnityEngine;
using UnityEngine.SceneManagement;

public class Load : MonoBehaviour
{
    public Save save { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        save = Save.Deserialize();
        SceneManager.LoadScene("Main");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
