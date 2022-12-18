using UnityEngine;
//

//
public static class QuaternionUtility
{
    //
    public static Quaternion AllZero => new Quaternion();

    //
    public static bool ApproximateCustom(Quaternion quaternion1, Quaternion quaternion2, float _approximateDelta)
    {
        Vector3 forward1 = quaternion1 * Vector3.forward, forward2 = quaternion2 * Vector3.forward;//Idk what is wrong with Quaternion.Angle, causes jump
        return (Vector3Utility.Angle(forward1, forward2) < _approximateDelta);//E^2 for angle to avoid jump exposure
    }

    //
    public static bool Approximate(Quaternion quaternion1, Quaternion quaternion2)
    {
        return ApproximateCustom(quaternion1, quaternion2, MathUtility.SqrEpsilon);
    }

    //
    public static bool PreciseApproximate(Quaternion quaternion1, Quaternion quaternion2)
    {
        float _sqrEpsilon = MathUtility.SqrEpsilon, _sqrSqrEpsilon = _sqrEpsilon * _sqrEpsilon;
        return ApproximateCustom(quaternion1, quaternion2, _sqrSqrEpsilon);
    }

    //
    public static bool AlternateApproximate(Quaternion quaternion1, Quaternion quaternion2)
    {
        return ApproximateCustom(quaternion1, quaternion2, MathUtility.SqrAlternateEpsilon);
    }

    //
    public static bool PreciseAlternateApproximate(Quaternion quaternion1, Quaternion quaternion2)
    {
        float _alternateSqrEpsilon = MathUtility.SqrAlternateEpsilon, _sqrAlternateSqrEpsilon = _alternateSqrEpsilon * _alternateSqrEpsilon;
        return ApproximateCustom(quaternion1, quaternion2, _alternateSqrEpsilon);
    }
}
