using UnityEngine;
using System.Collections;
using System;
using Random = System.Random;

public class TrackMovement : MonoBehaviour
{
    [Header("Movement Points")]
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;

    [Header("Settings")]
    private float moveDuration; // going between pointA and pointB
    [SerializeField] private float waitTime = 1.5f; // waiting at the points

    private void Start()
    {
        Random rnd = new();
        moveDuration = (float)((rnd.NextDouble() + 1) * 5);
        StartCoroutine(MovePingPong());
    }

    private IEnumerator MovePingPong()
    {
        while (true)
        {
            // Move from A to B
            yield return StartCoroutine(MoveBetweenPoints(pointA.position, pointB.position));
            yield return new WaitForSeconds(waitTime);

            // Move from B to A
            yield return StartCoroutine(MoveBetweenPoints(pointB.position, pointA.position));
            yield return new WaitForSeconds(waitTime);
        }
    }

    private IEnumerator MoveBetweenPoints(Vector3 start, Vector3 end)
    {
        float elapsed = 0f;
        while (elapsed < moveDuration)
        {
            float t = elapsed / moveDuration;
            transform.position = Vector3.Lerp(start, end, t);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = end;
    }
}