using UnityEngine;

public class TextFaceCamera : MonoBehaviour
{
    [SerializeField]private Transform playerT;
    private Transform mainCam;
    [SerializeField]private CanvasGroup CG;
    [SerializeField] private float distanceMax = 3.5f;

    void Start() {
        mainCam  = Camera.main.transform;
        if (CG == null)
        {
            CG = GetComponent<CanvasGroup>();
        }
    }

    void Update() {
        transform.LookAt(mainCam);
        transform.RotateAround(transform.position, transform.up, 180f);

        float dist = Vector3.Distance(playerT.position, transform.position);

        
        if (dist > distanceMax) {
            CG.alpha = 0;
        } else {
            CG.alpha = 1;
        }
    }
}
