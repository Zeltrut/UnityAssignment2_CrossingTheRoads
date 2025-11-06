using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] AudioSource collectFX;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            collectFX.Play();
            GameInfo.collectibleCount += 1;
            this.gameObject.SetActive(false);
        }
    }
}