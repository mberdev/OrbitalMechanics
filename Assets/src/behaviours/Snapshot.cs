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
        // Already generated before
        if (transform.childCount > 0)
        {
            Debug.LogWarning($"Snapshot {SnapshotHash} already generated. Not regenerating.");
            return;
        }

        // Generate meshes
        foreach (var entity in snapshot.Entities)
        {
            var definition = entity.Value.Definition;
            var mesh = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            mesh.transform.SetParent(transform);
            mesh.name = entity.Key;
            var diameter = definition.Diameter;
            if (diameter <= 0.0f)
            {
                Debug.LogError($"Entity {entity.Key} has diameter <= 0.0f");
                diameter = 0.1f;
            }

            mesh.transform.localScale = new Vector3(diameter, diameter, diameter);

            //TODO : temp : 
            var orbitFunction = definition.FixedOrbitFunctions[0];
            var position = new Vector3(orbitFunction.OffsetX, orbitFunction.OffsetY, orbitFunction.OffsetZ);
            mesh.transform.position = position;

            mesh.GetComponent<Renderer>().material.color = Color.red; 

            //TODO: link back to snapshot entity?
        }


    }
}
