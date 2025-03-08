using UnityEngine;
using Unity.Cinemachine;

public class CustomCinemachineInput : MonoBehaviour
{
    [Header("Assign the CinemachineInputAxisController here")]
    [SerializeField] private CinemachineInputAxisController inputController;

    void Start()
    {
        if (inputController == null)
            inputController = GetComponent<CinemachineInputAxisController>();

        // Start with camera movement disabled until player holds LMB.
        inputController.enabled = false;
    }

    void Update()
    {
        // While LMB held, enable input.
        if (Input.GetMouseButtonDown(0))
            inputController.enabled = true;
        
        // While LMB released, disable input.
        if (Input.GetMouseButtonUp(0))
            inputController.enabled = false;
    }
}