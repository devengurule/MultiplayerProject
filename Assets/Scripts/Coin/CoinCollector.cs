using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            GameController.instance.GetComponent<CoinCounterController>().RequestCoinCounterUpdate(GetComponent<PlayerInfo>().playerSlot, 1);
            other.gameObject.GetComponent<Coin>().DestroySequence();
        }
    }
}
