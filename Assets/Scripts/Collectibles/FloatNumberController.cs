using UnityEngine;

public class FloatNumberController : MonoBehaviour
{
    [SerializeField] private GameObject coinPopupPrefab;
    [SerializeField] private GameObject lossPopupPrefab;
    [SerializeField] private GameObject hpPopupPrefab;
    [SerializeField] private GameObject fullAutoPopupPrefab;
    [SerializeField] private GameObject targetObject;

    [SerializeField] private Vector2 spawnPosRange;

    public void SpawnCoinPopup()
    {
        SpawnPopup(coinPopupPrefab);
    }
    public void SpawnLossPopup()
    {
        SpawnPopup(lossPopupPrefab);
    }
    public void SpawnHPPopup()
    {
        SpawnPopup(hpPopupPrefab);
    }
    public void SpawnFullAutoPopup()
    {
        SpawnPopup(fullAutoPopupPrefab);
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
