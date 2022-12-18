using UnityEngine;
//

//
public static class Color_ExtentionMethods
{
    //
    public static Color Lerp(this Color color, Color target, float t)
    {
        if (ColorUtility.Approximate(color, target))
            return target;

        return Color.Lerp(color, target, t);
    }

    //
    public static Color Slerp(this Color color, Color target, float t)
    {
        if (ColorUtility.Approximate(color, target))
            return target;

        return ColorUtility.Slerp(color, target, t);
    }

    //
    public static Color SmoothLerp(this Color color, Color target, float t)
    {
        return color.Lerp(target, MathUtility.SmoothLerp(0.0f, 1.0f, t));
    }

    //
    public static Color SmoothSlerp(this Color color, Color target, float t)
    {
        return color.Slerp(target, MathUtility.SmoothLerp(0.0f, 1.0f, t));
    }

    //
    public static Color MoveTowards(this Color color, Color target, float step)
    {
        return Vector4.MoveTowards(color, target, step);
    }

    //
    public static Color ChangeAlpha(this Color color, float newAlpha)
    {
        Color col = color;
        col.a = newAlpha;

        return col;
    }

    //
    public static Color LerpAlpha(this Color color, float targetAlpha, float t)
    {
        float newAlpha = color.a.Lerp(targetAlpha, t);

        return color.ChangeAlpha(newAlpha);
    }

    //
    public static Color LerpRGB(this Color color, Color target, float t)
    {
        Color col = color;
        Vector3 vec3CurColor = new Vector3(col.r, col.g, col.b);
        Vector3 vec3TargetColor = new Vector3(target.r, target.g, target.b);

        Vector3 vec3LerpedColor = vec3CurColor.Lerp(vec3TargetColor, t);

        return new Color(vec3LerpedColor.x, vec3LerpedColor.y, vec3LerpedColor.z, col.a);
    }

    //
    public static Color SlerpRGB(this Color color, Color target, float t)
    {
        Color col = color;
        Vector3 vec3CurColor = new Vector3(col.r, col.g, col.b);
        Vector3 vec3TargetColor = new Vector3(target.r, target.g, target.b);

        Vector3 vec3SlerpedColor = vec3CurColor.Slerp(vec3TargetColor, t);

        return new Color(vec3SlerpedColor.x, vec3SlerpedColor.y, vec3SlerpedColor.z, col.a);
    }

    //
    public static Color Color3(this Color color)
    {
        return color.ChangeAlpha(1.0f);
    }
}
