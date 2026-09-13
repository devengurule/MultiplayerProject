using System;
using UnityEngine;

public class FloatNumberController : MonoBehaviour
{
    [SerializeField] private GameObject collectorPopupPrefab;
    [SerializeField] private GameObject targetObject;

    public void SpawnCollectorPopup()
    {
        GameObject popupObject = Instantiate(collectorPopupPrefab, GetComponent<RectTransform>());
        popupObject.GetComponent<RectTransform>().position = targetObject.transform.position;
    }
}
