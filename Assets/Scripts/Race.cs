using System;
using UnityEngine;
using UnityEngine.Events;

public enum RaceState
{
    Countdown,
    Racing,
    Cooldown,
    Finished
}

public class Race : MonoBehaviour
{
    [SerializeField]
    float StartCountdown = 5.1f;

    public UnityEvent OnRaceStart;
    public UnityEvent OnRaceEnd;
    public UnityEvent<float> OnCountdownSecond;

    public static Race Instance { get; private set; }

    public RaceState State { get; private set; }

    float CountdownTimer = 0.0f;
    float PrevCountdownTimer = 0.0f;

    void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        OnRaceStart = new UnityEvent();
        OnRaceEnd = new UnityEvent();
        OnCountdownSecond = new UnityEvent<float>();

        CountdownTimer = StartCountdown;
        PrevCountdownTimer = CountdownTimer;

        State = RaceState.Countdown;
    }

    void Update()
    {
        if (CountdownTimer > 0.0f)
        {
            CountdownTimer -= Time.deltaTime;
            if (CountdownTimer <= 0.0f)
            {
                State = RaceState.Racing;
                OnRaceStart.Invoke();
                return;
            }

            var CurFloor = Mathf.Floor(CountdownTimer);
            var PrevFloor = Mathf.Floor(PrevCountdownTimer);
            if (!Mathf.Approximately(CurFloor, PrevFloor))
            {
                OnCountdownSecond.Invoke(PrevFloor);
            }

            PrevCountdownTimer = CountdownTimer;
        }
    }

    public void FinishRace()
    {
        State = RaceState.Finished;
        OnRaceEnd.Invoke();
    }
}
