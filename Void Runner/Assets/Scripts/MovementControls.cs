using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class MovementControls : MonoBehaviour
{
    private TextMeshProUGUI controlsTXT;
    private Dictionary<GetControllerType.Controller, string> movement = new Dictionary<GetControllerType.Controller, string>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        movement.Add(GetControllerType.Controller.XBOX, "Use the Left Joystick!");
        movement.Add(GetControllerType.Controller.OTHER, "Use your assigned movement method!");
        movement.Add(GetControllerType.Controller.COMPUTER, "Use the W, A, S, D keys!");
    }

    void Start() {
        controlsTXT = GetComponent<TextMeshProUGUI>();
        GetControllerType.Controller controller = GetControllerType.Instance.GetController();
        controlsTXT.text += GetControllerType.Instance.returnValue(controller, movement);
    }
    // USE THIS: GetControllerType.Instance.GetController()
}
