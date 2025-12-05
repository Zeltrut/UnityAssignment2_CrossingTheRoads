using UnityEngine;

public class PowerUpItem : MonoBehaviour
{
    [Tooltip("How long the shield lasts in seconds.")]
    [SerializeField] private float duration = 5f;
    [SerializeField] private AudioSource pickupSound;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (pickupSound != null) 
            {
                // Use GlobalVolume here too
                AudioSource.PlayClipAtPoint(pickupSound.clip, transform.position, SoundEffectManager.GlobalVolume);
            }

            PlayerPowerUps playerPower = other.GetComponent<PlayerPowerUps>();
            if (playerPower != null)
            {
                playerPower.ActivateShield(duration);
            }

            gameObject.SetActive(false); 
        }
    }
}