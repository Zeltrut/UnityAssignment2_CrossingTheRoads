using UnityEngine;
using UnityEngine.UI;

public class LevelStartController : MonoBehaviour
{
    [Header("UI Pages")]
    [Tooltip("The main parent panel that holds both instruction pages.")]
    [SerializeField] private GameObject instructionPanel; //main panel where everything is in for instructions

    [Tooltip("The first page of instructions (e.g., Objectives).")]
    [SerializeField] private GameObject objectivePage;

    [Tooltip("The second page of instructions (e.g., Controls).")]
    [SerializeField] private GameObject controlsPage;
    
    [Header("Player Reference")]
    [Tooltip("A reference to the PlayerInputHandler script.")]
    [SerializeField] private PlayerInputHandler playerInputHandler;

    void Start()
    {
        // 1. Pause the game
        Time.timeScale = 0f;
        PauseController.SetPause(true); 

        // 2. Show the main panel
        if (instructionPanel != null)
        {
            instructionPanel.SetActive(true);
        }

        // 3. Show the first page (objective) and hide the second (controls)
        ShowObjectivePage();

        // 4. Unlock and show the cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // 5. Disable player input
        if (playerInputHandler != null)
        {
            playerInputHandler.DisableGameplayInput();
        }
    }

    public void ShowObjectivePage()
    {
        if (objectivePage != null) objectivePage.SetActive(true);
        if (controlsPage != null) controlsPage.SetActive(false);
    }

    public void ShowControlsPage()
    {
        if (objectivePage != null) objectivePage.SetActive(false);
        if (controlsPage != null) controlsPage.SetActive(true);
    }

    public void BeginLevel()
    {
        // 1. Resume the game
        Time.timeScale = 1f;
        PauseController.SetPause(false);

        // 2. Hide the main instruction panel (which hides both pages)
        if (instructionPanel != null)
        {
            instructionPanel.SetActive(false);
        }

        // 3. Lock and hide the cursor for gameplay
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // 4. Enable player input
        if (playerInputHandler != null)
        {
            playerInputHandler.EnableGameplayInput();
        }
    }
}