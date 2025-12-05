using UnityEngine;
using System.Collections;

public class PlayerPowerUps : MonoBehaviour
{
    public bool IsInvincible { get; private set; } = false;

    public void ActivateShield(float duration)
    {
        StopAllCoroutines(); 
        StartCoroutine(ShieldRoutine(duration));

        // Use the Singleton Instance directly
        if (ShieldUI.Instance != null)
        {
            ShieldUI.Instance.ActivateShieldUI(duration);
        }
    }

    private IEnumerator ShieldRoutine(float duration)
    {
        IsInvincible = true;
        Debug.Log("Shield Activated!");
        
        yield return new WaitForSeconds(duration);

        IsInvincible = false;
        Debug.Log("Shield Deactivated.");
    }
}