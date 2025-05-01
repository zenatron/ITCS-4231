using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class TrackMovement : MonoBehaviour
{
    [Header("Starting point")]
    [SerializeField] private Transform pointA;
    [Header("Ending point")]
    [SerializeField] private Transform pointB;
    private float speed = 1.0f;
    private float startTime;
    private float journeyLength;

    void Start() {
        //track = this.gameObject;
        startTime = Time.time;
        journeyLength = Vector3.Distance(pointA.position, pointB.position);
    }

    void Update() {
        float distCovered = (Time.time - startTime) * speed;
        float partOfJourney = distCovered / journeyLength;
        transform.position = Vector3.Lerp(pointA.position, pointB.position, partOfJourney);
    }
}