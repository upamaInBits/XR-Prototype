using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState Instance;

    // which doorIds are hidden/finished
    private HashSet<string> hiddenDoors = new HashSet<string>();

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        PlayerPrefs.DeleteKey("hiddenDoors");
        Load();
    }

    public bool IsDoorHidden(string doorId) => hiddenDoors.Contains(doorId);

    public void HideDoor(string doorId)
    {
        if (hiddenDoors.Add(doorId)) Save();
    }

    // Optional persistence so doors stay gone even if you restart the app
    const string Key = "hiddenDoors";
    void Save()
    {
        PlayerPrefs.SetString(Key, string.Join("|", hiddenDoors));
        PlayerPrefs.Save();
    }
    void Load()
    {
        hiddenDoors.Clear();
        var raw = PlayerPrefs.GetString(Key, "");
        if (!string.IsNullOrEmpty(raw))
        {
            foreach (var id in raw.Split('|'))
                if (!string.IsNullOrEmpty(id)) hiddenDoors.Add(id);
        }
    }
}
