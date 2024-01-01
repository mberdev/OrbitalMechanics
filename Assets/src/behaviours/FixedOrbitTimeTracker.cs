#nullable enable

using Assets.src.definitions;
using Assets.src.definitions.converters;
using Assets.src.definitions.tree;
using Assets.src.math;
using Assets.src.orbitFunctions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Add this behaviour to any FIXED ORBIT space object that has a timeline and changes with time.
/// </summary>
public class FixedOrbitTimeTracker : MonoBehaviour, ITimeTracker
{
    public long lastTimeUpdateMs { get; private set; } = -1;

    // Same as FixedOrbitTimeTracker, but for all the parents, all the way to root.
    public List<IOrbitFunction> AncestorsFixedOrbitFunctions { get; private set; } = new();
    public List<IOrbitFunction> FixedOrbitFunctions { get; private set; } = new();

    // Not cached. That's the point.
    public List<IOrbitFunction> AllFixedOrbitFunctions => AncestorsFixedOrbitFunctions.Concat(FixedOrbitFunctions).ToList();
    
    // Start is called before the first frame update
    void Start()
    {
    }


    public void UpdateTime(long timeMs)
    {
        var offset = Vector3.zero;

        // TODO : do this computation with a shader.
        foreach (var function in AllFixedOrbitFunctions)
        {
            switch (function.Type)
            {
                case "OFFSET":
                    offset += ((OffsetOrbitFunction)function).Offset;
                    break;
                case "ELLIPSIS_XZ":
                    var ellipsisXZ = (EllipsisXZOrbitFunction)function;
                    offset += ellipsisXZ.Offset;
                    offset += new Vector3(
                        Ellipse_XZ.LerpEllipseX(timeMs, ellipsisXZ.HorizontalAxisX, ellipsisXZ.DurationMs),
                        0.0f, 
                        Ellipse_XZ.LerpEllipseZ(timeMs, ellipsisXZ.VerticalAxisZ, ellipsisXZ.DurationMs)
                    );
                    break;
                case "KEPLER":
                    var kepler = (KeplerOrbitFunction)function;
                    offset += kepler.Offset;
                    // TODO: Snapshot3D
                    var keplerSnapshot = new Kepler.Snapshot2D(kepler.Kepler, timeMs);
                    offset += new Vector3(
                        keplerSnapshot.X,
                        0.0f,
                        keplerSnapshot.Z                        
                    );
                    break;
                default:
                    Debug.LogError($"{nameof(FixedOrbitTimeTracker)}: Unknown function type {function.Type} for function {function.Id}");
                    break;
            }
        }

        this.transform.position = offset;
        
    }

    /// <summary>
    /// Adds the entire chain of functions all the way up to "root" (the GameInstance, really),
    /// so that we can calculate the position without any "dependency" to other nodes.
    /// </summary>
    public void CompileFixedOrbitFunctions()
    {
        // Safety : avoid infinite loop (?)
        if (this.transform.parent == this.transform.root)
        {
            Debug.LogError($"{nameof(FixedOrbitTimeTracker)}: Object {name} should be descending from of a {nameof(GameInstance)} but instead was found at root!");
            Application.Quit(); // TODO: better error handling.
        }

        var parent = this.transform.parent;
        var parentAsTimeTracker = parent.GetComponent<FixedOrbitTimeTracker>();

        // "root" (GameInstance, really)
        if (parentAsTimeTracker == null)
        {
            return;
        }

        // Add parent's ancestor functions and parent's own functions
        AncestorsFixedOrbitFunctions.AddRange(parentAsTimeTracker.AncestorsFixedOrbitFunctions);
        AncestorsFixedOrbitFunctions.AddRange(parentAsTimeTracker.FixedOrbitFunctions); 
    
        // Debug only
        Debug.Log($"{nameof(FixedOrbitTimeTracker)}: Object {name}'s functions compiled. Functions are {string.Join(", ", AllFixedOrbitFunctions.Select(f => f.Id))}");
    }
        
    public void AddFixedOrbitFunctions(List<JsonFixedOrbitFunction> functions)
    {
        foreach (var function in functions)
        {
            if (string.IsNullOrEmpty(function.Id))
            {
                Debug.LogError($"{nameof(FixedOrbitTimeTracker)}: Every function must have a unique Id! Object {name}");
                Application.Quit(); // TODO: better error handling.
            }

            if (string.IsNullOrEmpty(function.Type))
            {
                Debug.LogError($"{nameof(FixedOrbitTimeTracker)}: Type is null or empty for function {function.Id}");
            }

            switch(function.Type)
            {
                case "OFFSET":
                    FixedOrbitFunctions.Add(FixedOrbitFunctionsConverter.ToOffsetFunction(function));
                    break;

                case "ELLIPSIS_XZ":
                    FixedOrbitFunctions.Add(FixedOrbitFunctionsConverter.ToEllipsisXZFunction(function));
                    break;

                case "KEPLER":
                    FixedOrbitFunctions.Add(FixedOrbitFunctionsConverter.ToKeplerFunction(function));
                    break;

                default:
                    Debug.LogError($"{nameof(FixedOrbitTimeTracker)}: Unknown function type {function.Type} for function {function.Id}");
                    break;
            }
        }
    }

}
