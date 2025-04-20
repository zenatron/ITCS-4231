using UnityEngine;
using System.Collections;

public class TextSway : MonoBehaviour
{
    public float swaySpeed = 10f;
    public float swayAmount = 15f;
    public float positionSwayAmount = 25f;

    RectTransform rect;
    Vector3 originalPosition;
    
    void Start() {
        rect = GetComponent<RectTransform>();
        originalPosition = rect.anchoredPosition;
    }
    
    void Update() {
        float sway = Mathf.PingPong(Time.unscaledTime * swaySpeed, 1f);
        float angle = Mathf.Lerp(-swayAmount, swayAmount, sway);
        rect.rotation = Quaternion.Euler(0, 0, angle);

        float xOffset = Mathf.Lerp(-positionSwayAmount, positionSwayAmount, sway);
        rect.anchoredPosition = originalPosition + new Vector3(xOffset, 0f, 0f);
    }
}
