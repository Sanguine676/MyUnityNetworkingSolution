using UnityEngine;
//

//
public static class Float_ExtentionMethods
{
    //=================================================================================================
    #region Validation Check Methods
    /// <summary> Uses float.IsNegativeInfinity. [it felt like float really needed it] </summary>
    public static bool IsNegativeInfinity(this float _fVal)
    {
        return float.IsNegativeInfinity(_fVal);
    }

    /// <summary> Uses float.IsPositiveInfinity. [it felt like float really needed it]? </summary>
    public static bool IsPositiveInfinity(this float _fVal)
    {
        return float.IsPositiveInfinity(_fVal);
    }

    /// <summary> Is _fVal either float.PositiveInfinity or float.NegativeInfinity? </summary>
    public static bool IsInfinity(this float _fVal)
    {
        return float.IsInfinity(_fVal);
    }

    /// <summary> Is _fVal a valid value? [neither float.NaN, nor [float.NegativeInfinity or float.PositiveInfinity]] </summary>
    public static bool IsValid(this float _fVal)
    {
        return MathUtility.IsValid(_fVal);
    }
    #endregion
    //=================================================================================================

    //=================================================================================================
    #region Approximation Check Methods
    /// <summary> Mathf.Epsilon is used for the check. </summary>
    public static bool Approximate(this float _fVal, float _target)
    {
        return MathUtility.Approximate(_fVal, _target);
    }

    /// <summary> MathUtility.SqrEpsilon is used for the check. </summary>
    public static bool PreciseApproximate(this float _fVal, float _target)
    {
        return MathUtility.PreciseApproximate(_fVal, _target);
    }

    /// <summary> MathUtility.AlternateEpsilon is used for the check. </summary>
    public static bool AlternateApproximate(this float _fVal, float _target)
    {
        return MathUtility.AlternateApproximate(_fVal, _target);
    }

    /// <summary> MathUtility.AlternateSqrEpsilon is used for the check. </summary>
    public static bool PreciseAlternateApproximate(this float _fVal, float _target)
    {
        return MathUtility.PreciseAlternateApproximate(_fVal, _target);
    }
    #endregion
    //=================================================================================================

    //=================================================================================================
    #region Interpolation Methods
    //
    public static float ToVal(this float _fVal, float _target, float _step)
    {
        return Mathf.MoveTowards(_fVal, _target, _step);
    }

    /// <summary> Doesn't matter if used in Update or FixedUpdate, Time.deltaTime in FixedUpdate is Time.fixedDeltaTime </summary>
    public static float ToValGradually(this float _fVal, float _target)
    {
        return _fVal.ToVal(_target, TimeInfo.DeltaTime);
    }

    //
    public static float ToZero(this float _fVal, float _step)
    {
        return _fVal.ToVal(0.0f, _step);
    }

    /// <summary> Doesn't matter if used in Update or FixedUpdate, Time.deltaTime in FixedUpdate is Time.fixedDeltaTime </summary>
    public static float ToZeroGradually(this float _fVal)
    {
        return _fVal.ToZero(TimeInfo.DeltaTime);
    }

    //
    public static float ToAngle(this float _fVal, float _targetAngle, float _degreeStep)
    {
        return Mathf.MoveTowardsAngle(_fVal, _targetAngle, _degreeStep);
    }

    //
    public static float Lerp(this float _fVal, float _target, float _t)
    {
        if (_fVal.Approximate(_target)) return _target;
        return Mathf.Lerp(_fVal, _target, _t);
    }

    //
    public static float LerpUnclamped(this float _fVal, float _target, float _t)
    {
        if (_fVal.Approximate(_target)) return _target;
        return Mathf.LerpUnclamped(_fVal, _target, _t);
    }

    //
    public static float SmoothStep(this float _fVal, float _target, float _t)
    {
        if (_fVal.Approximate(_target)) return _target;
        return Mathf.SmoothStep(_fVal, _target, _t);
    }

    //
    public static float SmoothLerp(this float _fVal, float _target, float _t)
    {
        if (_fVal.Approximate(_target)) return _target;
        return MathUtility.SmoothLerp(_fVal, _target, _t);
    }

    //
    public static float LerpAngle(this float _fVal, float _target, float _t)
    {
        if (_fVal.PreciseApproximate(_target)) return _target;//E^2 for angle to avoid jump exposure
        return Mathf.LerpAngle(_fVal, _target, _t);
    }
    #endregion
    //=================================================================================================
}
