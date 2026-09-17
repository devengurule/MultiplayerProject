using UnityEngine;

public class GrowingShrinkingText : MonoBehaviour
{
    [SerializeField, Min(1f)] private float maxScale;
    [SerializeField] private float scaleSpeed;
    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.localScale = Vector3.one;
    }

    private void Update()
    {
        UpdateScale();
    }

    private void UpdateScale()
    {
        float scale = 1 + Mathf.PingPong(Time.time * scaleSpeed, maxScale - 1);

        rectTransform.localScale = Vector3.one * scale;
    }
}
