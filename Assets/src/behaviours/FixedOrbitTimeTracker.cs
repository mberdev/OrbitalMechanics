using Assets.src.definitions;
using Assets.src.orbitFunctions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Add this behaviour to any FIXED ORBIT space object that has a timeline and changes with time.
/// </summary>
public class FixedOrbitTimeTracker : MonoBehaviour, ITimeTracker
{
    public long lastTimeUpdateMs { get; private set; } = -1;

    public List<IOrbitFunction> FixedOrbitFunctions { get; private set; } = new();

    // Start is called before the first frame update
    void Start()
    {
    }


    public void UpdateTime(long timeMs)
    {
        // TODO

        //var time = parent


        //// var lastDelta = _universeTime.LastDelta;

        //foreach (var celestialBody in CelestialBodies.Values)
        //{
        //    var orbitNode = celestialBody.OrbitNode;

        //    var x = orbitNode.X(time);
        //    var y = orbitNode.Y(time);
        //    var z = orbitNode.Z(time);

        //    var position = new Vector3(x, y, z);

        //    celestialBody.Mesh.transform.position = position;

        //}
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
                    var offset = new Vector3(
                        function.OffsetX ?? 0,
                        function.OffsetY ?? 0,
                        function.OffsetZ ?? 0
                    );

                    FixedOrbitFunctions.Add(new OffsetOrbitFunction(offset));
                    break;

                case "ELLIPSIS":
                    //TODO
                    //FixedOrbitFunctions.Add(new EllipsisOrbitFunction());
                    Debug.LogWarning($"{nameof(FixedOrbitTimeTracker)}: Ellipsis function not implemented yet for function {function.Id}");
                    break;

                default:
                    Debug.LogError($"{nameof(FixedOrbitTimeTracker)}: Unknown function type {function.Type} for function {function.Id}");
                    break;
            }
        }
    }


}
