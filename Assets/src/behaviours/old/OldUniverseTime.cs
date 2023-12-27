using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldUniverseTime : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }

    public float CurrentTime { get; private set;}
    public float LastDelta { get; private set;}
    public float Speed { get; private set;}

    // Reset time
    public void Reset()
    {
        CurrentTime = 0;
        Speed = 1.0f;
    }

    // To be called from an object's FixedUpdate
    public void FixedUpdate()
    {
        LastDelta = Speed * UnityEngine.Time.fixedDeltaTime;
        CurrentTime += LastDelta;
    }

}
