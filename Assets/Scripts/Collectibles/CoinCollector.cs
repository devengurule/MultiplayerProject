using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private GameObject coinFolder;
    [SerializeField, Range(0f, 1f)] private float coinEjectPercent;
    [SerializeField] private Vector2 coinEjectForce;
    [SerializeField] private FloatNumberController floatNumberController;

    private void OnEnable()
    {
        GetComponent<StunController>().OnLightStun += OnLightStun;
        GetComponent<StunController>().OnHeavyStun += OnHeavyStun;
    }
    private void OnDisable()
    {
        GetComponent<StunController>().OnLightStun -= OnLightStun;
        GetComponent<StunController>().OnHeavyStun -= OnHeavyStun;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin") && !GetComponent<StunController>().isStunned)
        {
            GameController.instance.GetComponent<CoinCounterController>().RequestCoinCounterUpdate(GetComponent<PlayerInfo>().playerSlot, 1);
            other.gameObject.GetComponent<Coin>().DestroySequence();
            floatNumberController.SpawnCollectorPopup();
        }
    }

    private void OnLightStun()
    {
        int currentAmount = GameController.instance.GetComponent<CoinCounterController>().RequestCoinAmount(GetComponent<PlayerInfo>().playerSlot);
        GameController.instance.GetComponent<CoinCounterController>().RequestCoinCounterUpdate(GetComponent<PlayerInfo>().playerSlot, -1);
        if (currentAmount > 0) SpawnCoins(1);
    }

    private void OnHeavyStun()
    {
        int currentAmount = GameController.instance.GetComponent<CoinCounterController>().RequestCoinAmount(GetComponent<PlayerInfo>().playerSlot);
        int ejectAmount = (int)((float)currentAmount * coinEjectPercent);

        GameController.instance.GetComponent<CoinCounterController>().RequestCoinCounterUpdate(GetComponent<PlayerInfo>().playerSlot, -ejectAmount);
        if(currentAmount > 0) SpawnCoins(ejectAmount);
    }

    private void SpawnCoins(int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            GameObject coin = Instantiate(coinPrefab, new(transform.position.x, 0.25f, transform.position.z), Quaternion.identity);
            coin.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized * Random.Range(coinEjectForce.x, coinEjectForce.y), ForceMode.Impulse);
            coin.transform.parent = coinFolder.transform;
            floatNumberController.SpawnLossPopup();
        }
    }
}
