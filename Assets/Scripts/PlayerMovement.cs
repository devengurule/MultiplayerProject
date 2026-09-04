using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private InputHandler input;
    private Rigidbody rb;
    
    private void Start()
    {
        input = GetComponent<InputHandler>();
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector3 moveVector = new(input.moveInput.x, 0, input.moveInput.y);
        rb.linearVelocity = moveVector * moveSpeed;

        Debug.Log(rb.linearVelocity);
    }
}
