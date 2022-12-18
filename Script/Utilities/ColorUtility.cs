using UnityEngine;
//

//
public static class ColorUtility
{
    //
    public static float SqrDistance(Color color1, Color color2)
    {
        return ((Vector4)(color2 - color1)).sqrMagnitude;
    }

    //
    public static float Distance(Color color1, Color color2)
    {
        return Mathf.Sqrt(SqrDistance(color1, color2));
    }

    //
    public static bool Approximate(Color color1, Color color2)
    {
        return SqrDistance(color1, color2) < MathUtility.SqrEpsilon;
    }

    //
    public static bool PreciseApproximate(Color color1, Color color2)
    {
        float sqrEpsilon = MathUtility.SqrEpsilon, sqrSqrEpsilon = sqrEpsilon * sqrEpsilon;

        return SqrDistance(color1, color2) < sqrSqrEpsilon;
    }

    //
    public static bool AlternateApproximate(Color color1, Color color2)
    {
        return SqrDistance(color1, color2) < MathUtility.SqrAlternateEpsilon;
    }

    //
    public static bool PreciseAlternateApproximate(Color color1, Color color2)
    {
        float sqrEpsilon = MathUtility.SqrAlternateEpsilon, sqrSqrEpsilon = sqrEpsilon * sqrEpsilon;

        return SqrDistance(color1, color2) < sqrSqrEpsilon;
    }

    //
    public static Color Slerp(Color startColor, Color endColor, float t)
    {
        Vector3 vec3StartColor = new Vector3(startColor.r, startColor.g, startColor.b);
        Vector3 vec3TargetColor = new Vector3(endColor.r, endColor.g, endColor.b);

        Vector3 vec3SlerpedColor = Vector3.Slerp(vec3StartColor, vec3TargetColor, t);
        Color slerpedColor = new Color(vec3SlerpedColor.x, vec3SlerpedColor.y, vec3SlerpedColor.z, Mathf.SmoothStep(startColor.a, endColor.a, t));

        return slerpedColor;
    }

    //
    public static Color SmoothLerp(Color startColor, Color endColor, float t)
    {
        return Color.Lerp(startColor, endColor, MathUtility.SmoothLerp(0.0f, 1.0f, t));
    }

    //
    public static Color SmoothSlerp(Color startColor, Color endColor, float t)
    {
        return Slerp(startColor, endColor, MathUtility.SmoothLerp(0.0f, 1.0f, t));
    }

    //
    public static Color MoveTowards(Color startColor, Color targetColor, float step)
    {
        return Vector4.MoveTowards(startColor, targetColor, step);
    }

    //
    public static void ChangeAlpha(ref Color color, float newAlpha)
    {
        Color col = color;
        col.a = newAlpha;

        color = col;
    }

    //
    public static Color Color3(Color color)
    {
        color.a = 1.0f;

        return color;
    }

    //
    public static void Color3(ref Color color)
    {
        ChangeAlpha(ref color, 1.0f);
    }

    //
    public static Color PingPongLerp(Color color1, Color color2, float speed)
    {
        float _scaledTimer = TimeInfo.GlobalTime * speed;
        return Color.Lerp(color1, color2, Mathf.PingPong(_scaledTimer, 1.0f));
    }

    //
    public static Color PingPongSlerp(Color color1, Color color2, float speed)
    {
        float _scaledTimer = TimeInfo.GlobalTime * speed;
        return Slerp(color1, color2, Mathf.PingPong(_scaledTimer, 1.0f));
    }

    //
    public static Color PingPongSmoothLerp(Color color1, Color color2, float speed)
    {
        float _scaledTimer = TimeInfo.GlobalTime * speed;
        return SmoothLerp(color1, color2, Mathf.PingPong(_scaledTimer, 1.0f));
    }

    //
    public static Color PingPongSmoothSlerp(Color color1, Color color2, float speed)
    {
        float _scaledTimer = TimeInfo.GlobalTime * speed;
        return SmoothSlerp(color1, color2, Mathf.PingPong(_scaledTimer, 1.0f));
    }
}
