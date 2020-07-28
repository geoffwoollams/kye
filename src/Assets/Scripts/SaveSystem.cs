using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    private static string _path = Application.persistentDataPath + "/settings.v1.dat";

    public static void Save(Settings settings)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(_path, FileMode.Create);
        formatter.Serialize(stream, settings);
        stream.Close();
    }

    public static Settings Load()
    {
        try
        {
            if(File.Exists(_path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(_path, FileMode.Open);

                Settings data = formatter.Deserialize(stream) as Settings;
                stream.Close();

                return data;
            }
            else
            {
                Debug.Log("Save file not found! Creating at " + _path);
                var settings = new Settings("first", 640, 480, Screen.fullScreen);
                Save(settings);
                return settings;
            }
        }
        catch(Exception ex)
        {
            Debug.Log(ex.ToString());
            return null;
        }
    }
}
