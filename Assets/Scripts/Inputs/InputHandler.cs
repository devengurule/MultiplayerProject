using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public float movingInput { get; private set; }
    public float turningInput { get; private set; }
    public bool flashlightToggle { get; private set; }

    private Controls controls;
    private PlayerInfo playerInfo;
    private bool isHeld;
    public event Action fireGun;

    private void Awake()
    {
        playerInfo = GetComponent<PlayerInfo>();

        controls = new()
        {
            bindingMask = InputBinding.MaskByGroup(playerInfo.playerSlot.ToString())
        };

        flashlightToggle = true;

        controls.Player.Moving.performed += OnMovingPerformed;
        controls.Player.Moving.canceled += OnMovingCancelled;

        controls.Player.Turning.performed += OnTurningPerformed;
        controls.Player.Turning.canceled += OnTurningCancelled;

        controls.Player.Flashlight.canceled += OnFlashlightCancelled;

        controls.Player.Shoot.started += OnShootStarted;
        controls.Player.Shoot.performed += OnShootPerformed;
        controls.Player.Shoot.canceled += OnShootCanceled;
    }

    private void Update()
    {
        if(isHeld && GetComponent<Gun>().fullAuto) fireGun?.Invoke();
    }

    private void OnEnable()
    {
        GameTimerController.timerEnded += OnGameEnd;

        if (controls != null)
        {
            controls.Enable();
        }
    }
    private void OnDisable()
    {
        GameTimerController.timerEnded -= OnGameEnd;

        if (controls != null)
        {
            controls.Disable();
        }
    }

    private void OnMovingPerformed(InputAction.CallbackContext context)
    {
        movingInput = context.ReadValue<float>();
    }
    private void OnMovingCancelled(InputAction.CallbackContext context)
    {
        movingInput = 0f;
    }


    private void OnTurningPerformed(InputAction.CallbackContext context)
    {
        turningInput = context.ReadValue<float>();
    }
    private void OnTurningCancelled(InputAction.CallbackContext context)
    {
        turningInput = 0f;
    }

    private void OnFlashlightCancelled(InputAction.CallbackContext context)
    {
        flashlightToggle = !flashlightToggle;
    }
    private void OnShootStarted(InputAction.CallbackContext context)
    {
        isHeld = true;
    }

    private void OnShootPerformed(InputAction.CallbackContext context)
    {
        if (!GetComponent<Gun>().fullAuto)
        {
            isHeld = false;
            fireGun?.Invoke();
        }
    }
    private void OnShootCanceled(InputAction.CallbackContext context)
    {
        if(GetComponent<Gun>().fullAuto) isHeld = false;
    }

    private void OnGameEnd()
    {
        gameObject.SetActive(false);
    }
}
