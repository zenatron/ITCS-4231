using UnityEngine;

public class OutlineRotator : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.SetActive(true);
        gameObject.GetComponent<Renderer>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Physics.Linecast(Camera.main.transform.position, transform.position))
        {
            gameObject.GetComponent<Renderer>().enabled = false;
            Debug.Log("Outline inactive");
        } /*else if (Physics.Linecast(Camera.main.transform.position, transform.position))
        {
            gameObject.SetActive(true);
            Debug.Log("Outline active");
        }*/
    }
}
