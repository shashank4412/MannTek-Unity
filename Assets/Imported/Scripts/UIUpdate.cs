using UnityEngine;

public class UIUpdate : MonoBehaviour
{
    [SerializeField] private Transform targetObject;
    private Transform uiObject;

    private void Start()
    {
        uiObject = gameObject.transform;
    }

    // Follow the targetObject position
    private void Update()
    {
        if (targetObject != null && uiObject != null)
        {
            Vector3 targetPosition = Camera.main.WorldToScreenPoint(targetObject.position);
            uiObject.position = targetPosition;
        }
    }
}