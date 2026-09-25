using UnityEngine;

public class GameController : MonoBehaviour
{
    [field: SerializeField] public int maxPlayerHealth { get; private set; }
    [field: SerializeField] public int bulletDamage { get; private set; }
    [field: SerializeField] public float bulletCooldown { get; private set; }
    [field: SerializeField] public float knockbackForce { get; private set; }
    [field: SerializeField] public int fullAutoBullets { get; private set; }
    [field: SerializeField] public float fullAutoCooldown { get; private set; }

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
