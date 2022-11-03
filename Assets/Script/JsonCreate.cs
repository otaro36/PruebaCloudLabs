using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Data;
using UnityEngine.SceneManagement;

public class JsonCreate : MonoBehaviour
{
    public string folderPath;
    public string filePath;
    // Start is called before the first frame update
    void Awake()
    {
        folderPath = Application.streamingAssetsPath;
        filePath = folderPath + "/Studiantes.json";
    }


    [ContextMenu("CreatePath")]
    public string GetPath()
    {

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
        if (!File.Exists(filePath))
        {
            File.Create(filePath).Close();
            File.WriteAllText(filePath, "Studiantes.json");
            Debug.Log(filePath);
        }
        else
        {

        }
        SceneManager.LoadScene("SampleScene");
        return filePath;
    }
}
