using System.Collections.Generic;
using UnityEngine;
//

//
public static class Vector3_ExtentionMethods
{
    //
    public static bool Approximate(this Vector3 _vec, Vector3 _target)
    {
        return Vector3Utility.Approximate(_vec, _target);
    }

    //
    public static bool PreciseApproximate(this Vector3 _vec, Vector3 _target)
    {
        return Vector3Utility.PreciseApproximate(_vec, _target);
    }

    //
    public static bool AlternateApproximate(this Vector3 _vec, Vector3 _target)
    {
        return Vector3Utility.AlternateApproximate(_vec, _target);
    }

    //
    public static bool PreciseAlternateApproximate(this Vector3 _vec, Vector3 _target)
    {
        return Vector3Utility.PreciseAlternateApproximate(_vec, _target);
    }

    //
    public static bool AngleApproximate(this Vector3 _vec, Vector3 _target)
    {
        return Vector3Utility.AngleApproximate(_vec, _target);
    }

    //
    public static bool PreciseAngleApproximate(this Vector3 _vec, Vector3 _target)
    {
        return Vector3Utility.PreciseAngleApproximate(_vec, _target);
    }

    //
    public static bool AngleAlternateApproximate(this Vector3 _vec, Vector3 _target)
    {
        return Vector3Utility.AngleAlternateApproximate(_vec, _target);
    }

    //
    public static bool PreciseAngleAlternateApproximate(this Vector3 _vec, Vector3 _target)
    {
        return Vector3Utility.PreciseAngleAlternateApproximate(_vec, _target);
    }

    //
    public static Vector3 HorizontalVec(this Vector3 _vec)
    {
        Vector3 _curVec = _vec;
        _curVec.y = 0.0f;

        return _curVec;
    }

    //
    public static Vector3 CustomLerp(this Vector3 _vec, Vector3 _target, float _t, bool _alternateApproximation, bool _preciseApproximation)
    {
        if (Vector3Utility.CustomApproximate(_vec, _target, _alternateApproximation, _preciseApproximation)) return _target;
        return Vector3.Lerp(_vec, _target, _t);
    }

    //
    public static Vector3 CustomSlerp(this Vector3 _vec, Vector3 _target, float _t, bool _alternateApproximation, bool _preciseApproximation)
    {
        if (Vector3Utility.CustomApproximate(_vec, _target, _alternateApproximation, _preciseApproximation)) return _target;
        return Vector3.Slerp(_vec, _target, _t);
    }

    //
    public static Vector3 Lerp(this Vector3 _vec, Vector3 _target, float _t)
    {
        return _vec.CustomLerp(_target, _t, false, false);
    }

    //
    public static Vector3 Slerp(this Vector3 _vec, Vector3 _target, float _t)
    {
        return _vec.CustomSlerp(_target, _t, false, false);

    }

    //
    public static Vector3 LerpUnclamped(this Vector3 vec, Vector3 target, float t)
    {
        return Vector3.LerpUnclamped(vec, target, t);
    }

    //
    public static Vector3 SlerpUnclamped(this Vector3 vec, Vector3 target, float t)
    {
        return Vector3.SlerpUnclamped(vec, target, t);
    }

    //
    public static Vector3 SmoothLerp(this Vector3 _vec, Vector3 _target, float _t)
    {
        return _vec.Lerp(_target, MathUtility.SmoothLerp(0.0f, 1.0f, _t));
    }

    //
    public static Vector3 SmoothSlerp(this Vector3 _vec, Vector3 _target, float _t)
    {
        return _vec.Slerp(_target, MathUtility.SmoothLerp(0.0f, 1.0f, _t));
    }

    //
    public static Vector3 LerpEuler(this Vector3 _vec, Vector3 _target, float _t)
    {
        Vector3 _curVec = _vec;
        Vector3 _targetVec = _target;

        if (_curVec.PreciseApproximate(_targetVec)) return _targetVec;

        _curVec.x = _curVec.x.LerpAngle(_targetVec.x, _t);
        _curVec.y = _curVec.y.LerpAngle(_targetVec.y, _t);
        _curVec.z = _curVec.z.LerpAngle(_targetVec.z, _t);

        return _curVec;
    }

    //
    public static Vector3 RotateTowards(this Vector3 _vec, Vector3 _target, float _degreeStep, float _maxMagnitudeStep = float.PositiveInfinity)
    {
        return Vector3.RotateTowards(_vec, _target, _degreeStep * Mathf.Deg2Rad, _maxMagnitudeStep);
    }

    //
    public static bool IsNormalized(this Vector3 _vec, float _sqrMag)
    {
        if (Mathf.Abs(_sqrMag - 1.0f) > 0.001f * 0.001f)
            return false;

        return true;
    }

