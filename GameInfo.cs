using UnityEngine;
using TMPro;

public class GameInfo : MonoBehaviour
{
    public static int collectibleCount = 0;

    [Header("UI References")]
    [Tooltip("The TMPro Text component that displays the current count.")]
    [SerializeField] private TMP_Text collectibleDisplay;
    
    [Tooltip("The TMPro Text component that displays the High Score.")]
    [SerializeField] private TMP_Text highScoreDisplay; 

    [Header("Level Settings")]
    [Tooltip("The name to display on the UI (e.g., CARROTS, DIAMONDS, DRINKS)")]
    [SerializeField] private string collectibleName = "SCORE";
    
    [Tooltip("The index of this level to track its specific high score.")]
    [SerializeField] private int levelIndex = 1; 

    [Header("Instruction Panel")]
    [Tooltip("Assign this ONLY for Level 1 to show the How To Play screen.")]
    [SerializeField] private GameObject instructionPanel;

    [Tooltip("The first page of instructions (e.g., Controls & Objectives).")]
    [SerializeField] private GameObject instructionPage1;

    [Tooltip("The second page of instructions (e.g., 'Begin').")]
    [SerializeField] private GameObject instructionPage2;
    
    [Tooltip("Reference to the PlayerInputHandler (needed if using Instruction Panel).")]
    [SerializeField] private PlayerInputHandler playerInputHandler;

    void Start()
    {
        collectibleCount = 0;
        UpdateHighScoreUI();

        // If an instruction panel is assigned (Level 1), pause and show it.
        if (instructionPanel != null)
        {
            ShowInstructions();
        }
        else
        {
            // Otherwise, just start the game immediately (Level 2, 3, etc.)
            StartGameSession();
        }
    }

    void Update()
    {
        if (collectibleDisplay != null)
            collectibleDisplay.text = $"{collectibleName}: {collectibleCount}";
    }

    public void AddCollectible()
    {
        collectibleCount++;
        CheckHighScore();
    }

    private void CheckHighScore()
    {
        string key = "HighScore_Level_" + levelIndex;
        int currentHigh = PlayerPrefs.GetInt(key, 0);
        
        if (collectibleCount > currentHigh)
        {
            PlayerPrefs.SetInt(key, collectibleCount);
            PlayerPrefs.Save();
            UpdateHighScoreUI();
        }
    }

    private void UpdateHighScoreUI()
    {
        if (highScoreDisplay != null)
        {
            string key = "HighScore_Level_" + levelIndex;
            int high = PlayerPrefs.GetInt(key, 0);
            highScoreDisplay.text = $"HIGH SCORE: {high}";
        }
    }

    // --- Instruction Logic ---

    private void ShowInstructions()
    {
        Time.timeScale = 0f;
        if (typeof(PauseController).GetMethod("SetPause") != null)
        {
            PauseController.SetPause(true);
        }

        instructionPanel.SetActive(true);
        
        // Show Page 1, Hide Page 2
        if (instructionPage1 != null) instructionPage1.SetActive(true);
        if (instructionPage2 != null) instructionPage2.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (playerInputHandler != null)
        {
            playerInputHandler.DisableGameplayInput();
        }
    }

    // Call this from your "Next" button
    public void GoToNextPage()
    {
        if (instructionPage1 != null) instructionPage1.SetActive(false);
        if (instructionPage2 != null) instructionPage2.SetActive(true);
    }

    // Call this from your "Begin" button in the Instruction Panel
    public void BeginLevel()
    {
        Time.timeScale = 1f;
        if (typeof(PauseController).GetMethod("SetPause") != null)
        {
            PauseController.SetPause(false);
        }

        if (instructionPanel != null)
        {
            instructionPanel.SetActive(false);
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (playerInputHandler != null)
        {
            playerInputHandler.EnableGameplayInput();
        }
    }

    private void StartGameSession()
    {
        // Standard start for levels without instructions
        Time.timeScale = 1f;
        if (typeof(PauseController).GetMethod("SetPause") != null)
        {
            PauseController.SetPause(false);
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Auto-find input handler if missing, just to be safe
        if (playerInputHandler == null)
        {
            playerInputHandler = FindAnyObjectByType<PlayerInputHandler>();
        }

        if (playerInputHandler != null)
        {
            playerInputHandler.EnableGameplayInput();
        }
    }
}