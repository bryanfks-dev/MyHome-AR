using UnityEngine;
using System.IO;

[System.Serializable]
public class Data
{
    public bool passed_intro_screen = false;
    public bool passed_get_started_popup = false;
    public bool passed_instrcution = false;
    public float fov = 60f;
    public float view_sens = 10f;
}

public class JsonFile: MonoBehaviour
{
    private static readonly string jsonPath = Application.persistentDataPath + "/data.json";
    public static Data data = null;

    public static void InitJson()
    {
        // Check if json file is exists
        if (!File.Exists(jsonPath))
        {
            // Initialize default Data class value
            data = new Data();

            // Write json with default value
            WriteFile(data);
        }
        else
        {
            // Retrieve data from json file
            string jsonData = File.ReadAllText(jsonPath);

            // Convert json data into Data class
            data = JsonUtility.FromJson<Data>(jsonData);
        }
    }

    private static void WriteFile(Data data)
    {
        string jsonData = JsonUtility.ToJson(data);

        File.WriteAllText(jsonPath, jsonData);
    }

    public static void WritePassedIntro(bool boolean)
    {
        data.passed_intro_screen = boolean;

        WriteFile(data);
    }

    public static void WritePassedGetStarted(bool boolean)
    {
        data.passed_get_started_popup = boolean;

        WriteFile(data);
    }

    public static void WritePassedInstruction(bool boolean)
    {
        data.passed_instrcution = boolean;

        WriteFile(data);
    }

    public static void WriteFOV(float num)
    {
        data.fov = num;

        WriteFile(data);
    }

    public static void WriteViewSens(float num)
    {
        data.view_sens = num;

        WriteFile(data);
    }
}
