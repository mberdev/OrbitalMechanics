using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestialBody
{
    public CelestialBody(OrbitNode orbitNode, GameObject mesh)
    {
        OrbitNode = orbitNode;
        Mesh = mesh;
    }

    public OrbitNode OrbitNode { get; }
    public GameObject Mesh { get; }
}
