using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public Vector2 moveInput { get; private set; }
    private Controls controls;
    private PlayerInfo playerInfo;

    private void Awake()
    {
        playerInfo = GetComponent<PlayerInfo>();

        controls = new()
        {
            bindingMask = InputBinding.MaskByGroup(playerInfo.playerSlot.ToString())
        };

        controls.Player.Move.performed += OnMovePerformed;
        controls.Player.Move.canceled += OnMoveCanceled;
    }

    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        moveInput = Vector2.zero;
    }
}
