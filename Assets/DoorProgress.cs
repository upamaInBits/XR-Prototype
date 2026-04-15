using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorProgress : MonoBehaviour
{
    public static DoorProgress Instance;

    [SerializeField] int totalDoors = 3;
    [SerializeField] string readyScene = "ReadyScene";

    private HashSet<string> completed = new HashSet<string>();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

       
    }

    public void MarkDoorDone(string doorId)
    {
        if (string.IsNullOrEmpty(doorId))
        {
            Debug.LogWarning("MarkDoorDone: empty id");
            return;
        }

        if (!completed.Add(doorId))
        {
            Debug.Log($"Already counted {doorId}");
            return;
        }

        Debug.Log($"[DoorProgress] Marked {doorId} ({completed.Count}/{totalDoors})");

        if (AreAllDoorsDone())
        {
            Debug.Log("[DoorProgress] All done → loading ReadyScene (Single)");
            SceneManager.LoadScene(readyScene, LoadSceneMode.Single);
        }
    }

    public bool AreAllDoorsDone() => completed.Count >= totalDoors;

    public bool IsDoorDone(string doorId)
    {
        if (string.IsNullOrEmpty(doorId)) return false;
        return completed.Contains(doorId);
    }
}
