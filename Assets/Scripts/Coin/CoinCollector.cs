using System;
using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    [SerializeField] private FloatNumberController floatNumberController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            GameController.instance.GetComponent<CoinCounterController>().RequestCoinCounterUpdate(GetComponent<PlayerInfo>().playerSlot, 1);
            other.gameObject.GetComponent<Coin>().DestroySequence();
            floatNumberController.SpawnCollectorPopup();
        }
    }
}
