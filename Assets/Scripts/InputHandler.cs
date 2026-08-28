using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private Controls controls;

    private void Awake()
    {
        controls = new();

        controls.Player.Move.performed += OnMovePerformed;
        controls.Player.Move.canceled += OnMoveCanceled;
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        Debug.Log(context);
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {

    }
}
