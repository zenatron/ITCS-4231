using UnityEngine;

public class TrackDisappear : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    bool Dcheck = false;
    float timer = 0.0f;
    Collider other;

    void Start ()
    {
        void OnCollisionEnter(Collision collision)
        {
            // Perform death logic here
            if (collision.collider.tag == "MainSphere")
            {
                Disappear(other.gameObject, true);//resets track visibility after a death
            }
        }
        
    }

    void Update()//step 2
    {
        if (Dcheck)//after ontrigger scripts sets dcheck to true, it means the next section is a disappearing cp
        {
            timer += Time.deltaTime;//timer starts
            if (timer > 3.0f)//after 3 seconds the disappear function executes
            {
                Disappear(other.gameObject, false);
            }
        }
    }

    void OnTriggerEnter(Collider other1) //step 1: starts on the trigger
    {
        Dcheck = false;//if a Dcp is reached, then a normal cp, this resets it to normal
        other = other1;
        if (other.gameObject.CompareTag("DCheckpoint"))//checks if cp is a disappearing cp or not
        {
            Dcheck = true;
            Debug.Log("Track disappear 1");
        }
    }

    void Disappear(GameObject checkpoint, bool visible)//step 3: disabling/reenabling visibility
    {
        for (int i = 0; i < checkpoint.transform.childCount; i++)//for every track in the section, disables its visibility
        {
            checkpoint.transform.GetChild(i).gameObject.GetComponent<Renderer>().enabled = visible;
        }
    }
}
