using UnityEngine;
using UnityEngine.UI;

public class FlowerSpawner : MonoBehaviour
{
    public GameObject flowerPrefab;   // flower to spawn
    public Button flowerButton;       // UI button
    public SpawnPot potSpawner;       // reference to SpawnPot script
    public FlowerGrowthManager growthManager;


    public GameObject currentFlower;
    public MessageDisplay messageDisplay;

    public void SpawnFlowerNow()
    {
        if (growthManager != null)
        growthManager.StartTimerIfNeeded();
        Debug.Log("[FlowerSpawner] Button clicked");

        // 1) require pot first

 if (potSpawner == null || potSpawner.currentPot == null)
    {
        Debug.Log("[FlowerSpawner] No pot yet! Cannot place flower.");

        if (messageDisplay != null)
            messageDisplay.ShowTemporary("Place the pot first!");

        return;
    }


        // 2) only one flower allowed
        if (currentFlower != null)
        {
            Debug.Log("[FlowerSpawner] Flower already exists.");
            flowerButton.interactable = false;
            return;
        }

        // 3) find snap point inside the pot
        Transform snapPoint = potSpawner.currentPot.transform.Find("FlowerSnapPoint");

        if (snapPoint == null)
        {
            Debug.LogError("[FlowerSpawner] FlowerSnapPoint not found on pot!");
            return;
        }

        // 4) spawn the flower on top of the pot
        currentFlower = Instantiate(flowerPrefab, snapPoint.position, snapPoint.rotation);
        Debug.Log("[FlowerSpawner] Spawned flower at " + snapPoint.position);

        // 5) disable the button so kids can't spam flowers
        flowerButton.interactable = false;
    }
}
