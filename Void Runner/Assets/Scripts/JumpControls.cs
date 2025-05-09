using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class JumpControls : MonoBehaviour
{
    private TextMeshProUGUI controlsTXT;
    private Dictionary<GetControllerType.Controller, string> jumping = new Dictionary<GetControllerType.Controller, string>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        jumping.Add(GetControllerType.Controller.XBOX, "Press the Right Trigger!");
        jumping.Add(GetControllerType.Controller.OTHER, "Use your assigned jumping method!");
        jumping.Add(GetControllerType.Controller.COMPUTER, "Use the Space Bar!");
    }

    void Start() {
        controlsTXT = GetComponent<TextMeshProUGUI>();
        GetControllerType.Controller controller = GetControllerType.Instance.GetController();
        controlsTXT.text += GetControllerType.Instance.returnValue(controller, jumping);
    }
    // USE THIS: GetControllerType.Instance.GetController()
}