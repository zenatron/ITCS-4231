using UnityEngine;
using TMPro;

class Timer : MonoBehaviour
{
    public string time;
    public TextMeshProUGUI totalTime;
    private float secondsCount;
    private int minuteCount;

    void Start()
    {
        secondsCount = 0;
        minuteCount = 0;
        SetTimerText();
    }


    void Update()
    {
        UpdateTimerUI();
        SetTimerText();
    }

    void SetTimerText()
    {
        totalTime.text = "Time: " + time.ToString();
    }

    //call this on update
    public void UpdateTimerUI()
    {
        //set timer UI
        secondsCount += Time.deltaTime;
        time = minuteCount + " m, " + (int)secondsCount + " s ";
        if (secondsCount >= 60)
        {
            minuteCount++;
            secondsCount = 0;
        }

    }
}
