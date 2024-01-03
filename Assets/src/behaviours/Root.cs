#nullable enable

using Assets.src.definitions;
using Assets.src.definitions.generator;
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

    // TODO: instantiate entirely from code. Why does Unity make it so hard?
    public ComputeShader orbitFunctionsComputeShader;

    public delegate void OnGameInstanceCreated(GameInstance? instance, int count);
    public OnGameInstanceCreated onGameInstanceCreated;

    void Awake()
    {
        Instance = this;

        if (sunPrefab == null)
        {
            Debug.LogError("Missing sun prefab. Please assign it in the editor.");
            Application.Quit(); // TODO: better error handling.
        }

        if (orbitFunctionsComputeShader == null)
        {
            Debug.LogError("Missing orbit functions compute shader. Please assign it in the editor.");
            Application.Quit(); // TODO: better error handling.
        }
    }

    private void Start()
    {

    }

    // TODO: trigger this from UI
    private void OnLoadDefinitions()
    {
        var definitions = new DefinitionsLoader().LoadAllDefinitionsAsync().GetAwaiter().GetResult();
        //var (definition, count) = (definitions[0], 10); ; // TODO : ability to choose which.
        //var (definition, count) = (definitions[1], 2); // TODO : ability to choose which.

        //FOR DEBUG: generated definition
        var (definition, count) = new UniverseGenerator().Generate();

        CurrentGameInstance = GameInstance.Create(definition);
        onGameInstanceCreated?.Invoke(CurrentGameInstance, count);
    }

    // Update is called once per frame
    void Update()
    {
        // Load once
        float timeSinceGameStartInSeconds = Time.time;
        if (timeSinceGameStartInSeconds > 2.0f && CurrentGameInstance == null)
        { 
            OnLoadDefinitions();
        }
    }
}
