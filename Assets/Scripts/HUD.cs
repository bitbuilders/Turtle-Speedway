using System;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField]
    TMP_Text RaceTimerText;

    [SerializeField]
    TMP_Text CountdownText;

    [SerializeField]
    TMP_Text FinishedText;

    [SerializeField]
    float HideCountdownDelay = 0.5f;

    [SerializeField]
    float FinishedDelay = 1.5f;

    RaceTimer RaceTimer;

    void Start()
    {
        RaceTimerText.SetText("0.00");

        CountdownText.enabled = false;
        FinishedText.enabled = false;

        Race.Instance.OnRaceStart.AddListener(OnRaceStart);
        Race.Instance.OnRaceEnd.AddListener(OnRaceEnd);
        Race.Instance.OnCountdownSecond.AddListener(OnCountdownSecond);

        RaceTimer = SubsystemCreator.GetSubsystem<RaceTimer>();
    }

    void LateUpdate()
    {
        RaceTimerText.SetText(RaceTimer.RaceTime.ToString("0.00"));
    }

    void OnCountdownSecond(float second)
    {
        CountdownText.SetText(second.ToString("0"));
        CountdownText.enabled = true;
    }

    void OnRaceStart()
    {
        CountdownText.SetText("START!");

        Invoke(nameof(HideCountdown), HideCountdownDelay);
    }

    void OnRaceEnd()
    {
        FinishedText.enabled = true;

        Invoke(nameof(HideFinished), FinishedDelay);
    }

    void HideCountdown()
    {
        CountdownText.enabled = false;
    }

    void HideFinished()
    {
        FinishedText.enabled = false;
    }
}
