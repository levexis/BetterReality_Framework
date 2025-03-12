using UnityEngine;

public class MotionTracker : MonoBehaviour
{
    private Vector3 _lastPosition;
    public float Speed { get; private set; }
    public Vector3 Direction { get; private set; }
    private Vector3 _velocity;
    
    
    void Start()
    {
        _lastPosition = transform.position;
    }

    void Update()
    {
        // Calculate speed
        Vector3 currentPosition = transform.position;
        //again work out which, presumably speed can be calculated when it's got.
        Speed = Vector3.Distance(currentPosition, _lastPosition) / Time.deltaTime;
        _velocity = (transform.position - _lastPosition) / Time.deltaTime;
        
        
        // Calculate direction
        if (currentPosition != _lastPosition)
        { 
            Direction = (currentPosition - _lastPosition).normalized;
        }

        // Update last position
        _lastPosition = currentPosition;

        // Log or use the speed and direction as needed
//        Debug.Log("Speed: " + speed + " units/second");
//        Debug.Log("Direction: " + direction);
    }
    /// <summary>
    /// Gets the speed in a given direction
    /// </summary>
    /// <param name="direction"></param>
    public float GetSpeed(Vector3 direction)
    {
        // todo: default direction is direction it's travelling in
        return Vector3.Dot(_velocity,Direction); /// todo convert to property
    }

    /// <summary>
    /// Gets the primary direction
    /// </summary>
    /// <returns>
    /// Vector3.down etc
    public Vector3 GetPrimaryDirection()
    {
        // Define primary directions
        Vector3[] primaryDirections = {
            Vector3.up,
            Vector3.down,
            Vector3.left,
            Vector3.right,
            Vector3.forward,
            Vector3.back
        };

        // Initialize variables to track the closest direction
        float maxDot = -1f;
        Vector3 closestDirection = Vector3.zero;

        // Iterate over each primary direction
        foreach (Vector3 direction in primaryDirections)
        {
            // Calculate the dot product
            float dot = Vector3.Dot(Direction, direction);

            // Check if this direction is closer
            if (dot > maxDot)
            {
                maxDot = dot;
                closestDirection = direction;
            }
        }

        return closestDirection;
    }
}
