using UnityEngine;

public static class UnityJsonManager { 
    //JSON string to PlayerPrefs saving class.
    public static void SaveToJSON<T>(string fileName, T obj) {
        string json = JsonUtility.ToJson(obj);
        PlayerPrefs.SetString(fileName, json);
    }

    public static void GetFromJSON<T>(string fileName, T obj) {
        string json;
        if (!PlayerPrefs.HasKey(fileName)) {
            json = JsonUtility.ToJson(obj);
            PlayerPrefs.SetString(fileName, json);
        }
        json = PlayerPrefs.GetString(fileName);
        JsonUtility.FromJsonOverwrite(json, obj);
    }
}
