#nullable enable

using System.Collections.Generic;
using UnityEngine;

public class OrbitNode
{
    public static OrbitNode UniverseRootNode { get; } = new OrbitNode("UniverseCenter");

    public OrbitNode? Parent { get; protected set; }
    public string Name { get; }
    public List<OrbitNode> Children { get; } = new();
    public List<OrbitModifier> Modifiers { get; } = new();

    // TODO: Cache X, Y, Z until t changes
    public float X(float t) {
        float x = 0.0f;

        if (Parent != null)
        {
            x += Parent.X(t);
        }
        
        foreach (var modifier in Modifiers)
        {
            x += modifier.XOffset(t);
        }
        return x;
    }

    public float Y(float t) {
        float y = 0.0f;

        if (Parent != null)
        {
            y += Parent.Y(t);
        }

        foreach (var modifier in Modifiers)
        {
            y += modifier.YOffset(t);
        }
        return y;
    }

    public float Z(float t) {
        float z = 0.0f;

        if (Parent != null)
        {
            z += Parent.Z(t);
        }
        
        foreach (var modifier in Modifiers)
        {
            z += modifier.ZOffset(t);
        }
        return z;
    }


    public OrbitNode(OrbitNode parent, string name)
    {
        Parent = parent;
        Name = name;
    }

    // Only for the Universe
    private OrbitNode(string name)
    {
        Parent = null;
        Name = name;
    }

}

