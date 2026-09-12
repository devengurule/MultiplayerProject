using TMPro;
using UnityEngine;

public class CoinCounterController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI p1Text;
    [SerializeField] private TextMeshProUGUI p2Text;

    private int p1Counter = 0;
    private int p2Counter = 0;

    private void Awake()
    {
        UpdateCoinCounter(p1Text, p1Counter);
        UpdateCoinCounter(p2Text, p2Counter);
    }

    private void UpdateCoinCounter(TextMeshProUGUI textAsset, int coinAmount)
    {
        if(coinAmount.ToString().Length > 1)
        {
            textAsset.text = coinAmount.ToString();
        }
        else
        {
            textAsset.text = "0" + coinAmount.ToString();
        }
    }

    public void RequestCoinCounterUpdate(PlayerSlotEnum playerID, int updateAmount)
    {
        switch (playerID)
        {
            case PlayerSlotEnum.Player1:

                p1Counter += updateAmount;

                p1Counter = Mathf.Clamp(p1Counter, 0, 99);

                UpdateCoinCounter(p1Text, p1Counter);

                break;
            case PlayerSlotEnum.Player2:

                p2Counter += updateAmount;

                p2Counter = Mathf.Clamp(p2Counter, 0, 99);

                UpdateCoinCounter(p2Text, p2Counter);

                break;
            default:

                Debug.Log($"Updated Coin Counter: {playerID}");

                break;
        }
    }
}
