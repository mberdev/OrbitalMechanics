using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Add this behaviour to a GameInstance
/// </summary>
public class UniverseTime : MonoBehaviour
{
    public List<ITimeTracker> TimeTrackers { get;  } = new();

    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }

    public long CurrentTimeMs { get; private set;}

    /// <summary>
    /// Delta-time in-universe, not in Unity.
    /// </summary>
    public long fLastDeltaMs { get; private set;}
    public float fSpeed { get; private set;}

    // Reset time
    public void Reset()
    {
        CurrentTimeMs = 0;
        fSpeed = 1.0f;
    }

    public void AddTimeTracker(ITimeTracker timeTracker)
    {
        TimeTrackers.Add(timeTracker);
    }

    // To be called from an object's FixedUpdate
    public void FixedUpdate()
    {
        // TODO: handle negative speed.
        // TODO: handle negative delta
        var deltaMs = (long)(1000.0f * (fSpeed * UnityEngine.Time.fixedDeltaTime));

        var newTimeMs =  CurrentTimeMs + deltaMs;

        // Cannot go further back than beginning of time, duh.
        if (newTimeMs < 0)
        {
            deltaMs = -CurrentTimeMs; // From CurrentTimeMs, backwards to 0
            newTimeMs = 0;
        }

        CurrentTimeMs = newTimeMs;
        fLastDeltaMs = deltaMs;

        // time is not frozen
        if (deltaMs != 0.0f)
        {
            foreach (var timeTracker in TimeTrackers)
            {
                // TODO: this is actually a misuse of delta / current time.
                // we should rely on something like speed/2. See https://www.youtube.com/watch?v=yGhfUcPjXuE
                timeTracker.UpdateTime(CurrentTimeMs);
            }
        }


    }

}
