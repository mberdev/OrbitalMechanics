

using UnityEngine;

public class Ellipse_XZ
{
    // TODO: Start time offset


    // 'horizontalAxis' and 'verticalAxis' are the 2 axis of the ellipse
    // 'center' is the central point around which the ellipse is centered, needs to have x and y
    // 'duration' is the duration of the whole movement/animation
    public Ellipse_XZ( float horizontalAxis, float verticalAxis, Vector2 center, float duration )
    {
        HorizontalAxis = horizontalAxis;
        VerticalAxis = verticalAxis;
        Center = center;
        Duration = duration;
    }

    public float HorizontalAxis { get; }
    public float VerticalAxis { get; }

    // Center is in the X,Y plane. We change it to X, Z at calculation.
    public Vector2 Center { get; }
    public float Duration { get; }

    public float LerpEllipseX(float time) {
        return Center.x + ( HorizontalAxis * Mathf.Cos( ( time / Duration ) * Mathf.PI * 2 ) );
    }

    public float LerpEllipseZ(float time) {
        return Center.y + ( VerticalAxis * Mathf.Sin( ( time / Duration ) * Mathf.PI * 2 ) );
    }

    // Lerp Ellipse gives you the x and y position for something moving along an ellipse
    // 'time' is the time that has passed since the animation/movement has started
    public Vector2 LerpEllipse( float time )
    {
        return new Vector2( LerpEllipseX(time), LerpEllipseZ(time) );
    }
}
