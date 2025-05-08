using UnityEngine;
using TMPro;

public class WinTime : MonoBehaviour
{
    private TextMeshProUGUI winTime;
    public static WinTime Instance {get; private set;}

    void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
        winTime = GetComponent<TextMeshProUGUI>();
    }

    public void DisplayTime(string time) {
        winTime.text = "Your Time: " + time;
    }
}