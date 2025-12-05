using UnityEngine;
using UnityEngine.UI;

public class ShieldUI : MonoBehaviour
{
    public static ShieldUI Instance { get; private set; }

    [Header("UI Reference")]
    [SerializeField] private Image shieldImage;

    [Header("Blink Settings")]
    [SerializeField] private float blinkDuration = 2.0f; 
    [SerializeField] private float startBlinkSpeed = 5.0f;
    [SerializeField] private float endBlinkSpeed = 20.0f;

    private float shieldTimer = 0f;
    private float totalDuration = 0f;
    private bool isShieldActive = false;

    void Awake()
    {
        // Simple Singleton reset on load
        Instance = this;

        if (shieldImage != null)
        {
            shieldImage.enabled = false;
        }
    }

    public void ActivateShieldUI(float duration)
    {
        if (shieldImage == null) return;

        totalDuration = duration;
        shieldTimer = duration;
        isShieldActive = true;

        shieldImage.enabled = true;
        SetAlpha(1f);
    }

    void Update()
    {
        if (!isShieldActive || shieldImage == null) return;

        shieldTimer -= Time.deltaTime;

        if (shieldTimer <= 0)
        {
            Deactivate();
            return;
        }

        // Handle Blinking
        float timeRemaining = shieldTimer;
        if (timeRemaining <= blinkDuration)
        {
            // Calculate how far into the blink phase we are (0 to 1)
            float blinkProgress = 1f - (timeRemaining / blinkDuration);
            
            float currentBlinkSpeed = Mathf.Lerp(startBlinkSpeed, endBlinkSpeed, blinkProgress);
            float alpha = Mathf.PingPong(Time.time * currentBlinkSpeed, 1f);
            
            SetAlpha(alpha);
        }
        else
        {
            // Solid
            SetAlpha(1f);
        }
    }

    private void Deactivate()
    {
        isShieldActive = false;
        if (shieldImage != null)
        {
            shieldImage.enabled = false;
        }
    }

    private void SetAlpha(float alpha)
    {
        Color c = shieldImage.color;
        c.a = alpha;
        shieldImage.color = c;
    }
}