#nullable enable

using Assets.src.definitions;
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

    public static GameInstance? CurrentGameInstance { get; private set; }

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

        CurrentGameInstance = GameInstance.Create(definition);
    }

    private void Start()
    {
        
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
