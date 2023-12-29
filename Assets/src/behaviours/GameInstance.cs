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
            (definition, parent) =>
                {
                    var o = new GameObject(definition.Id);

                    if (parent != null)
                        o.transform.SetParent(parent.transform);
                    else // root
                        o.transform.SetParent(transform);

                    // TODO: also do "non-fixed" functions (if that's a thing?)
                    //       Check if there's a conflict with fixed functions.
                    if (definition.FixedOrbitFunctions != null)
                    {
                        o.AddComponent<FixedOrbitTimeTracker>();
                        var fixedOrbitTimeTracker = o.GetComponent<FixedOrbitTimeTracker>();
                        fixedOrbitTimeTracker.AddFixedOrbitFunctions(definition.FixedOrbitFunctions);
                        universeTime.AddTimeTracker(fixedOrbitTimeTracker);
                    }

                    //TODO : Add all behaviours
                    // ..

                    // pass o down to children
                    return o;
                },
            gameInstance
        );

        //// Generate meshes
        //foreach (var entity in snapshot.Entities)
        //{
        //    var definition = entity.Value.Definition;
        //    var mesh = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        //    mesh.transform.SetParent(transform);
        //    mesh.name = entity.Key;
        //    var diameter = definition.Diameter;
        //    if (diameter <= 0.0f)
        //    {
        //        Debug.LogError($"Entity {entity.Key} has diameter <= 0.0f");
        //        diameter = 0.1f;
        //    }

        //    mesh.transform.localScale = new Vector3(diameter, diameter, diameter);

        //    //TODO : temp : 
        //    var orbitFunction = definition.FixedOrbitFunctions[0];
        //    var position = new Vector3(orbitFunction.OffsetX, orbitFunction.OffsetY, orbitFunction.OffsetZ);
        //    mesh.transform.position = position;

        //    mesh.GetComponent<Renderer>().material.color = Color.red;

        //    //TODO: link back to snapshot entity?
        //}


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
