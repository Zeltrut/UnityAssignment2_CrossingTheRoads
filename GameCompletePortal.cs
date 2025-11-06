using UnityEngine;

public class GameCompletePortal : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject thanksForPlayingPanel;
    [SerializeField] private PlayerInputHandler playerInputHandler;

    private bool hasBeenTriggered = false;

    void Start()
    {
        // Make sure the panel is hidden on start
        if (thanksForPlayingPanel != null)
            thanksForPlayingPanel.SetActive(false);

        // Auto-find the Input Handler if not assigned.
        if (playerInputHandler == null)
        {
            playerInputHandler = FindAnyObjectByType<PlayerInputHandler>();
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

        FirstPersonController fpc = other.GetComponent<FirstPersonController>();
        if (fpc != null)
            fpc.enabled = false;

        if (playerInputHandler != null)
            playerInputHandler.DisableGameplayInput();

        Time.timeScale = 0f;
        PauseController.SetPause(true); 

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // 4. Show the "Thanks" panel
        if (thanksForPlayingPanel != null)
            thanksForPlayingPanel.SetActive(true);
    }
}