using UnityEngine;
using System.Collections;

public class TextSway : MonoBehaviour
{

    public RectTransform blurb;
    public float degrees;
    public float time;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(StartSway());
    }

    IEnumerator StartSway() {
        while (true) {
            float angle = Mathf.Sin(Time.realtimeSinceStartup * time) * degrees;
            blurb.rotation = Quaternion.Euler(0, 0, angle);
            yield return null;
        }
    }

}
