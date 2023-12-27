using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// The 'sin' function along the Y axis
/// </summary>
public class SinOrbitModifier_Y : OrbitModifier
{
    // TODO: Start time offset


    public override float XOffset(float t) => 0;
    public override float YOffset(float t) => Mathf.Sin(t);
    public override float ZOffset(float t) => 0;


    public SinOrbitModifier_Y() : base()
    {
    }
}

