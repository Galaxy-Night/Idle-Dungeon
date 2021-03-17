using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

using UnityEngine;
using UnityEngine.SceneManagement;

public class Load : MonoBehaviour
{
    public SaveData saveData;
    // Start is called before the first frame update
    void Start()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(SaveData));
        Stream stream;

        if (!File.Exists(Application.persistentDataPath + "/dungeonstate.save")) {
            SceneManager.LoadScene("Main");
		}
        else {
            DontDestroyOnLoad(this);
            stream = new FileStream(Application.persistentDataPath + "/dungeonstate.save", FileMode.Open);
            saveData = (SaveData)serializer.Deserialize(stream);
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onResumeClick() {
        SceneManager.LoadScene("Main");
    }

    public void onTimeLapseClick() {
        Debug.Log("Time lapse");
	}
}
