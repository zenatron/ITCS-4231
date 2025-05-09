using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class LookingControls : MonoBehaviour
{
    private TextMeshProUGUI controlsTXT;
    private Dictionary<GetControllerType.Controller, string> looking = new Dictionary<GetControllerType.Controller, string>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        looking.Add(GetControllerType.Controller.XBOX, "Use the Right Joystick!");
        looking.Add(GetControllerType.Controller.OTHER, "Use your assigned looking method!");
        looking.Add(GetControllerType.Controller.COMPUTER, "Use the mouse!");
    }

    void Start() {
        controlsTXT = GetComponent<TextMeshProUGUI>();
        GetControllerType.Controller controller = GetControllerType.Instance.GetController();
        controlsTXT.text += GetControllerType.Instance.returnValue(controller, looking);
    }
    // USE THIS: GetControllerType.Instance.GetController()
}
