using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class GameDataWriter
{
    private static GameData _gameData;
    public static GameData GameData
    {
        get
        {
            if(_gameData == null)
            {
                _gameData = new GameData();
            }
            return _gameData;
        }
    }

    public static void SaveGameData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/save.kek";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, GameData);
        stream.Close();
    }

    public static void LoadGameData()
    {
        string path = Application.persistentDataPath + "/save.kek";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            _gameData = (GameData)formatter.Deserialize(stream);
            stream.Close();
        }
        else
        {
            Debug.Log("Failed to load save file in " + path);
        }
    }
}
