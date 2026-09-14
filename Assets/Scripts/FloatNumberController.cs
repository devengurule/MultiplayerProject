using UnityEngine;

public class FloatNumberController : MonoBehaviour
{
    [SerializeField] private GameObject collectorPopupPrefab;
    [SerializeField] private GameObject lossPopupPrefab;
    [SerializeField] private GameObject targetObject;

    [SerializeField] private Vector2 spawnPosRange;

    public void SpawnCollectorPopup()
    {
        SpawnPopup(collectorPopupPrefab);
    }
    public void SpawnLossPopup()
    {
        SpawnPopup(lossPopupPrefab);
    }

    private void SpawnPopup(GameObject prefab)
    {
        GameObject popupObject = Instantiate(prefab, GetComponent<RectTransform>());
        Vector3 spawnPosRandomizer = new(RandomNumberInRange(), RandomNumberInRange(), RandomNumberInRange());
        popupObject.GetComponent<RectTransform>().position = targetObject.transform.position + spawnPosRandomizer;
    }

    private float RandomNumberInRange()
    {
        return Random.Range(spawnPosRange.x, spawnPosRange.y);
    }
}
