using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [Header("Movement")]
    [Tooltip("The local direction the object will move (e.g., (1, 0, 0) for its local right, (0, 0, 1) for its local forward).")]
    [SerializeField] private Vector3 moveAxis = Vector3.right;

    [Tooltip("The total distance the object will travel from its start point.")]
    [SerializeField] private float moveDistance = 10f;

    [Tooltip("How fast the object moves back and forth.")]
    [SerializeField] private float moveSpeed = 3f;

    private Vector3 startPosition;
    
    private Vector3 worldMoveDirection;

    void Start()
    {
        startPosition = transform.position;

        // Convert the local moveAxis into a world-space direction based on the object's rotation.
        worldMoveDirection = transform.TransformDirection(moveAxis.normalized);
    }

    void Update()
    {
        if (PauseController.IsGamePaused)
        {
            return;
        }

        float pingPongValue = Mathf.PingPong(Time.time * moveSpeed, moveDistance);

        transform.position = startPosition + (worldMoveDirection * pingPongValue);
    }
}