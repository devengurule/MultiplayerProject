using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private float trackSpeed;
    
    private float zOffset;

    private Vector3 velocity;

    private void Start()
    {
        zOffset = transform.position.z - target.transform.position.z;
    }

    private void FixedUpdate()
    {
        UpdatePosition();
    }


    private void UpdatePosition()
    {
        
        Vector3 targetPos = target.transform.position;
        Vector3 newPosition = new(targetPos.x, transform.position.y, targetPos.z + zOffset);
        
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, trackSpeed);

    }
}
