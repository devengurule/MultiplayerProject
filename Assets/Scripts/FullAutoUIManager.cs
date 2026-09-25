using UnityEngine;

public class FullAutoUIManager : MonoBehaviour
{
    private Quaternion initalRotation;

    private void Start()
    {
        initalRotation = GetComponent<RectTransform>().rotation;
    }
    private void Update()
    {
        GetComponent<RectTransform>().rotation = initalRotation;
    }
}
