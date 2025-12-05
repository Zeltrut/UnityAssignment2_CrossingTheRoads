using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private AudioSource collectFX;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            GameInfo gameInfo = FindAnyObjectByType<GameInfo>();
            if (gameInfo != null)
            {
                gameInfo.AddCollectible();
            }

            if (collectFX != null)
            {
                // Create a temporary audio source at the position
                // Use the GlobalVolume from SoundEffectManager
                AudioSource.PlayClipAtPoint(collectFX.clip, transform.position, SoundEffectManager.GlobalVolume); 
            }

            this.gameObject.SetActive(false); 
        }
    }
}