using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public enum PlayerSlotEnum
    {
        Player1,
        Player2
    }

    [field: SerializeField] public PlayerSlotEnum playerSlot { get; private set; }
}
