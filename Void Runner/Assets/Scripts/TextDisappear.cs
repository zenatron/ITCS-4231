using UnityEngine;

public class TextDisappear : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    bool Dcheck = false;
    float timer = 0.0f;
    Collider other;

    void Update ()
    {
        if (Dcheck)
        {
            timer += Time.deltaTime;
            if (timer > 3.0f)
            {
                Debug.Log("Track disappear 2"); 
                Disappear(other.gameObject);
            }
        }
    }

    void OnTriggerEnter(Collider other1)
    {
        other = other1;
        //other.gameObject.GetComponent<Renderer>().enabled = true;
        if (other.gameObject.CompareTag("DCheckpoint"))
        {
            Dcheck = true;
            Debug.Log("Track disappear 1");
        }
    }

    void Disappear(GameObject checkpoint)
    {
        //checkpoint.gameObject.GetComponent<Renderer>().enabled = false;
        for (int i = 0; i < checkpoint.transform.childCount; i++)
        {
            checkpoint.transform.GetChild(i).gameObject.GetComponent<Renderer>().enabled = false; 
            Debug.Log("track dissapear 3");
        }
    }
}
