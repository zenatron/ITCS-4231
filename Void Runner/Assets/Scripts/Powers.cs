using UnityEngine;

public class Powers : MonoBehaviour
{
    private Rigidbody rb;
    private Transform tf;
    private Material mat;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        tf = GetComponent<Transform>();
        mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            mat.color = Color.red;
        } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            mat.color = Color.green;
        } else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            mat.color = Color.blue;
        } else if (Input.GetKeyDown(KeyCode.Alpha4)) {
            mat.color = Color.yellow;
        }
    }
}
