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
    public float X(long timeMs) {
        float x = 0.0f;

        if (Parent != null)
        {
            x += Parent.X(timeMs);
        }
        
        foreach (var modifier in Modifiers)
        {
            x += modifier.XOffset(timeMs);
        }
        return x;
    }

    public float Y(long timeMS) {
        float y = 0.0f;

        if (Parent != null)
        {
            y += Parent.Y(timeMS);
        }

        foreach (var modifier in Modifiers)
        {
            y += modifier.YOffset(timeMS);
        }
        return y;
    }

    public float Z(long timeMs) {
        float z = 0.0f;

        if (Parent != null)
        {
            z += Parent.Z(timeMs);
        }
        
        foreach (var modifier in Modifiers)
        {
            z += modifier.ZOffset(timeMs);
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

