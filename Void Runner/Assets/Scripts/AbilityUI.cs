using UnityEngine;
using TMPro;

public class AbilityUI : MonoBehaviour
{
    public TextMeshProUGUI PowerTXT;

    // Update is called once per frame
    void Update()
    {
        SetPowerText();
    }

    private void SetPowerText() {
        PowerTXT.text = "Current Power: " + Powers.Instance.currPower.ToString();
    }
}
