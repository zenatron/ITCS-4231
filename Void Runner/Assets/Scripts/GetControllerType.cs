using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
public class GetControllerType : MonoBehaviour
{
    public static GetControllerType Instance {get; private set;}

    public enum Controller {
        XBOX,
        OTHER,
        COMPUTER
    }

    void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame

    public Controller GetController() {
        bool controllerConnected = Gamepad.all.Count > 0;
        Debug.Log("Controller connected: " + controllerConnected);
        if (controllerConnected) {
            return Controller.XBOX;
        } else {
            return Controller.COMPUTER;
        }
    }

    public string returnValue(Controller controller, Dictionary<Controller, string> controls) {
        foreach (var ele in controls) {
            if (ele.Key == controller) {
                return ele.Value;
            }
        }
        return "";
    }
}
