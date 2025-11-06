using UnityEngine;

public class ItemBobber : MonoBehaviour
{
    [Header("Bobbing Settings")]
    [Tooltip("The vertical distance the object will bob up and down.")]
    [SerializeField] private float bobHeight = 0.2f;

    [Tooltip("The speed at which the object will bob.")]
    [SerializeField] private float bobSpeed = 2f;

    // Store the starting position of the object
    private Vector3 startPosition;
    
    // A random offset ensures that items don't all bob in perfect sync
    private float randomOffset;

    void Start()
    {
        // Save the original position when the game starts
        startPosition = transform.position;
        
        randomOffset = Random.Range(0f, 2f * Mathf.PI);
    }

    void Update()
    {
        float bobAmount = Mathf.Sin((Time.time * bobSpeed) + randomOffset) * bobHeight;
        
        // Apply the bobbing motion to the object's starting position
        transform.position = startPosition + new Vector3(0f, bobAmount, 0f);
    }
}