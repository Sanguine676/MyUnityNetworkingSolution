using UnityEngine;
//

/// <summary> A mathematics utility class containing helper fields, properties, and methods mixed up with the Mathf struct. </summary>
public static class MathUtility
{
    //=================================================================================================
    #region Variables
    //
    private const float p_alternateEpsilon = 0.0001f;
    private const float p_sqrAlternateEpsilon = p_alternateEpsilon * p_alternateEpsilon;

    private const float p_alternateEpsilon2 = 0.00001f;
    private const float p_sqrAlternateEpsilon2 = p_alternateEpsilon2 * p_alternateEpsilon2;

    private const float p_sqrFloatEpsilon = float.Epsilon * float.Epsilon;

    private const float p_sqrtOf2 = 1.414214f,
        p_sqrtOf3 = 1.732050f,
        p_sqrtOf5 = 2.236067f,
        p_sqrtOf6 = 2.449489f,
        p_sqrtOf7 = 2.645751f,
        p_sqrtOf10 = 3.162277f,
        p_sqrtOf11 = 3.316624f,
        p_sqrtOf13 = 3.605551f,
        p_sqrtOf14 = 3.741657f,
        p_sqrtOf15 = 3.872983f,
        p_sqrtOf17 = 4.123105f,
        p_sqrtOf19 = 4.358898f;

    private const float p_eulerValue = 2.718281f;
    #endregion
    //=================================================================================================

    //=================================================================================================
    #region References
    #endregion
    //=================================================================================================

    //=================================================================================================
    #region Properties
    /// <summary> Short form for Mathf.Epsilon * Mathf.Epsilon. </summary>
    public static float SqrEpsilon
    {
        get
        {
            float _epsilon = Mathf.Epsilon;
            return _epsilon * _epsilon;
        }
    }

    /// <summary> A specific tiny floating point value. [by default : 0.0001f] </summary>
    public static float AlternateEpsilon => p_alternateEpsilon;

    /// <summary> Short form for MathUtility.AlternateEpsilon * MathUtility.AlternateEpsilon. </summary>
    public static float SqrAlternateEpsilon => p_sqrAlternateEpsilon;

    /// <summary> A specific tiny floating point value. [by default : 0.00001f] </summary>
    public static float AlternateEpsilon2 => p_alternateEpsilon2;

    /// <summary> Short form for MathUtility.AlternateEpsilon2 * MathUtility.AlternateEpsilon2. </summary>
    public static float SqrAlternateEpsilon2 => p_sqrAlternateEpsilon2;

    /// <summary> Short form for float.Epsilon * float.Epsilon. </summary>
    public static float SqrFloatEpsilon => p_sqrFloatEpsilon;

    /// <summary> Square root of 2. </summary>
    public static float SqrtOf2 => p_sqrtOf2;

    /// <summary> Square root of 3. </summary>
    public static float SqrtOf3 => p_sqrtOf3;

    /// <summary> Square root of 5. </summary>
    public static float SqrtOf5 => p_sqrtOf5;

    /// <summary> Square root of 6. </summary>
    public static float SqrtOf6 => p_sqrtOf6;

    /// <summary> Square root of 7. </summary>
    public static float SqrtOf7 => p_sqrtOf7;

    /// <summary> Square root of 10. </summary>
    public static float SqrtOf10 => p_sqrtOf10;

    /// <summary> Square root of 11. </summary>
    public static float SqrtOf11 => p_sqrtOf11;

    /// <summary> Square root of 13. </summary>
    public static float SqrtOf13 => p_sqrtOf13;

    /// <summary> Square root of 14. </summary>
    public static float SqrtOf14 => p_sqrtOf14;

    /// <summary> Square root of 15. </summary>
    public static float SqrtOf15 => p_sqrtOf15;

    /// <summary> Square root of 17. </summary>
    public static float SqrtOf17 => p_sqrtOf17;

    /// <summary> Square root of 19. </summary>
    public static float SqrtOf19 => p_sqrtOf19;

    /// <summary> The famous 2.71... euler value. </summary>
    public static float EulerValue => p_eulerValue;
    #endregion
    //=================================================================================================

    //=================================================================================================
    #region Validation Check Methods
    /// <summary> Is _fVal a valid value? [neither float.NaN, nor [float.NegativeInfinity or float.PositiveInfinity]] </summary>
    public static bool IsValid(float _fVal)
        => !float.IsNaN(_fVal) && !float.IsInfinity(_fVal);

