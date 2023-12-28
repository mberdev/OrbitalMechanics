using Assets.src.state2;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Attach this script to the "Initializer" game object in the scene.
/// 
/// </summary>
public class InitBehaviour : MonoBehaviour
{
    /// <summary>
    /// Singleton pattern
    /// </summary>
    public static InitBehaviour Instance { get; private set; }

    public GameObject Root { get; private set; }
    public SnapshotsManager SnapshotsManager { get; private set; }

    void Awake()
    {
        Instance = this;

        Root = GameObject.Find("root");
        if (Root == null)
        {
            Debug.LogError("The scene must contain an object named 'root' at top level. Create an empty object in the Editor if needed.");
            Application.Quit();
        }

        CreateSnapshotsManager();
    }
    private void Start()
    {
        
    }

    /// <summary>
    /// Create empty node "SnapshotsManager" at root level
    /// </summary>
    private void CreateSnapshotsManager()
    {
        var node = Root.GetComponentInChildren<SnapshotsManager>()?.gameObject;
        if (node == null)
        {
            node = new GameObject(SnapshotsManager.NodeName);
            node.transform.SetParent(Root.transform);
            node.AddComponent<SnapshotsManager>();
            node.GetComponent<SnapshotsManager>().enabled = true;
        }
        else
        {
            Debug.LogWarning($"Snapshots manager already exists. Not regenerating.");
        }

        SnapshotsManager = node.GetComponent<SnapshotsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
