using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class OrbitModifier
{
    public abstract float XOffset(float t);
    public abstract float YOffset(float t);
    public abstract float ZOffset(float t);
}
