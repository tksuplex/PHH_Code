using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

// Utilized this tutorial: https://www.youtube.com/watch?v=XOjd_qU2Ido
public static class SaveLoadScript
{
    public static void SaveData(GameStatusScript gs)
    {
        BinaryFormatter form = new BinaryFormatter();
        string path = Application.persistentDataPath + "/SaveData.savefile";
        FileStream s = new FileStream(path, FileMode.Create);

        SaveData data = new SaveData(gs);

        form.Serialize(s, data);
        s.Close();
    }

    public static SaveData LoadData()
    {
        string path = Application.persistentDataPath + "/SaveData.savefile";

        if(File.Exists(path))
        {
            BinaryFormatter form = new BinaryFormatter();
            FileStream s = new FileStream(path, FileMode.Open);

            SaveData data = form.Deserialize(s) as SaveData;
            s.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in chosen path.");
            return null;
        }
    }
}
