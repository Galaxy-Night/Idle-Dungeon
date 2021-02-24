using System.IO;

using UnityEngine;

public class DeleteSave : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(GameObject.Find("load_handler"));
        string fileLocation = Application.persistentDataPath + "/DungeonState.save";

        if (File.Exists(fileLocation))
            File.Delete(fileLocation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
