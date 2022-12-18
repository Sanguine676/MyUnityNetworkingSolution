using UnityEngine;
//

//
public static class Quaternion_ExtentionMethods
{
    //
    public static Quaternion Lerp(this Quaternion quaternion, Quaternion target, float t)
    {
        if (QuaternionUtility.Approximate(quaternion, target)) return target;
        return Quaternion.Lerp(quaternion, target, t);
    }

    //
    public static Quaternion Slerp(this Quaternion quaternion, Quaternion target, float t)
    {
        if (QuaternionUtility.Approximate(quaternion, target)) return target;
        return Quaternion.Slerp(quaternion, target, t);
    }

    //
    public static Quaternion RotateTowards(this Quaternion quaternion, Quaternion target, float degreeStep)
    {
        return Quaternion.RotateTowards(quaternion, target, degreeStep);
    }

    //
    public static Quaternion RotateTowardsHorizontally(this Quaternion quaternion, Quaternion target, float degreeStep)
    {
        Vector3 curEuler = quaternion.eulerAngles;
        curEuler.y = curEuler.y.ToAngle(target.eulerAngles.y, degreeStep);
        quaternion = Quaternion.Euler(curEuler);

        return quaternion;
    }

    //
    public static Quaternion RotateTowardsVertically(this Quaternion quaternion, Quaternion target, float degreeStep)
    {
        Vector3 curEuler = quaternion.eulerAngles;
        Vector3 targetEuler = target.eulerAngles;
        float step = degreeStep * 0.81f;
        curEuler.x = curEuler.x.ToAngle(targetEuler.x, step);
        curEuler.z = curEuler.z.ToAngle(targetEuler.z, step);
        quaternion = Quaternion.Euler(curEuler);

        return quaternion;
    }

    //
    public static Quaternion GetDelta(this Quaternion quaternion, Quaternion targetRot)
    {
        return Quaternion.FromToRotation(quaternion * Vector3.forward, targetRot * Vector3.forward);
    }

    //
    public static Quaternion GetDelta(this Quaternion quaternion, Vector3 targetForward)
    {
        return Quaternion.FromToRotation(quaternion * Vector3.forward, targetForward);
    }

    //
    public static Quaternion GetSolidDelta(this Quaternion quaternion, Quaternion targetRot)
    {
        return targetRot * Quaternion.Inverse(quaternion);
    }

    //
    public static Quaternion GetSolidDelta(this Quaternion quaternion, Vector3 targetForward)
    {
        return Quaternion.LookRotation(targetForward) * Quaternion.Inverse(quaternion);
    }
}