    /// <summary> Is _fVal either epsilon or zero? [the alternate epsilon is MathUtility.AlternateEpsilon, otherwise Mathf.Epsilon will be used] </summary>
    public static bool IsApproximatelyZero(float _fVal, bool _alternateEpsilon = false)
        => Mathf.Abs(_fVal) <= (_alternateEpsilon ? AlternateEpsilon : Mathf.Epsilon);

    //
    public static bool IsApproximatelyPreciselyZero(float _fVal, bool _alternateEpsilon = false)
        => Mathf.Abs(_fVal) <= (_alternateEpsilon ? SqrAlternateEpsilon : SqrEpsilon);

    /// <summary> Is _fVal an epsilon value? [the alternate epsilon is MathUtility.AlternateEpsilon, otherwise Mathf.Epsilon will be used. Also 0.0f isn't epsilon.] </summary>
    public static bool IsEpsilon(float _fVal, bool _alternateEpsilon = false)
        => _fVal != 0.0f && IsApproximatelyZero(_fVal, _alternateEpsilon);

    /// <summary> Is _fVal a value with no floating-point digit? [the approximation refers to whether epsilon digits are ignored or not] </summary>
    public static bool IsExact(float _fVal, bool _approximately = false, bool _alternateEpsilon = false)
        => _approximately ? IsApproximatelyZero(_fVal - Mathf.Floor(_fVal), _alternateEpsilon) : _fVal == Mathf.Floor(_fVal);

    /// <summary> Does _fVal contain epsilon in its floating-point digits? </summary>
    public static bool ContainsEpsilon(float _fVal, bool _alternateEpsilon = false)
        => IsEpsilon(_fVal - Mathf.Floor(_fVal), _alternateEpsilon);
    #endregion
    //=================================================================================================

    //=================================================================================================
    #region Approximation Check Methods
    /// <summary> Mathf.Epsilon is used for the check. </summary>
    public static bool Approximate(float _fVal1, float _fVal2)
        => Mathf.Abs(_fVal2 - _fVal1) <= Mathf.Epsilon;

    /// <summary> MathUtility.SqrEpsilon is used for the check. </summary>
    public static bool PreciseApproximate(float _fVal1, float _fVal2)
        => Mathf.Abs(_fVal2 - _fVal1) <= SqrEpsilon;

    /// <summary> MathUtility.AlternateEpsilon is used for the check. </summary>
    public static bool AlternateApproximate(float _fVal1, float _fVal2)
        => Mathf.Abs(_fVal2 - _fVal1) <= AlternateEpsilon;

    /// <summary> MathUtility.SqrAlternateEpsilon is used for the check. </summary>
    public static bool PreciseAlternateApproximate(float _fVal1, float _fVal2)
        => Mathf.Abs(_fVal2 - _fVal1) <= SqrAlternateEpsilon;

    /// <summary> float.Epsilon is used for the check. </summary>
    public static bool FloatApproximate(float _fVal1, float _fVal2)
        => Mathf.Abs(_fVal2 - _fVal1) <= float.Epsilon;

    /// <summary> MathUtility.SqrFloatEpsilon is used for the check. </summary>
    public static bool PreciseFloatApproximate(float _fVal1, float _fVal2)
        => Mathf.Abs(_fVal2 - _fVal1) <= SqrFloatEpsilon;

    /// <summary> A custom approximation check including different approximation methods depending on the parameters. </summary>
    public static bool CustomApproximation(float _fVal1, float _fVal2, bool _alternateApproximation, bool _preciseApproximation)
    {
        bool _approximatelyMatches = _alternateApproximation ?
            _preciseApproximation ? PreciseAlternateApproximate(_fVal1, _fVal2) : AlternateApproximate(_fVal1, _fVal2) :
            _preciseApproximation ? PreciseApproximate(_fVal1, _fVal2) : Approximate(_fVal1, _fVal2);

        return _approximatelyMatches;
    }
    #endregion
    //=================================================================================================

    //=================================================================================================
    #region Interpolation Methods
    /// <summary> Moves _fVal, _step value towards _target. </summary>
    public static void ToVal(ref float _fVal, float _target, float _step)
        => _fVal = Mathf.MoveTowards(_fVal, _target, _step);

    //
    public static void ToValBySpeed(ref float _fVal, float _target, float _speed)
        => ToVal(ref _fVal, _target, TimeInfo.DeltaTime * _speed);

    /// <summary> Moves _fVal, delta time value towards _target. [Time.deltaTime in FixedUpdate is considered Time.fixedDeltaTime] </summary>
    public static void ToValGradually(ref float _fVal, float _target)
        => ToValBySpeed(ref _fVal, _target, 1.0f);

