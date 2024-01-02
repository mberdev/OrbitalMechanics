

//using UnityEngine;

//public class Ellipse_XZ
//{
//    // 'horizontalAxis' and 'verticalAxis' are the 2 axis of the ellipse
//    // 'center' is the central point around which the ellipse is centered, needs to have x and y
//    // 'duration' is the duration of the whole movement/animation
//    public Ellipse_XZ( float axisX, float axisZ, float duration )
//    {
//        AxisX = axisX;
//        AxisZ = axisZ;
//        Duration = duration;
//    }

//    public float AxisX { get; }
//    public float AxisZ { get; }

//    public float Duration { get; }

//    public float LerpEllipseX(long timeMs) => Ellipse_XZ.LerpEllipseX(timeMs, AxisX, Duration);

//    public float LerpEllipseZ(long timeMs) => Ellipse_XZ.LerpEllipseZ(timeMs, AxisZ, Duration);

//    public static float LerpEllipseX(long timeMs, float axisX, float duration)
//    {
//        return ( axisX * Mathf.Cos( ( timeMs / duration ) * Mathf.PI * 2 ) );
//    }

//    public static float LerpEllipseZ(long timeMs, float axizZ, float duration)
//    {
//        return ( axizZ * Mathf.Sin( ( timeMs / duration ) * Mathf.PI * 2 ) );
//    }

//    //// Lerp Ellipse gives you the x and y position for something moving along an ellipse
//    //// 'time' is the time that has passed since the animation/movement has started
//    //public Vector2 LerpEllipse2( float time )
//    //{
//    //    return new Vector2( LerpEllipseX(time), LerpEllipseZ(time) );
//    //}

//    //public Vector3 LerpEllipse3(float time)
//    //{
//    //    return new Vector3(LerpEllipseX(time), 0, LerpEllipseZ(time));
//    //}
//}
