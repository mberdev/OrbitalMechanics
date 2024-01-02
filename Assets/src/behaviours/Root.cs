using Assets.src.definitions;
using Assets.src.definitions.generator;
using Assets.src.state2;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Attach this script manually to the "Root" game object in the scene.
/// </summary>
public class Root : MonoBehaviour
{
    /// <summary>
    /// Singleton pattern
    /// </summary>
    public static Root Instance { get; private set; }

    /// <summary>
    /// Assign this in the editor with your favorite sun prefab.
    /// TODO: instantiate entirely from code. Why does Unity make it so hard?
    /// </summary>
    public GameObject sunPrefab;


    void Awake()
    {
        Instance = this;

        var definitions = new DefinitionsLoader().LoadAllDefinitionsAsync().GetAwaiter().GetResult();
        var definition = definitions[0]; // TODO : ability to choose which.

        //DEBUG: generated definition
        //var definition = new UniverseGenerator().Generate();

        CreateGameInstance(definition);
    }

    private void Start()
    {
        
    }

    private void CreateGameInstance(JsonDefinitionRoot definition)
    {
        GameInstance.Create(definition);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
