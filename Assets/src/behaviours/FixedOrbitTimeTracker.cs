#nullable enable


using Assets.src.orbitFunctions;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Add this behaviour to any FIXED ORBIT space object that has a timeline and changes with time.
/// </summary>
public class FixedOrbitTimeTracker : MonoBehaviour, ITimeTracker
{
    public long lastTimeUpdateMs { get; private set; } = -1;

    // Both its own functions and its parent's functions, in order from root (parents) to leaf (this object).
    public IOrbitFunction[] FixedOrbitFunctions { get; set; } = new IOrbitFunction[0];


    // Start is called before the first frame update
    void Start()
    {
    }


    public void UpdateTime(long timeMs)
    {
        this.transform.position = ProcessAllFunctions(timeMs);
    }

    // TODO : Do this with a compute shader.
    private Vector3 ProcessAllFunctions(long timeMs)
    {
        var offset = Vector3.zero;

        // TODO : do this computation with a shader.
        foreach (var function in FixedOrbitFunctions)
        {
            var fOffset = new Vector3(
                function.OffsetX ?? 0.0f,
                function.OffsetY ?? 0.0f,
                function.OffsetZ ?? 0.0f
            );

            offset += fOffset;

            switch (function.Type)
            {
                case "OFFSET":
                    break;
                case "ELLIPSIS_XZ":
                    var ellipsisXZ = (EllipsisXZOrbitFunction)function;
                    offset += new Vector3(
                        ellipsisXZ.LerpEllipseX(timeMs),
                        0.0f,
                        ellipsisXZ.LerpEllipseZ(timeMs)
                    );
                    break;
                case "KEPLER":
                    var kepler = (KeplerOrbitFunction)function;
                    // TODO: Snapshot3D
                    //TEST ONLY!
                    long slowTime = timeMs / 10;
                    //-TEST ONLY!
                    var keplerSnapshot = new KeplerOrbitFunction.Snapshot2D(kepler, /* timeMs*/ slowTime);
                    offset += new Vector3(
                        keplerSnapshot.X,
                        0.0f,
                        keplerSnapshot.Z
                    );
                    break;

                case "LAGUE_KEPLER":
                    var lagueKepler = (LagueKeplerOrbitFunction)function;
                    //TEST ONLY!
                    double slowTime2 = timeMs / 1000.0;
                    //-TEST ONLY!

                    var centerOfMass = Vector2.zero; // TODO
                    var angleRadiants = slowTime2; //TODO
                    offset += new Vector3(
                        lagueKepler.X_FromAngleRadiants(centerOfMass, angleRadiants),
                        0.0f,
                        lagueKepler.Y_FromAngleRadiants(centerOfMass, angleRadiants)
                    );
                    break;

                default:
                    Debug.LogError($"{nameof(FixedOrbitTimeTracker)}: Unknown function type {function.Type} for function {function.Id}");
                    break;
            }
        }
        return offset;
    }

    // Helper to safely get the orbit functions from any kind of game object
    public static IOrbitFunction[] GetOrbitFunctions(GameObject o)
    {
        var asOrbitTimeTracker = o.GetComponent<FixedOrbitTimeTracker>();
        if (asOrbitTimeTracker != null)
        {
            return asOrbitTimeTracker.FixedOrbitFunctions;
        } else
        {
            // Maybe we've reached the root, or whatever other reason why we're no longer looking
            // at actual time trackers.
            return new IOrbitFunction[0];
        }
    }



}
