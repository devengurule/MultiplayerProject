using UnityEngine;

public class GameController : MonoBehaviour
{
    [field: SerializeField] public int maxPlayerHealth { get; private set; }
    [field: SerializeField] public int bulletDamage { get; private set; }
    [field: SerializeField] public float bulletCooldown { get; private set; }
    [field: SerializeField] public float knockbackForce { get; private set; }
    [field: SerializeField] public int fullAutoBullets { get; private set; }
    [field: SerializeField] public float fullAutoCooldown { get; private set; }
    [SerializeField] private GameObject pause;

    public bool isPaused { get; private set; }
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
    private void OnEnable() => InputHandler.PauseGame += OnPauseGame;
    private void OnDisable() => InputHandler.PauseGame -= OnPauseGame;

    public void OnPauseGame()
    {
        if (pause.activeSelf)
        {
            pause.SetActive(false);
            isPaused = false;
        }
        else
        {
            pause.SetActive(true);
            isPaused = true;
        }
    }
}