    /// <summary> Moves _fVal, _step value towards zero. </summary>
    public static void ToZero(ref float _fVal, float _step)
        => ToVal(ref _fVal, 0.0f, _step);

    //
    public static void ToZeroBySpeed(ref float _fVal, float _speed)
        => ToZero(ref _fVal, TimeInfo.DeltaTime * _speed);

    /// <summary> Moves _fVal, delta time value towards zero. [Time.deltaTime in FixedUpdate is considered Time.fixedDeltaTime] </summary>
    public static void ToZeroGradually(ref float _fVal)
        => ToZeroBySpeed(ref _fVal, 1.0f);

    /// <summary> Moves _fVal, _degreeStep value towards _targetAngle while making sure it wraps around 360 degrees correctly. </summary>
    public static void ToAngle(ref float _fVal, float _targetAngle, float _degreeStep)
        => _fVal = Mathf.MoveTowardsAngle(_fVal, _targetAngle, _degreeStep);

    /// <summary> Moves _fVal, (_target - _fVal) * _t value towards _target with _t clamped between 0 and 1. [has approximation check] </summary>
    public static void Lerp(ref float _fVal, float _target, float _t)
        => _fVal = Approximate(_fVal, _target) ? _target : Mathf.Lerp(_fVal, _target, _t);

    /// <summary> Moves _fVal, (_target - _fVal) * _t value towards _target with _t unclamped. [has approximation check, experimental] </summary>
    public static void LerpUnclamped(ref float _fVal, float _target, float _t)
        => _fVal = Approximate(_fVal, _target) ? _target : Mathf.LerpUnclamped(_fVal, _target, _t);

    /// <summary> Moves _fVal, (_targetAngle - _fVal) * _t value towards _targetAngle with _t clamped between 0 and 1 while making sure it wraps around 360 degrees.
    /// [has approximation check] </summary>
    public static void LerpAngle(ref float _fVal, float _targetAngle, float _t)
        => _fVal = PreciseApproximate(_fVal, _targetAngle) ? _targetAngle : Mathf.LerpAngle(_fVal, _targetAngle, _t);

    /// <summary> Returns an extremely smoothed lerped value between _startVal and _endVal by combining Mathf.Lerp and Mathf.SmoothStep. </summary>
    public static float SmoothLerp(float _startVal, float _endVal, float _t)
        => Mathf.Lerp(_startVal, _endVal, Mathf.SmoothStep(0.0f, 1.0f, _t));

    /// <summary> Returns an extremely smoothed lerped value between _startAngle and _endAngle by combining Mathf.LerpAngle and MathUtility.SmoothLerp
    /// while making sure it wraps around 360 degrees correctly. </summary>
    public static float SmoothLerpAngle(float _startAngle, float _endAngle, float _t)
        => Mathf.LerpAngle(_startAngle, _endAngle, SmoothLerp(0.0f, 1.0f, _t));

    /// <summary> Returns a circularly lerped value between _startVal and _endVal by the formula:
    /// y = ((max - min) * sin(angle * compressor) + min + max) * 0.5f </summary>
    public static float CircularLerp(float _startVal, float _endVal, float _t, float _horizontalCompression = 1.0f)
    {
        const float _90_Rad = Mathf.PI * 0.5f;
        float _radAngle = Mathf.Lerp(-_90_Rad, _90_Rad, _t);
        float _scaledRadAngle = _radAngle * _horizontalCompression;
        float _lerpedValue = ((_endVal - _startVal) * Mathf.Sin(_scaledRadAngle) + _startVal + _endVal) * 0.5f;

        return _lerpedValue;
    }

    /// <summary> Returns a circularly smoothed lerped value between _startVal and _endVal with the help of MathUtility.SmoothLerp by the formula:
    /// y = ((max - min) * sin(angle * compressor) + min + max) * 0.5f </summary>
    public static float CircularSmoothLerp(float _startVal, float _endVal, float _t, float _horizontalCompression = 1.0f)
        => CircularLerp(_startVal, _endVal, SmoothLerp(0.0f, 1.0f, _t), _horizontalCompression);

    /// <summary> Like MathUtility.CircularLerp but makes sure it wraps around 360 degrees correctly. </summary>
    public static float CircularLerpAngle(float _startAngle, float _endAngle, float _t, float _horizontalCompression = 1.0f)
        => Mathf.LerpAngle(_startAngle, _endAngle, CircularLerp(0.0f, 1.0f, _t, _horizontalCompression));

