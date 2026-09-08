using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    [SerializeField] private GameObject flashlight;
    private InputHandler input;

    private void Start()
    {
        input = GetComponent<InputHandler>();
    }

    private void Update()
    {
        toggleFlashlight();
    }

    private void toggleFlashlight()
    {
        if (input.flashlightToggle == true) flashlight.SetActive(true);
        else flashlight.SetActive(false);
    }
}
