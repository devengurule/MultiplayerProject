using UnityEngine;

public class CoinRotation : MonoBehaviour
{
    [SerializeField] private Vector3 rotation;
    private Vector3 transformRotation;

    private void Start()
    {
        transformRotation = transform.eulerAngles;
    }

    private void Update()
    {
        transformRotation += rotation;

        transform.rotation = Quaternion.Euler(transformRotation);
    }
}
