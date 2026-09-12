using UnityEngine;

public class CoinRotation : MonoBehaviour
{
    [SerializeField] private Vector3 rotation;
    [SerializeField, Range(0f,1f)] private float startRandomizer;
    private Vector3 transformRotation;

    private void Start()
    {
        Animator animator = GetComponent<Animator>();

        if(animator != null ) animator.Play("CoinOscillation", 0, Random.Range(0, startRandomizer));

        Vector3 randomStartAngles = new(Random.Range(0, startRandomizer * 360), Random.Range(0, startRandomizer * 360), Random.Range(0, startRandomizer * 360));

        transform.eulerAngles = randomStartAngles;

        transformRotation = transform.eulerAngles;
    }

    private void Update()
    {
        transformRotation += rotation;

        transform.rotation = Quaternion.Euler(transformRotation);
    }
}
