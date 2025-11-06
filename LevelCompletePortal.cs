using UnityEngine;

public class LevelCompletePortal : MonoBehaviour
{
    [Header("Level Settings")]
    [Tooltip("The index of the *next* level this portal should unlock (e.g., 2 if this is Level 1)")]
    [SerializeField] private int levelToUnlock = 2;

    [Header("References")]
    [Tooltip("A reference to the SaveController in the scene.")]
    [SerializeField] private SaveController saveController;

    private bool hasBeenTriggered = false;

    void Start()
    {
        // Auto-find the SaveController if not assigned.
        if (saveController == null)
        {
            saveController = FindAnyObjectByType<SaveController>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Ensure we only trigger this once and that it's the player.
        if (hasBeenTriggered || !other.CompareTag("Player"))
        {
            return;
        }

        hasBeenTriggered = true;
        Debug.Log("Level Complete!");

        // 1. Save the new unlocked level.
        GameProgression.UnlockLevel(levelToUnlock);

        // 2. Set the flag to return to the level select screen.
        GameProgression.ReturnToLevelSelect = true;

        // 3. Call the new function in SaveController to clean up and go to the menu.
        if (saveController != null)
        {
            saveController.CompleteLevelAndGoToMenu();
        }
    }
}