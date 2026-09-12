using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float maxTurnSpeed;
    [SerializeField] private float angularDrag;
    private InputHandler input;
    private Rigidbody rb;
    
    private void Start()
    {
        input = GetComponent<InputHandler>();
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Movement();
        Turning();
    }

    private void Movement()
    {
        Vector3 moveVector = new(transform.forward.x * input.movingInput, 0, transform.forward.z * input.movingInput);
        rb.linearVelocity = moveVector * moveSpeed;
    }

    private void Turning()
    {
        Vector3 torqueVector = new(0, input.turningInput * turnSpeed, 0);

        rb.AddTorque(torqueVector);

        if(Mathf.Abs(input.turningInput) == 0 && Mathf.Abs(rb.angularVelocity.magnitude) != 0)
        {
            rb.AddTorque(-(rb.angularVelocity.normalized) * angularDrag);

            if (Mathf.Abs(rb.angularVelocity.magnitude) < 0.1) rb.angularVelocity = Vector3.zero;
        }

        if (Mathf.Abs(rb.angularVelocity.magnitude) > maxTurnSpeed) rb.angularVelocity = rb.angularVelocity.normalized * maxTurnSpeed;
    }
}
