using UnityEngine;
using System;

public class TrackDisappear : MonoBehaviour
{
    [SerializeField] private Material trackMaterial;
    [SerializeField] private Material invisibleMaterial;
    float timer = 0.0f;
    private GameObject cpParent;
    [Header("Assign to the Checkpoint (green cube) of the track that is going to disappear")]
    [SerializeField]private GameObject cpToDisappear;
    private bool cpActivated;


    void Start ()
    {
        cpParent = this.gameObject;
    }

    void Update()
    {
        if (cpToDisappear.activeInHierarchy) {
            cpActivated = true;
        }
        if (cpActivated) {
            if (!cpToDisappear.activeInHierarchy) {
                timer += Time.deltaTime; // timer starts
            if (timer > 3.0f) // after 3 seconds the disappear function executes
            {
                Disappear(cpParent, false);
            }
            if (Time.timeScale == 0f){
                timer = 0f;
                Disappear(cpParent, true);
            }
            }   
        }
        
    }

    void Disappear(GameObject checkpoint, bool visible)//step 3: disabling/reenabling visibility
    {
        for (int i = 0; i < checkpoint.transform.childCount; i++)//for every track in the section, disables/reenables its visibility
        {
            if (visible)
                checkpoint.transform.GetChild(i).gameObject.GetComponent<Renderer>().material = trackMaterial;
            else
                checkpoint.transform.GetChild(i).gameObject.GetComponent<Renderer>().material = invisibleMaterial;
        }
    }
}