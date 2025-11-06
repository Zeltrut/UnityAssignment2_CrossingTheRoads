using UnityEngine;
using UnityEngine.UI;

public class LevelStartController : MonoBehaviour
{
    [Header("References")]
    [Tooltip("The UI panel that shows the instructions.")]
    [SerializeField] private GameObject instructionPanel;
    
    [Tooltip("A reference to the PlayerInputHandler script.")]
    [SerializeField] private PlayerInputHandler playerInputHandler;

    void Start()
    {
        // 1. Pause the game
        Time.timeScale = 0f;
        PauseController.SetPause(true);

        // 2. Show the instruction panel
        if (instructionPanel != null)
        {
            instructionPanel.SetActive(true);
        }

        // 3. Unlock and show the cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // 4. Disable player input
        if (playerInputHandler != null)
        {
            playerInputHandler.DisableGameplayInput();
        }
    }

    public void BeginLevel()
    {
        // 1. Resume the game
        Time.timeScale = 1f;
        PauseController.SetPause(false);

        // 2. Hide the instruction panel
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