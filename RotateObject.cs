using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [Tooltip("The speed at which the object will rotate on the Y-axis.")]
    [SerializeField] private float rotateSpeed = 100f;
    
    [Tooltip("Which axis to rotate around.")]
    [SerializeField] private Vector3 rotateAxis = Vector3.up;

    [Tooltip("The space to rotate in. World space is usually best.")]
    [SerializeField] private Space rotateSpace = Space.World;

    void Update()
    {
        // Rotate the object every frame
        transform.Rotate(rotateAxis * rotateSpeed * Time.deltaTime, rotateSpace);
    }
}