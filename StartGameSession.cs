using UnityEngine;

public class StartGameSession : MonoBehaviour
{
    [Tooltip("A reference to the PlayerInputHandler script.")]
    [SerializeField] private PlayerInputHandler playerInputHandler;

    void Start()
    {
        // 1. Ensure time is running
        Time.timeScale = 1f;

        // 2. Ensure game is not paused
        PauseController.SetPause(false);

        // 3. Lock and hide the cursor for gameplay
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // 4. Ensure player input is enabled
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