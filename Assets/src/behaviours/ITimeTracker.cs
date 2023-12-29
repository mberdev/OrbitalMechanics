using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// All TimeTracker behaviours must implement this interface.
/// Add TimeTracker behaviours to any space object that has a timeline and changes with time.
/// </summary>
public interface ITimeTracker
{
    public void UpdateTime(long timeMs);

}
