using UnityEngine;
using UnityEngine.UI;

public class SpawnPot : MonoBehaviour
{
    public GameObject potPrefab;
    public Transform spawnPoint;
    public Button potButton;   // assign your Pot UI button here
    public FlowerGrowthManager growthManager;


    public GameObject currentPot;

    public void SpawnPotNow()
    {
        Debug.Log("[SpawnPot] Button clicked");

        if (currentPot != null)
        {
            Debug.Log("[SpawnPot] Pot already exists, disabling button.");
            potButton.interactable = false;   // disable button
            return;
        }

        currentPot = Instantiate(potPrefab, spawnPoint.position, spawnPoint.rotation);

        if (growthManager != null)
        growthManager.StartTimerIfNeeded();

        Debug.Log("[SpawnPot] Spawned pot at " + spawnPoint.position);

        // disable the button after successful spawn
        potButton.interactable = false;
    }
}
