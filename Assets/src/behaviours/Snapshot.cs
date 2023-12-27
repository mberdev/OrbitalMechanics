using Assets.src.state2;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snapshot : MonoBehaviour
{
    public string SnapshotHash { get; }

    public Snapshot(string snapshotHash)
    {
        SnapshotHash = snapshotHash;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Generate(GlobalSnapshot snapshot)
    {
        //TODO : if already has children , then error

        // TODO : generate the meshes, etc.

    }
}
