using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class OrbitModifier
{
    public abstract float XOffset(long timeMs);
    public abstract float YOffset(long timeMS);
    public abstract float ZOffset(long timeMS);
}
