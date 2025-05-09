using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class FastSlowControls : MonoBehaviour
{
    private TextMeshProUGUI controlsTXT;
    private Dictionary<GetControllerType.Controller, string> fastSlow = new Dictionary<GetControllerType.Controller, string>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        fastSlow.Add(GetControllerType.Controller.XBOX, "To use the Fast Ability press the X button. To use the Slow Ability press the Y button");
        fastSlow.Add(GetControllerType.Controller.OTHER, "Use your assigned Ability 1 method to go faster and use your Ability 3 method to go slower!");
        fastSlow.Add(GetControllerType.Controller.COMPUTER, "To use the Fast Ability press the J key. To use the Slow Ability press the L key");
    }

    void Start() {
        controlsTXT = GetComponent<TextMeshProUGUI>();
        GetControllerType.Controller controller = GetControllerType.Instance.GetController();
        controlsTXT.text += GetControllerType.Instance.returnValue(controller, fastSlow);
    }
    // USE THIS: GetControllerType.Instance.GetController()
}