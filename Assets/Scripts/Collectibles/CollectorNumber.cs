using TMPro;
using UnityEngine;

public class CollectorNumber : MonoBehaviour
{
    [SerializeField] private float verticalMoveSpeed;
    [SerializeField] private float alphaLossRate;

    private RectTransform rectTranform;
    private TextMeshProUGUI text;

    private void Awake()
    {
        rectTranform = GetComponent<RectTransform>();
        text = GetComponent<TextMeshProUGUI>();
    }

    private void FixedUpdate()
    {
        rectTranform.anchoredPosition += Vector2.up * verticalMoveSpeed;
        
        text.alpha -= alphaLossRate;

        if (text.alpha <= 0f)
        {
            Destroy(gameObject);
        }
    }
}