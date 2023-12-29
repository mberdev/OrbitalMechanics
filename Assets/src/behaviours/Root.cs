using Assets.src.definitions;
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

    void Awake()
    {
        Instance = this;

        var definitions = new DefinitionsLoader().LoadAllDefinitionsAsync().GetAwaiter().GetResult();
        var definition = definitions[0]; // TODO : ability to choose which.
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
