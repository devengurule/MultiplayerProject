using UnityEngine;

public class FloatNumberController : MonoBehaviour
{
    [SerializeField] private GameObject collectorPopupPrefab;
    [SerializeField] private GameObject lossPopupPrefab;
    [SerializeField] private GameObject targetObject;

    public void SpawnCollectorPopup()
    {
        GameObject popupObject = Instantiate(collectorPopupPrefab, GetComponent<RectTransform>());
        popupObject.GetComponent<RectTransform>().position = targetObject.transform.position;
    }
    public void SpawnLossPopup()
    {
        GameObject popupObject = Instantiate(lossPopupPrefab, GetComponent<RectTransform>());
        popupObject.GetComponent<RectTransform>().position = targetObject.transform.position;
    }
}
