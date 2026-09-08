using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public Vector2 movingInput { get; private set; }
    public Vector2 turningInput { get; private set; }
    private Controls controls;
    private PlayerInfo playerInfo;

    private void Awake()
    {
        playerInfo = GetComponent<PlayerInfo>();

        controls = new()
        {
            bindingMask = InputBinding.MaskByGroup(playerInfo.playerSlot.ToString())
        };

        //controls.Player.Move.performed += OnMovePerformed;
        //controls.Player.Move.canceled += OnMoveCanceled;

        controls.Player.Moving.performed += OnMovingPerformed;
        controls.Player.Moving.canceled += OnMovingCancelled;

        controls.Player.Turning.performed += OnTurningPerformed;
        controls.Player.Turning.canceled += OnTurningCancelled;
    }

    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }

    //private void OnMovePerformed(InputAction.CallbackContext context)
    //{
    //    moveInput = context.ReadValue<Vector2>();
    //}

    //private void OnMoveCanceled(InputAction.CallbackContext context)
    //{
    //    moveInput = Vector2.zero;
    //}

    private void OnMovingPerformed(InputAction.CallbackContext context)
    {
        movingInput = context.ReadValue<Vector2>();
    }
    private void OnMovingCancelled(InputAction.CallbackContext context)
    {
        movingInput = Vector2.zero;
    }


    private void OnTurningPerformed(InputAction.CallbackContext context)
    {
        turningInput = context.ReadValue<Vector2>();
    }
    private void OnTurningCancelled(InputAction.CallbackContext context)
    {
        turningInput = Vector2.zero;
    }
}
