#nullable enable


using Assets.src.computeShaders;
using Assets.src.computeShaders.converters;
using Assets.src.orbitFunctions;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Add this behaviour to any FIXED ORBIT space object that has a timeline and changes with time.
/// The fixed orbit(s) of this object need to exist in the global behaviour attached to the Universe. 
/// </summary>
public class FixedOrbitTimeTracker : MonoBehaviour, ITimeTracker
{
    public CSOrbitFunction[] FixedOrbitFunctions { get; set; } = new CSOrbitFunction[0];

    // Start is called before the first frame update
    void Start()
    {
    }


    public void UpdateTime(long timeMs)
    {
    }

}
