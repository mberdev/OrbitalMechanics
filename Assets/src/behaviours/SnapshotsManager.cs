# nullable enable

using UnityEngine;


/// <summary>
/// The intializer is supposed to attach this behaviour to the SnapshotsManager node at startup.
/// </summary>
public class SnapshotsManager : MonoBehaviour
{
    public static string NodeName { get; private set; } = "SnapshotsManager";

    // Container node (empty game object) to store all the snapshots
    public static SnapshotsManager Instance { get; private set; }

    void Start()
    {
        Instance = InitBehaviour.Instance.SnapshotsManager;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Ony creates the subnode. Does not generate the meshes, etc.
    public Snapshot GetOrCreateSnapshotNode(string snapshotHash)
    {
        // Find child of SnapshotsManagerNode with name snapshotHash
        var node = Instance.transform.Find(snapshotHash)?.gameObject;
        if (node == null)
        {
            node = new GameObject(snapshotHash);
            node.transform.SetParent(Instance.transform);
            node.AddComponent<Snapshot>();
            //node.GetComponent<Snapshot>().enabled = true;
        }
        else
        {
            Debug.LogWarning($"Snapshot node {snapshotHash} already exists. Not regenerating.");
        }

        return node.GetComponent<Snapshot>();
    }

}
