using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Collectible")]
public class CollectibleSO : ScriptableObject
{
    public GameObject prefab;
    public int spawnAmount;
    [Range(0f, 1f)] public float repawnPercent;
}
