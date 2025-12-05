using UnityEngine;

public static class GameProgression
{
    private const string LevelKey = "HighestLevelUnlocked";
    public static bool ReturnToLevelSelect = false;

    // Saves that a new level has been unlocked, if it's higher than the current one.
    public static void UnlockLevel(int levelIndex)
    {
        // Get the current highest level.
        int currentHighest = GetHighestLevelUnlocked();

        // Only save if this new level is higher than what's already saved.
        if (levelIndex > currentHighest)
        {
            PlayerPrefs.SetInt(LevelKey, levelIndex);
            PlayerPrefs.Save();
            Debug.Log($"GameProgression: New highest level unlocked: {levelIndex}");
        }
    }

    // Gets the highest level the player is allowed to play.
    public static int GetHighestLevelUnlocked()
    {
        // Level 1 is always unlocked by default.
        return PlayerPrefs.GetInt(LevelKey, 1);
    }

    // Resets all global progression data to its default state.
    public static void ResetProgression()
    {
        PlayerPrefs.DeleteKey(LevelKey);
        
        // --- NEW: Reset High Scores ---
        // Assuming you have 3 levels, we loop through and delete their high score keys.
        // If you add more levels later, increase this number or use a loop.
        for (int i = 1; i <= 3; i++) 
        {
            PlayerPrefs.DeleteKey("HighScore_Level_" + i);
        }
        
        PlayerPrefs.Save();
        Debug.Log("Game Progression and High Scores have been reset.");
    }
}