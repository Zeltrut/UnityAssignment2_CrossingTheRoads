using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuController : MonoBehaviour
{
    [Header("Menu Panels")]
    [Tooltip("The main title UI, which is a child of the Menu.")]
    [SerializeField] private GameObject TitleUI; 

    [Tooltip("The panel that shows all the level selection buttons.")]
    [SerializeField] private GameObject levelSelectPanel;

    [Header("Level Unlocking")]
    [Tooltip("A list of all the level buttons, in order (e.g., Level 1, Level 2...)")]
    [SerializeField] private Button[] levelButtons;

    void Start()
    {
        // Check if we are returning from a completed level to show the correct panel.
        if (GameProgression.ReturnToLevelSelect)
        {
            ShowLevelSelect();
            GameProgression.ReturnToLevelSelect = false;
        }
        else
        {
            ShowMainMenu();
        }
    }

    public void ShowLevelSelect()
    {
        if (TitleUI != null) TitleUI.SetActive(false);
        if (levelSelectPanel != null) levelSelectPanel.SetActive(true);

        UpdateLevelButtons();
    }

    public void ShowMainMenu()
    {
        if (TitleUI != null) TitleUI.SetActive(true);
        if (levelSelectPanel != null) levelSelectPanel.SetActive(false);
    }

    public void LoadLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    private void UpdateLevelButtons()
    {
        int highestLevel = GameProgression.GetHighestLevelUnlocked();

        for (int i = 0; i < levelButtons.Length; i++)
        {
            int levelIndex = i + 1;

            if (levelIndex <= highestLevel)
            {
                levelButtons[i].interactable = true;
            }
            else
            {
                levelButtons[i].interactable = false;
            }
        }
    }

    // "Reset Progress" button.
    public void ResetGameProgression()
    {
        GameProgression.ResetProgression();
        UpdateLevelButtons();
    }

    // A new debug function to check the saved progression value.
    public void DebugProgression()
    {
        int highestLevel = GameProgression.GetHighestLevelUnlocked();
        Debug.Log($"[DEBUG] Highest Level Unlocked is: {highestLevel}");
    }
}