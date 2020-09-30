using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{

    public static void SavePlayer(Transform player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.marmot";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);

        Debug.Log("<b>[SaveSystem]</b> Game saved");
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.marmot";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            Debug.Log("<b>[SaveSystem]</b> Game loaded");
            stream.Close();
            return data;
        } else
        {
            Debug.LogError("[SaveSystem] File not found in " + path);
            return null;
        }
    }

    public static void DeleteSavedGame()
    {
        string path = Application.persistentDataPath + "/player.marmot";
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }
}
