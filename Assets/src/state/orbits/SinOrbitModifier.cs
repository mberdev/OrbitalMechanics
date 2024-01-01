using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// The 'sin' function along the Y axis
/// </summary>
public class SinOrbitModifier_Y : OrbitModifier
{
    // TODO: Start time offset


    public override float XOffset(long timeMs) => 0;
    public override float YOffset(long timeMs) => Mathf.Sin(timeMs);
    public override float ZOffset(long timeMs) => 0;


    public SinOrbitModifier_Y() : base()
    {
    }
}