    /// <summary> Like MathUtility.CircularSmoothLerp but makes sure it wraps around 360 degrees correctly. </summary>
    public static float CircularSmoothLerpAngle(float _startAngle, float _endAngle, float _t, float _horizontalCompression = 1.0f)
        => Mathf.LerpAngle(_startAngle, _endAngle, CircularSmoothLerp(0.0f, 1.0f, _t, _horizontalCompression));
    #endregion
    //=================================================================================================

    //=================================================================================================
    #region Ping Pong Methods
    #region Linear Ping Pong Methods
    /// <summary> Like Mathf.PingPong but more advanced and customized. </summary>
    public static float PingPongCustom(float _timer, float _minVal, float _maxVal, float _speed)
    {
        float _scaledTimer = _timer * _speed;
        float _delta = _maxVal - _minVal;
        return Mathf.PingPong(_scaledTimer, _delta) + _minVal;
    }

    /// <summary> Like MathUtility.PingPongCustom but the minimum value is 0.0f. </summary>
    public static float PingPongCustom(float _timer, float _maxVal, float _speed)
        => PingPongCustom(_timer, 0.0f, _maxVal, _speed);

    /// <summary> Like MathUtility.PingPongCustom but uses TimeInfo.GlobalTime as the timer. </summary>
    public static float PingPong(float _minVal, float _maxVal, float _speed)
    {
        float _timer = TimeInfo.GlobalTime;
        return PingPongCustom(_timer, _minVal, _maxVal, _speed);
    }

    /// <summary> Like MathUtility.PingPongCustom but uses TimeInfo.GlobalTime as the timer and the minimum value is 0.0f. </summary>
    public static float PingPong(float _maxVal, float _speed)
    {
        float _timer = TimeInfo.GlobalTime;
        return PingPongCustom(_timer, _maxVal, _speed);
    }

    /// <summary> Like MathUtility.PingPongCustom but with smoothness applied with the help of MathUtility.SmoothLerp. </summary>
    public static float PingPongSmoothLerpCustom(float _timer, float _minVal, float _maxVal, float _speed)
    {
        float _pingPongedRelation = PingPongCustom(_timer, 1.0f, _speed);
        float _smoothLerpedValue = SmoothLerp(_minVal, _maxVal, _pingPongedRelation);
        return _smoothLerpedValue;
    }

    /// <summary> Like MathUtility.PingPongSmoothLerpCustom but the minimum value is 0.0f. </summary>
    public static float PingPongSmoothLerpCustom(float _timer, float _maxVal, float _speed)
        => PingPongSmoothLerpCustom(_timer, 0.0f, _maxVal, _speed);

    /// <summary> Like MathUtility.PingPongSmoothLerpCustom but uses TimeInfo.GlobalTime as the timer. </summary>
    public static float PingPongSmoothLerp(float _minVal, float _maxVal, float _speed)
    {
        float _timer = TimeInfo.GlobalTime;
        return PingPongSmoothLerpCustom(_timer, _minVal, _maxVal, _speed);
    }

    /// <summary> Like MathUtility.PingPongSmoothLerpCustom but uses TimeInfo.GlobalTime as the timer and the minimum value is 0.0f. </summary>
    public static float PingPongSmoothLerp(float _maxVal, float _speed)
    {
        float _timer = TimeInfo.GlobalTime;
        return PingPongSmoothLerpCustom(_timer, _maxVal, _speed);
    }
    #endregion

    #region Circular Ping Pong Methods
    /// <summary> Like Mathf.PingPong but the interpolation is circular, more advanced, and customized.
    /// [y = ((max - min) * sin(angle * compressor) + min + max) * 0.5f] </summary>
    public static float PingPongSinusCustom(float _radianTimer, float _minVal, float _maxVal, float _speed, float _horizontalCompression = 1.0f)
    {
        float _scaledRadianTimer = _radianTimer * _speed;
        float _compressedScaledRadianTimer = _scaledRadianTimer * _horizontalCompression;
        float _pingPongedValue = ((_maxVal - _minVal) * Mathf.Sin(_compressedScaledRadianTimer) + _minVal + _maxVal) * 0.5f;

        return _pingPongedValue;
    }

    /// <summary> Like MathUtility.PingPongSinusCustom but the minimum value is 0.0f. </summary>
    public static float PingPongSinusCustom(float _radianTimer, float _maxVal, float _speed, float _horizontalCompression = 1.0f)
        => PingPongSinusCustom(_radianTimer, 0.0f, _maxVal, _speed, _horizontalCompression);

    /// <summary> Like MathUtility.PingPongSinusCustom but uses TimeInfo.GlobalTime as the timer. </summary>
    public static float PingPongSinus(float _minVal, float _maxVal, float _speed, float _horizontalCompression = 1.0f)
    {
        float _radianTimer = TimeInfo.GlobalTime;
        return PingPongSinusCustom(_radianTimer, _minVal, _maxVal, _speed, _horizontalCompression);
    }

