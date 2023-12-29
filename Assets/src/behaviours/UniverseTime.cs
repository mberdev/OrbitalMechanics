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

    public long CurrentTime { get; private set;}
    public long LastDelta { get; private set;}
    public float Speed { get; private set;}

    // Reset time
    public void Reset()
    {
        CurrentTime = 0;
        Speed = 1.0f;
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
        LastDelta = (long)(Speed * UnityEngine.Time.fixedDeltaTime);

        CurrentTime += LastDelta;

        // Cannot go further back than beginning of time, duh.
        if (CurrentTime < 0)
        {
            CurrentTime = 0;
        }

        foreach (var timeTracker in TimeTrackers)
        {
            timeTracker.UpdateTime(CurrentTime);
        }
    }

}
