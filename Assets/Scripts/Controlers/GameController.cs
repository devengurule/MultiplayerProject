using UnityEngine;

public class GameController : MonoBehaviour
{
    //[field: SerializeField, Range(0f, 1f)] public float lightLossPercent { get; private set; }
    //[field: SerializeField, Range(0f, 1f)] public float heavyLossPercent { get; private set; }
    [field: SerializeField] public int maxPlayerHealth { get; private set; }
    [field: SerializeField] public int bulletDamage { get; private set; }
    [field: SerializeField] public float knockbackForce { get; private set; }

    public static GameController instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
