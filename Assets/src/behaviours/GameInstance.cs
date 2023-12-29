using Assets.src.definitions;
using Assets.src.definitions.tree;
using Assets.src.state2;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInstance : MonoBehaviour
{
    private JsonDefinitionRoot _definition;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PopulateFromDefinition(JsonDefinitionRoot definition)
    {
        // Already generated before
        if (_definition != null)
        {
            Debug.LogWarning($"Game instance {name} already generated. Not regenerating.");
            return;
        }

        _definition = definition;

        var gameInstance = this.gameObject;
        var universeTime = gameInstance.GetComponent<UniverseTime>();
        if ( universeTime == null )
        {
            Debug.LogError($"{nameof(GameInstance)} {gameInstance.name} missig behaviour {nameof(UniverseTime)}");
            Application.Quit(); // TODO: better error handling.
        }

        DefinitionsTraversal.Traverse_PassParent<GameObject>(
            _definition.Universe, 
            (JsonDefinitionNode definition, GameObject parent) =>
                {
                    var o = new GameObject(definition.Id);

                    o.transform.SetParent(parent.transform);

                    // TODO: also do "non-fixed" functions (if that's a thing?)
                    //       Check if there's a conflict with fixed functions.
                    if (definition.FixedOrbitFunctions != null)
                    {
                        o.AddComponent<FixedOrbitTimeTracker>();

                        // Add own functions
                        var fixedOrbitTimeTracker = o.GetComponent<FixedOrbitTimeTracker>();
                        fixedOrbitTimeTracker.AddFixedOrbitFunctions(definition.FixedOrbitFunctions);
                        
                        // Make the universe track this object.
                        universeTime.AddTimeTracker(fixedOrbitTimeTracker);

                        // Compile ancestors functions
                        fixedOrbitTimeTracker.CompileFixedOrbitFunctions();

                        // Add mesh
                        // (TODO : better mesh rendering)
                        var mesh = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        mesh.transform.SetParent(o.transform);
                        mesh.name = $"mesh-{definition.Id}";
                        var diameter = definition.Diameter;
                        if (diameter <= 0.0f)
                        {
                            Debug.LogError($"Entity {definition.Id} has diameter <= 0.0f");
                            diameter = 0.1f;
                        }
                        mesh.GetComponent<Renderer>().material.color = Color.red;
                        mesh.transform.localScale = new Vector3(diameter, diameter, diameter);
                    }


                    // TODO: also do "non-fixed" functions (if that's a thing?)
                    //       Check if there's a conflict with fixed functions.
                    //else if (...) {

                    //}

                    //TODO : Add all behaviours
                    // ..

                    // pass o down to children
                    return o;
                },
            gameInstance
        );
    }

    public static void Create(JsonDefinitionRoot definition)
    {
        // For now, only one game instance at all times.
        // In the future, maybe one per player (multiplayer)
        var id = 0;
        var o = new GameObject($"GameInstance_{id}");
        o.transform.SetParent(Root.Instance.transform);
        o.AddComponent<GameInstance>();
        o.AddComponent<UniverseTime>();

        var gameInstance = o.GetComponent<GameInstance>();
        //component.enabled = true; // needed?
        gameInstance.PopulateFromDefinition(definition);


        //return o;
    }
}
