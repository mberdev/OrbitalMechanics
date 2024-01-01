using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// An ellipsis in the X, Z plane
/// </summary>
public class EllipsisOrbitModifier_XZ : OrbitModifier
{
    // TODO: Start time offset
    public override float XOffset(long timeMs) => Ellipse_XZ.LerpEllipseX(timeMs);
    public override float YOffset(long timeMS) => 0.0f;
    public override float ZOffset(long timeMS) => Ellipse_XZ.LerpEllipseZ(timeMS);

    public Ellipse_XZ Ellipse_XZ { get; }

    public EllipsisOrbitModifier_XZ(Ellipse_XZ ellipse) : base()
    {
        Ellipse_XZ = ellipse;
    }
}

