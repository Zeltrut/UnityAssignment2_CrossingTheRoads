using UnityEngine;

public class GameInfo : MonoBehaviour
{
    // Renamed from carrotCount to be generic
    public static int collectibleCount = 0;
    
    [Header("UI References")]
    [Tooltip("The TMPro Text component that displays the score.")]
    [SerializeField] private TMPro.TMP_Text collectibleDisplay;
    
    [Header("Level Settings")]
    [Tooltip("The name to display on the UI (e.g., CARROTS, DIAMONDS, TRASH)")]
    [SerializeField] private string collectibleName = "SCORE";

    void Update()
    {
        // Updated the UI with the correct name and count
        collectibleDisplay.GetComponent<TMPro.TMP_Text>().text = collectibleName + ":  " + collectibleCount;
    }
}