    /// <summary> Like MathUtility.PingPongSinusCustom but uses TimeInfo.GlobalTime as the timer and the minimum value is 0.0f. </summary>
    public static float PingPongSinus(float _maxVal, float _speed, float _horizontalCompression = 1.0f)
    {
        float _radianTimer = TimeInfo.GlobalTime;
        return PingPongSinusCustom(_radianTimer, _maxVal, _speed, _horizontalCompression);
    }

    /// <summary> Like MathUtility.PingPongSinusCustom but with smoothness applied with the help of MathUtility.SmoothLerp. </summary>
    public static float PingPongSinusSmoothLerpCustom(float _radianTimer, float _minVal, float _maxVal, float _speed, float _horizontalCompression = 1.0f)
    {
        float _pingPongedRelation = PingPongSinusCustom(_radianTimer, 1.0f, _speed, _horizontalCompression);
        float _smoothLerpedValue = SmoothLerp(_minVal, _maxVal, _pingPongedRelation);
        return _smoothLerpedValue;
    }

    /// <summary> Like MathUtility.PingPongSinusSmoothLerpCustom but the minimum value is 0.0f. </summary>
    public static float PingPongSinusSmoothLerpCustom(float _radianTimer, float _maxVal, float _speed, float _horizontalCompression = 1.0f)
        => PingPongSinusSmoothLerpCustom(_radianTimer, 0.0f, _maxVal, _speed, _horizontalCompression);

    /// <summary> Like MathUtility.PingPongSinusSmoothLerpCustom but uses TimeInfo.GlobalTime as the timer. </summary>
    public static float PingPongSinusSmoothLerp(float _minVal, float _maxVal, float _speed, float _horizontalCompression = 1.0f)
    {
        float _radianTimer = TimeInfo.GlobalTime;
        return PingPongSinusSmoothLerpCustom(_radianTimer, _minVal, _maxVal, _speed, _horizontalCompression);
    }

    /// <summary> Like MathUtility.PingPongSinusSmoothLerpCustom but uses TimeInfo.GlobalTime as the timer and the minimum value is 0.0f. </summary>
    public static float PingPongSinusSmoothLerp(float _maxVal, float _speed, float _horizontalCompression = 1.0f)
    {
        float _radianTimer = TimeInfo.GlobalTime;
        return PingPongSinusSmoothLerpCustom(_radianTimer, _maxVal, _speed, _horizontalCompression);
    }
    #endregion
    #endregion
    //=================================================================================================

    //=================================================================================================
    #region Sign Check Methods
    /// <summary> Like Mathf.Sign but returns 0 if _fVal is 0, its also sbyte. </summary>
    public static sbyte Sign(float _fVal)
        => _fVal > 0.0f ? (sbyte)1 : _fVal < 0.0f ? (sbyte)(-1) : (sbyte)0;

    /// <summary> Like MathUtility.Sign but it returns float. [avoiding manual casting] </summary>
    public static float Signf(float _fVal)
        => (float)Sign(_fVal);
    #endregion
    //=================================================================================================

    //=================================================================================================
    #region Additional Methods
    /// <summary> Like Mathf.Sqrt but first checks if the parameter value has its square root already cached. </summary>
    public static float SqrtOf(float _fVal)
        => _fVal == 2 ? SqrtOf2 :
            _fVal == 3 ? SqrtOf3 :
            _fVal == 5 ? SqrtOf5 :
            _fVal == 6 ? SqrtOf6 :
            _fVal == 7 ? SqrtOf7 :
            _fVal == 10 ? SqrtOf10 :
            _fVal == 11 ? SqrtOf11 :
            _fVal == 13 ? SqrtOf13 :
            _fVal == 14 ? SqrtOf14 :
            _fVal == 15 ? SqrtOf15 :
            _fVal == 17 ? SqrtOf17 :
            _fVal == 19 ? SqrtOf19 :
            Mathf.Sqrt(_fVal);

    /// <summary> Is the _fVal a squared value? </summary>
    public static bool IsSquared(float _fVal)
        => IsExact(SqrtOf(_fVal), false , false);

    /// <summary> Is _fVal a primary number? </summary>
    public static bool IsPrimary(float _fVal)
    {
        if (_fVal <= 1.0f || !IsExact(_fVal, false, false))
            return false;

        for (int i = 2; i < _fVal; ++i)
            if (_fVal % (float)i == 0.0f) return false;

        return true;
    }
    #endregion
    //=================================================================================================
}
