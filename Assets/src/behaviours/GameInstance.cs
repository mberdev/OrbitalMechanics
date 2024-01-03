using Assets.src.computeShaders;
using Assets.src.computeShaders.converters;
using Assets.src.definitions;
using Assets.src.definitions.converters;
using Assets.src.definitions.tree;
using Assets.src.extensions;
using Assets.src.meshes;
using Assets.src.orbitFunctions;
using Assets.src.state2;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
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

    //// Helper class to help traverse the tree
    //class ParentNode
    //{
    //    public GameObject parent;
    //    public int parentLastFunctionIndex;
    //}

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
        var universeTime = gameInstance.SafeGetComponent<UniverseTime>();
        var globalTimeTracker = gameInstance.SafeGetComponent<FixedOrbitsTimeTracker>();

        globalTimeTracker.FgManager = new();
        var manager = globalTimeTracker.FgManager;
        manager.StartAddingFunctions();

        DefinitionsTraversal.Traverse_PassParent<GameObject>(
            _definition.Universe, 
            (JsonDefinitionNode definition, GameObject parent, int depth) =>
                {
                    var o = new GameObject(definition.Id);
                    o.transform.SetParent(parent.transform);

                    var parentTimeTracker = parent.GetComponent<FixedOrbitTimeTracker>();
                    var fixedOrbitTimeTracker = o.AddComponent<FixedOrbitTimeTracker>();

                    // Make the universe track this object too.
                    universeTime.AddTimeTracker(fixedOrbitTimeTracker);

                    var functions = definition.FixedOrbitFunctions ?? new IOrbitFunction[0];
                    fixedOrbitTimeTracker.FixedOrbitFunctions = CSOrbitFunctionsConverter.Convert(functions);

                    // Create group
                    manager.AddGgroup(fixedOrbitTimeTracker, parentTimeTracker, depth);

                    // Add mesh
                    var meshDefinition = definition.Mesh;
                    if (meshDefinition != null)
                    {
                        AddMesh(o, definition.Id, meshDefinition);
                    }


                    return o;
                },
            gameInstance,
            depth: 0
        );

        manager.EndAddingFunctions();

    }

    private void AddMesh(GameObject o, string id, JsonMesh meshDefinition)
    {
        GameObject mesh;
        switch (meshDefinition.Type)
        {
            case "COLORED_SPHERE":
                mesh = MeshesGenerator.CreateSphereMesh(id, meshDefinition);
                break;
            case "SUN":
                mesh = MeshesGenerator.CreateSunMesh(id, meshDefinition);
                break;
            default:
                Debug.LogError($"Unknown mesh type {meshDefinition.Type} for {id}");
                return;
        }

        mesh.transform.SetParent(o.transform);
        mesh.name = $"{id}-mesh";
    }

    public static GameInstance Create(JsonDefinitionRoot definition)
    {
        // For now, only one game instance at all times.
        // In the future, maybe one per player (multiplayer)
        var id = 0;
        var o = new GameObject($"GameInstance_{id}");
        o.transform.SetParent(Root.Instance.transform);
        var gameInstance = o.AddComponent<GameInstance>();
        o.AddComponent<UniverseTime>();
        o.AddComponent<FixedOrbitsTimeTracker>(); // the global time tracker

        //gameInstance.enabled = true; // needed?
        gameInstance.PopulateFromDefinition(definition);

        return gameInstance;
    }
}
