using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// An ellipsis in the X, Z plane
/// </summary>
public class EllipsisOrbitModifier_XZ : OrbitModifier
{
    // TODO: Start time offset
    public override float XOffset(float t) => Ellipse.LerpEllipseX(t);
    public override float YOffset(float t) => 0.0f;
    public override float ZOffset(float t) => Ellipse.LerpEllipseZ(t);

    public Ellipse_XZ Ellipse { get; }

    public EllipsisOrbitModifier_XZ(Ellipse_XZ ellipse) : base()
    {
        Ellipse = ellipse;
    }
}

