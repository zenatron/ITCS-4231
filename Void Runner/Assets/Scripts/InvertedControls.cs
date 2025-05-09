using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class InvertedControls : MonoBehaviour
{
    private TextMeshProUGUI controlsTXT;
    private Dictionary<GetControllerType.Controller, string> inverted = new Dictionary<GetControllerType.Controller, string>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        inverted.Add(GetControllerType.Controller.XBOX, "Press the B button!");
        inverted.Add(GetControllerType.Controller.OTHER, "Use your assigned Ability 2 method!");
        inverted.Add(GetControllerType.Controller.COMPUTER, "Use the K key!");
    }

    void Start() {
        controlsTXT = GetComponent<TextMeshProUGUI>();
        GetControllerType.Controller controller = GetControllerType.Instance.GetController();
        controlsTXT.text += GetControllerType.Instance.returnValue(controller, inverted);
    }
    // USE THIS: GetControllerType.Instance.GetController()
}
