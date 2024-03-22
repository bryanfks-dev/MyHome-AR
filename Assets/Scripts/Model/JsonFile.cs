using UnityEngine;
using System.IO;

[System.Serializable]
public class Data
{
    public bool passed_intro_screen = false;
    public bool passed_get_started_popup = false;
}

public class JsonFile: MonoBehaviour
{
    private static string _jsonPath = Application.persistentDataPath + "/data.json";
    public static Data data = null;

    public static void initJson()
    {
        if (!File.Exists(_jsonPath))
        {
            // Initialize default Data class value
            data = new Data();

            // Write json with default value
            writeFile(data);
        }
        else
        {
            // Retrieve data from json file
            string jsonData = File.ReadAllText(_jsonPath);

            // Convert json data into Data class
            data = JsonUtility.FromJson<Data>(jsonData);
        }
    }

    private static void writeFile(Data data)
    {
        string jsonData = JsonUtility.ToJson(data);

        File.WriteAllText(_jsonPath, jsonData);
    }

    public static void writePassedIntro(bool boolean)
    {
        data.passed_intro_screen = boolean;

        writeFile(data);
    }

    public static void writePassedGetStarted(bool boolean)
    {
        data.passed_get_started_popup = boolean;

        writeFile(data);
    }
}