    //
    public static bool IsNormalized(this Vector3 _vec)
    {
        float _sqrMag = _vec.sqrMagnitude;
        return _vec.IsNormalized(_sqrMag);
    }

    //
    public static bool Contains(this Vector3[] _vecArr, Vector3 _target, bool _approximate = false)
    {
        //Approximation check must be done earlier to have lesser iteration time
        if (_approximate)
        {
            for (int i = 0; i < _vecArr.Length; ++i)
                if (_vecArr[i] == _target)
                    return true;
        }
        else
        {
            for (int i = 0; i < _vecArr.Length; ++i)
                if (_vecArr[i].Equals(_target))
                    return true;
        }

        return false;
    }

    //
    public static Vector3 MoveTowards(this Vector3 _vec, Vector3 _target, float _step)
    {
        return Vector3.MoveTowards(_vec, _target, _step);
    }

    //
    public static Vector3[] SimulateCircle(this Vector3 _vec, int _pointCount, int _circleCount, float _minRadius, float _maxRadius, Vector3 _sinAxis, Vector3 _cosAxis)
    {
        return Vector3Utility.SimulateCirclesCustom(_pointCount, _circleCount, _vec, _minRadius, _maxRadius, _cosAxis, _sinAxis);
    }

    //
    public static float Angle(this Vector3 _vec, Vector3 _toVec)
    {
        return Vector3.Angle(_vec, _toVec);
    }

    //
    public static float Angle(this Vector3 _vec, Vector3 _toVec, float _multMagnitude)
    {
        if (_multMagnitude < Mathf.Epsilon)
            return float.NaN;
        return Mathf.Acos(Mathf.Clamp(Vector3.Dot(_vec, _toVec) / _multMagnitude, -1.0f, 1.0f)) * Mathf.Rad2Deg;
    }

    //
    public static float Angle(this Vector3 _vec, float _dot, float _multMagnitude)
    {
        if (_multMagnitude < Mathf.Epsilon)
            return float.NaN;
        return Mathf.Acos(Mathf.Clamp(_dot / _multMagnitude, -1.0f, 1.0f)) * Mathf.Rad2Deg;
    }

    //
    public static Vector3[] ValueForAll(this Vector3[] _vecArr, Vector3 _newVec)
    {
        Vector3[] _newVecArr = new Vector3[_vecArr.Length];
        for (int i = 0; i < _newVecArr.Length; ++i)
            _newVecArr[i] = _newVec;

        return _newVecArr;
    }

    //
    public static void SetValueForAll(this Vector3[] _vecArr, Vector3 _newVec)
    {
        Vector3[] _newVecArr = new Vector3[_vecArr.Length];
        for (int i = 0; i < _newVecArr.Length; ++i)
            _newVecArr[i] = _newVec;

        _vecArr = _newVecArr;
    }

    //
    public static bool IsValid(this Vector3 _vec)
    {
        return !_vec.Equals(Vector3.positiveInfinity) && !_vec.Equals(Vector3.negativeInfinity) && !Vector3Utility.IsNaN(_vec);
    }

    //
    public static Vector3[] ValidOnes(this Vector3[] _vecArr)
    {
        List<Vector3> _vecList = new List<Vector3>();
        for (int i = 0; i < _vecArr.Length; ++i)
            if (_vecArr[i].IsValid())
                _vecList.Add(_vecArr[i]);

        return _vecList.ToArray();
    }

    //
    public static Vector3 Normalized(this Vector3 _vec, float _magnitude)
    {
        if (_magnitude < Mathf.Epsilon)
            return Vector3.zero;

        return _vec / _magnitude;
    }

    //
    public static Vector3 Clamped1(this Vector3 _vec, float _maxMagnitude, float _curMagnitude)
    {
        if (_curMagnitude < Mathf.Epsilon)
            return Vector3.zero;

        float _deltaMagnitude = _maxMagnitude - _curMagnitude;
        if (Mathf.Abs(_deltaMagnitude) < Mathf.Epsilon)
            return _vec;

        Vector3 _normalizedVec = _vec / _curMagnitude;

        return _normalizedVec * _maxMagnitude;
    }

    //
    public static Vector3 Clamped2(this Vector3 _vec, float _maxMagnitude, float _curSqrMagnitude)
    {
        float _sqrEpsilon = Mathf.Epsilon * Mathf.Epsilon;

        if (_curSqrMagnitude < _sqrEpsilon)
            return Vector3.zero;

        float _deltaSqrMagnitude = _maxMagnitude * _maxMagnitude - _curSqrMagnitude;
        if (Mathf.Abs(_deltaSqrMagnitude) < _sqrEpsilon)
            return _vec;

        float _curMagnitude = Mathf.Sqrt(_curSqrMagnitude);
        Vector3 _normalizedVec = _vec / _curMagnitude;

        return _normalizedVec * _maxMagnitude;
    }
}
