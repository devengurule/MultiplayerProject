using UnityEngine;
using UnityEngine.Rendering;

public class LightCulling : MonoBehaviour
{
    [SerializeField] private Light targetLight;
    private Camera localCamera;

    private void Awake()
    {
        // Cache the camera component this script is attached to
        localCamera = GetComponent<Camera>();
    }

    private void OnEnable()
    {
        // Subscribe to URP camera rendering events
        RenderPipelineManager.beginCameraRendering += OnBeginCameraRendering;
        RenderPipelineManager.endCameraRendering += OnEndCameraRendering;
    }

    private void OnDisable()
    {
        // Always unsubscribe to prevent memory leaks
        RenderPipelineManager.beginCameraRendering -= OnBeginCameraRendering;
        RenderPipelineManager.endCameraRendering -= OnEndCameraRendering;
    }

    private void OnBeginCameraRendering(ScriptableRenderContext context, Camera cam)
    {
        // If URP is currently rendering THIS camera, turn off the light
        if (cam == localCamera && targetLight != null)
        {
            targetLight.enabled = false;
        }
    }

    private void OnEndCameraRendering(ScriptableRenderContext context, Camera cam)
    {
        // Turn the light back on as soon as this camera finishes rendering
        if (cam == localCamera && targetLight != null)
        {
            targetLight.enabled = true;
        }
    }
}
