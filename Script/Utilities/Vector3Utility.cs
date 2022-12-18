using System;
using UnityEngine;
//

/// <summary> A 3D vector utility class containing helper fields, properties, and methods mixed up with other utility classes. </summary>
public static class Vector3Utility
{
    //=================================================================================================
    #region Variables
    /// <summary> Contains the vertices of a default sphere mesh. </summary>
    private static Vector3[] p_defaultSphereVertexArr;

    /// <summary> Short form for new Vector3(float.NaN, float.NaN, float.NaN). </summary>
    private static readonly Vector3 p_vec3NaN = new Vector3(float.NaN, float.NaN, float.NaN);
    #endregion
    //=================================================================================================

    //=================================================================================================
    #region Properties
    /// <summary> Short form for new Vector3(float.NaN, float.NaN, float.NaN). </summary>
    public static Vector3 NaN => p_vec3NaN;

    /// <summary> Contains the vertices of a default sphere mesh. </summary>
    public static Vector3[] DefaultSphereVertexArr
    {
        get
        {
            if (p_defaultSphereVertexArr == null)
                InitializeDefaultSphereVertexArray();

            return p_defaultSphereVertexArr;
        }
    }
    #endregion
    //=================================================================================================

    //=================================================================================================
    #region Approximation Check Methods
    /// <summary> Uses MathUtility.SqrEpsilon for the squared distance check. </summary>
    public static bool Approximate(Vector3 _vec1, Vector3 _vec2)
        => (_vec2 - _vec1).sqrMagnitude <= MathUtility.SqrEpsilon;

    /// <summary> Uses MathUtility.SqrEpsilon * MathUtility.SqrEpsilon for the squared distance check. </summary>
    public static bool PreciseApproximate(Vector3 _vec1, Vector3 _vec2)
    {
        float _sqrEpsilon = MathUtility.SqrEpsilon, _sqrSqrEpsilon = _sqrEpsilon * _sqrEpsilon;
        return (_vec2 - _vec1).sqrMagnitude <= _sqrSqrEpsilon;
    }

    /// <summary> Uses MathUtility.SqrEpsilon for the angle check. </summary>
    public static bool AngleApproximate(Vector3 _vec1, Vector3 _vec2)
        => Angle(_vec1, _vec2) <= MathUtility.SqrEpsilon;//E^2 for angle to avoid jump exposure

    /// <summary> Uses MathUtility.SqrEpsilon * MathUtility.SqrEpsilon for the angle check. </summary>
    public static bool PreciseAngleApproximate(Vector3 _vec1, Vector3 _vec2)
    {
        float _sqrEpsilon = MathUtility.SqrEpsilon, _sqrSqrEpsilon = _sqrEpsilon * _sqrEpsilon;
        return Angle(_vec1, _vec2) <= _sqrSqrEpsilon;//E^2 for angle to avoid jump exposure
    }

    /// <summary> Uses MathUtility.AlternateSqrEpsilon for the squared distance check. </summary>
    public static bool AlternateApproximate(Vector3 _vec1, Vector3 _vec2)
        => (_vec2 - _vec1).sqrMagnitude <= MathUtility.SqrAlternateEpsilon;

    /// <summary> Uses MathUtility.AlternateSqrEpsilon * MathUtility.AlternateSqrEpsilon for the squared distance check. </summary>
    public static bool PreciseAlternateApproximate(Vector3 _vec1, Vector3 _vec2)
    {
        float _alternateSqrEpsilon = MathUtility.SqrAlternateEpsilon, _sqrAlternateSqrEpsilon = _alternateSqrEpsilon * _alternateSqrEpsilon;
        return (_vec2 - _vec1).sqrMagnitude <= _sqrAlternateSqrEpsilon;
    }

    /// <summary> A customized approximation check method. </summary>
    public static bool CustomApproximate(Vector3 _vec1, Vector3 _vec2, bool _alternateApproximation, bool _preciseApproximation)
    {
        bool _approximatelyMatches = _alternateApproximation ?
            _preciseApproximation ? PreciseAlternateApproximate(_vec1, _vec2) : AlternateApproximate(_vec1, _vec2) :
            _preciseApproximation ? PreciseApproximate(_vec1, _vec2) : Approximate(_vec1, _vec2);

        return _approximatelyMatches;
    }

    /// <summary> Uses MathUtility.AlternateSqrEpsilon for the angle check. </summary>
    public static bool AngleAlternateApproximate(Vector3 _vec1, Vector3 _vec2)
        => Angle(_vec1, _vec2) <= MathUtility.SqrAlternateEpsilon;//E^2 for angle to avoid jump exposure

    /// <summary> Uses MathUtility.AlternateSqrEpsilon * MathUtility.AlternateSqrEpsilon for the angle check. </summary>
    public static bool PreciseAngleAlternateApproximate(Vector3 _vec1, Vector3 _vec2)
    {
        float _alternateSqrEpsilon = MathUtility.SqrAlternateEpsilon, _sqrAlternateSqrEpsilon = _alternateSqrEpsilon * _alternateSqrEpsilon;
        return Angle(_vec1, _vec2) <= _sqrAlternateSqrEpsilon;//E^2 for angle to avoid jump exposure
    }
    #endregion
    //=================================================================================================

    //=================================================================================================
    #region Interpolation Methods
    /// <summary> Returns a smoothly interpolated Vector3 between _startVec and _endVec by combining Vector3.Lerp and MathUtility.SmoothLerp
    /// with _t clamped between 0 and 1. </summary>
    public static Vector3 SmoothLerp(Vector3 _startVec, Vector3 _endVec, float _t)
        => Vector3.Lerp(_startVec, _endVec, MathUtility.SmoothLerp(0.0f, 1.0f, _t));

    /// <summary> Returns a smoothly spherically interpolated Vector3 between _startVec and _endVec by combining Vector3.Slerp and MathUtility.SmoothLerp
    /// with _t clamped between 0 and 1. </summary>
    public static Vector3 SmoothSlerp(Vector3 _startVec, Vector3 _endVec, float _t)
        => Vector3.Slerp(_startVec, _endVec, MathUtility.SmoothLerp(0.0f, 1.0f, _t));

    /// <summary> Like Vector3.RotateTowards but the angle step is in degrees. </summary>
    public static Vector3 RotateTowards(Vector3 _fromVec, Vector3 _toVec, float _degreeStep, float _magnitudeStep = float.PositiveInfinity)
        => Vector3.RotateTowards(_fromVec, _toVec, _degreeStep * Mathf.Deg2Rad, _magnitudeStep);
    #endregion
    //=================================================================================================

    //=================================================================================================
    #region Ping Pong Methods

    #region Linear Ping Pong Functions
    /// <summary> Returns a linearly ping ponged Vector3 between _vec1 and _vec2 with the help of MathUtility.PingPongCustom. </summary>
    public static Vector3 PingPongLerpCustom(Vector3 _vec1, Vector3 _vec2, float _timer, float _speed)
        => Vector3.Lerp(_vec1, _vec2, MathUtility.PingPongCustom(_timer, 1.0f, _speed));

    /// <summary> Returns a spherically ping ponged Vector3 between _vec1 and _vec2 with the help of MathUtility.PingPongCustom. </summary>
    public static Vector3 PingPongSlerpCustom(Vector3 _vec1, Vector3 _vec2, float _timer, float _speed)
        => Vector3.Slerp(_vec1, _vec2, MathUtility.PingPongCustom(_timer, 1.0f, _speed));

    /// <summary> Returns a smoothly linearly ping ponged Vector3 between _vec1 and _vec2 with the help of MathUtility.PingPongCustom. </summary>
    public static Vector3 PingPongSmoothLerpCustom(Vector3 _vec1, Vector3 _vec2, float _timer, float _speed)
        => SmoothLerp(_vec1, _vec2, MathUtility.PingPongCustom(_timer, 1.0f, _speed));

    /// <summary> Returns a smoothly spherically ping ponged Vector3 between _vec1 and _vec2 with the help of MathUtility.PingPongCustom. </summary>
    public static Vector3 PingPongSmoothSlerpCustom(Vector3 _vec1, Vector3 _vec2, float _timer, float _speed)
        => SmoothSlerp(_vec1, _vec2, MathUtility.PingPongCustom(_timer, 1.0f, _speed));

    /// <summary> Like Vector3Utility.PingPongLerpCustom but uses TimeInfo.GlobalTime as the timer. </summary>
    public static Vector3 PingPongLerp(Vector3 _vec1, Vector3 _vec2, float _speed)
        => PingPongLerpCustom(_vec1, _vec2, TimeInfo.GlobalTime, _speed);

    /// <summary> Like Vector3Utility.PingPongSlerpCustom but uses TimeInfo.GlobalTime as the timer. </summary>
    public static Vector3 PingPongSlerp(Vector3 _vec1, Vector3 _vec2, float _speed)
        => PingPongSlerpCustom(_vec1, _vec2, TimeInfo.GlobalTime, _speed);

    /// <summary> Like Vector3Utility.PingPongSmoothLerpCustom but uses TimeInfo.GlobalTime as the timer. </summary>
    public static Vector3 PingPongSmoothLerp(Vector3 _vec1, Vector3 _vec2, float _speed)
        => PingPongSmoothLerpCustom(_vec1, _vec2, TimeInfo.GlobalTime, _speed);

    /// <summary> Like Vector3Utility.PingPongSmoothSlerpCustom but uses TimeInfo.GlobalTime as the timer. </summary>
    public static Vector3 PingPongSmoothSlerp(Vector3 _vec1, Vector3 _vec2, float _speed)
        => PingPongSmoothSlerpCustom(_vec1, _vec2, TimeInfo.GlobalTime, _speed);
    #endregion

    #region Circular Ping Pong functions
    /// <summary> Returns a circularly ping ponged Vector3 between _vec1 and _vec2 with the help of MathUtility.PingPongSinusCustom. </summary>
    public static Vector3 PingPongSinusLerpCustom(Vector3 _vec1, Vector3 _vec2, float _radianTimer, float _speed, float _horizontalCompression = 1.0f)
        => Vector3.Lerp(_vec1, _vec2, MathUtility.PingPongSinusCustom(_radianTimer, 1.0f, _speed, _horizontalCompression));

    /// <summary> Returns a spherically circularly ping ponged Vector3 between _vec1 and _vec2 with the help of MathUtility.PingPongSinusCustom. </summary>
    public static Vector3 PingPongSinusSlerpCustom(Vector3 _vec1, Vector3 _vec2, float _radianTimer, float _speed, float _horizontalCompression = 1.0f)
        => Vector3.Slerp(_vec1, _vec2, MathUtility.PingPongSinusCustom(_radianTimer, 1.0f, _speed, _horizontalCompression));

    /// <summary> Returns a smoothly circularly ping ponged Vector3 between _vec1 and _vec2 with the help of MathUtility.PingPongSinusCustom. </summary>
    public static Vector3 PingPongSinusSmoothLerpCustom(Vector3 _vec1, Vector3 _vec2, float _radianTimer, float _speed, float _horizontalCompression = 1.0f)
        => SmoothLerp(_vec1, _vec2, MathUtility.PingPongSinusCustom(_radianTimer, 1.0f, _speed, _horizontalCompression));

    /// <summary> Returns a smoothly spherically circularly ping ponged Vector3 between _vec1 and _vec2 with the help of MathUtility.PingPongSinusCustom. </summary>
    public static Vector3 PingPongSinusSmoothSlerpCustom(Vector3 _vec1, Vector3 _vec2, float _radianTimer, float _speed, float _horizontalCompression = 1.0f)
        => SmoothSlerp(_vec1, _vec2, MathUtility.PingPongSinusCustom(_radianTimer, 1.0f, _speed, _horizontalCompression));

    /// <summary> Like Vector3Utility.PingPongSinusLerpCustom but uses Time.GlobalTime as the timer. </summary>
    public static Vector3 PingPongSinusLerp(Vector3 _vec1, Vector3 _vec2, float _speed, float _horizontalCompression = 1.0f)
        => PingPongSinusLerpCustom(_vec1, _vec2, TimeInfo.GlobalTime, _speed, _horizontalCompression);

    /// <summary> Like Vector3Utility.PingPongSinusSlerpCustom but uses Time.GlobalTime as the timer. </summary>
    public static Vector3 PingPongSinusSlerp(Vector3 _vec1, Vector3 _vec2, float _speed, float _horizontalCompression = 1.0f)
        => PingPongSinusSlerpCustom(_vec1, _vec2, TimeInfo.GlobalTime, _speed, _horizontalCompression);

    /// <summary> Like Vector3Utility.PingPongSinusSmoothLerpCustom but uses Time.GlobalTime as the timer. </summary>
    public static Vector3 PingPongSinusSmoothLerp(Vector3 _vec1, Vector3 _vec2, float _speed, float _horizontalCompression = 1.0f)
        => PingPongSinusSmoothLerpCustom(_vec1, _vec2, TimeInfo.GlobalTime, _speed, _horizontalCompression);

    /// <summary> Like Vector3Utility.PingPongSinusSmoothSlerpCustom but uses Time.GlobalTime as the timer. </summary>
    public static Vector3 PingPongSinusSmoothSlerp(Vector3 _vec1, Vector3 _vec2, float _speed, float _horizontalCompression = 1.0f)
        => PingPongSinusSmoothSlerpCustom(_vec1, _vec2, TimeInfo.GlobalTime, _speed, _horizontalCompression);
    #endregion
    #endregion
    //=================================================================================================

    //=================================================================================================
    #region Normalization Methods
    /// <summary> Determines whether the parameter squared magnitude is in the approximate normalized radius. </summary>
    public static bool IsNormalized(float _sqrMagnitude)
        => _sqrMagnitude > MathUtility.SqrEpsilon && Mathf.Abs(1.0f - _sqrMagnitude) <= 0.001f;

    /// <summary> Determines whether the squared magnitude of the parameter Vector3 is in the approximate normalized radius. </summary>
    public static bool IsNormalized(Vector3 _vec)
    {
        float _sqrMagnitude = _vec.sqrMagnitude;
        return IsNormalized(_sqrMagnitude);
    }

    /// <summary> Normalizes the ref parameter Vector3 only if its given magnitude isn't in the approximate normalized radius. </summary>
    public static void Normalize(ref Vector3 _vec, float _magnitude)
    {
        if (_magnitude <= Mathf.Epsilon || Mathf.Abs(1.0f - _magnitude) <= 0.001f) return;

        Vector3 _curVec = _vec;
        _curVec /= _magnitude;
        _vec = _curVec;
    }

    /// <summary> Normalizes the ref parameter Vector3 only if its squared magnitude isn't in the approximate normalized radius. </summary>
    public static void Normalize(ref Vector3 _vec)
    {
        Vector3 _curVec = _vec;
        float _sqrMagnitude = _curVec.sqrMagnitude;

        if (_sqrMagnitude <= MathUtility.SqrEpsilon || Mathf.Abs(1.0f - _sqrMagnitude) <= 0.001f) return;

        float _magnitude = Mathf.Sqrt(_sqrMagnitude);
        _curVec /= _magnitude;

        _vec = _curVec;
    }
    #endregion
    //=================================================================================================

    #region Clamping Methods
    /// <summary> Clamps the magnitude of the ref parameter Vector3 between 0 and _maxMagnitude. </summary>
    public static void ClampMagnitude(ref Vector3 _vec, float _maxMagnitude)
        => _vec = Vector3.ClampMagnitude(_vec, _maxMagnitude);

    /// <summary> Clamps the magnitude of the ref parameter Vector3 between _minMagnitude and _maxMagnitude if the Vector3 isn't Vector3.zero or close to it. </summary>
    public static void ClampMagnitude(ref Vector3 _vec, float _minMagnitude, float _maxMagnitude)
    {
        Vector3 _curVec = _vec;
        float _sqrMagnitude = _curVec.sqrMagnitude;

        if (_sqrMagnitude <= Mathf.Epsilon) return;

        bool _isLessThanMinMagnitude = _sqrMagnitude < _minMagnitude * _minMagnitude, _isMoreThanMaxMagnitude = _sqrMagnitude > _maxMagnitude * _maxMagnitude;
        bool _isOutOfMagnitudeRange = _isLessThanMinMagnitude || _isMoreThanMaxMagnitude;

        if (_isOutOfMagnitudeRange)
        {
            float _magnitude = Mathf.Sqrt(_sqrMagnitude);
            Vector3 _normalizedVec = _curVec / _magnitude;
            _vec = _normalizedVec * (_isLessThanMinMagnitude ? _minMagnitude : _maxMagnitude);
        }
    }

    //
    public static Vector3 ClampAngle(Vector3 _variableVec, Vector3 _staticVec, float _minAngle, float _maxAngle)
    {
        float _curAngle = Vector3.Angle(_variableVec, _staticVec);
        if (_curAngle < _minAngle || _curAngle > _maxAngle)
        {
            float _angleDelta = _curAngle - (_curAngle < _minAngle ? _minAngle : _maxAngle);
            return RotateTowards(_variableVec, _staticVec, _angleDelta);
        }

        return _variableVec;
    }
    #endregion

    //=================================================================================================
    #region Validation Check Methods
    /// <summary> Determines whether a component of the parameter Vector3 is float.NaN. </summary>
    public static bool IsNaN(Vector3 _vec)
        => float.IsNaN(_vec.x) || float.IsNaN(_vec.y) || float.IsNaN(_vec.z);

    //
    public static bool IsApproximatelyZero(Vector3 _vec)
        => MathUtility.IsApproximatelyPreciselyZero(_vec.sqrMagnitude);

    /// <summary> Determines whether a component of the parameter Vector3 is float.PositiveInfinity. </summary>
    public static bool IsPositiveInfinity(Vector3 _vec)
        => float.IsPositiveInfinity(_vec.x) || float.IsPositiveInfinity(_vec.y) || float.IsPositiveInfinity(_vec.z);

    /// <summary> Determines whether a component of the parameter Vector3 is float.NegativeInfinity. </summary>
    public static bool IsNegativeInfinity(Vector3 _vec)
        => float.IsNegativeInfinity(_vec.x) || float.IsNegativeInfinity(_vec.y) || float.IsNegativeInfinity(_vec.z);

    /// <summary> Determines whether a component of the parameter Vector3 is either float.PositiveInfinity or float.NegativeInfinity. </summary>
    public static bool IsInfinity(Vector3 _vec)
        => IsPositiveInfinity(_vec) || IsNegativeInfinity(_vec);

    /// <summary> Determines whether the parameter Vector3 isn't NaN or Infinity. </summary>
    public static bool IsValid(Vector3 _vec)
        => !IsNaN(_vec) && !IsInfinity(_vec);
    #endregion
    //=================================================================================================

    //=================================================================================================
    #region Angle Methods
    /// <summary> Like Vector3.Angle but avoids the square root calculation. [even more performant, but it always returns from 0.0f to 90.0f] </summary>
    public static float Angle(Vector3 _vec1, Vector3 _vec2)
    {
        //Cos(2x) = 2.0f * dot(vec1, vec2)^2 / (vec1.sqrMag * vec2.sqrMag) - 1.0f

        float _sqrMagMult = _vec1.sqrMagnitude * _vec2.sqrMagnitude;
        float _dot = Vector3.Dot(_vec1, _vec2);
        float _angleCos2 = 2.0f * _dot * _dot / _sqrMagMult - 1.0f;

        float _angle = Mathf.Acos(_angleCos2) * (0.5f * Mathf.Rad2Deg);

        return _angle;
    }

    /// <summary> SignedAngle in case if you already have the magnitude multiplication of both vectors. </summary>
    public static float SignedAngle(Vector3 _fromVec, Vector3 _toVec, Vector3 _axis, float _multipliedMagnitude)
    {
        float _unsignedAngle = _fromVec.Angle(_toVec, _multipliedMagnitude);

        float _crossX = _fromVec.y * _toVec.z - _fromVec.z * _toVec.y;
        float _crossY = _fromVec.z * _toVec.x - _fromVec.x * _toVec.z;
        float _crossZ = _fromVec.x * _toVec.y - _fromVec.y * _toVec.x;

        float _signedValue = _axis.x * _crossX + _axis.y * _crossY + _axis.z * _crossZ;
        float _sign = Mathf.Sign(_signedValue);

        return _unsignedAngle * _sign;
    }

    /// <summary> SignedAngle in case if you already have the unsigned angle between the two vectors. </summary>
    public static float SignedAngle2(Vector3 _fromVec, Vector3 _toVec, Vector3 _axis, float _unsignedAngle)
    {
        float _crossX = _fromVec.y * _toVec.z - _fromVec.z * _toVec.y;
        float _crossY = _fromVec.z * _toVec.x - _fromVec.x * _toVec.z;
        float _crossZ = _fromVec.x * _toVec.y - _fromVec.y * _toVec.x;

        float _signedValue = _axis.x * _crossX + _axis.y * _crossY + _axis.z * _crossZ;
        float _sign = Mathf.Sign(_signedValue);

        return _unsignedAngle * _sign;
    }
    #endregion
    //=================================================================================================

    //=================================================================================================
    #region Circle Simulation Methods
    /// <summary> Returns Vector3 points forming specific number of circles starting from a minimum radius to a maximum one along specific axes. </summary>
    public static Vector3[] SimulateCirclesCustom(int _pointCount, int _circleCount,
        Vector3 _circleCenter,
        float _minRadius, float _maxRadius,
        Vector3 _cosAxis, Vector3 _sinAxis,
        bool _smoothRadius = false)
    {
        float _angle = 0.0f;
        Vector3[] _pointArr = new Vector3[_pointCount];
        float _additiveAngle = 2.0f * Mathf.PI * (float)_circleCount / (float)_pointCount;
        bool _radiusIsConstant = _minRadius == _maxRadius;

        Func<float, float, float, float> _interpolateFunc = _smoothRadius ?
            new Func<float, float, float, float>(Mathf.SmoothStep) :
            new Func<float, float, float, float>(Mathf.Lerp);

        for (int pointIndex = 0; pointIndex < _pointCount; ++pointIndex)
        {
            float _cos = Mathf.Cos(_angle), _sin = Mathf.Sin(_angle);
            float _curRadius = _radiusIsConstant ? _maxRadius : _interpolateFunc(_minRadius, _maxRadius, (float)pointIndex / (float)(_pointCount - 1));
            _pointArr[pointIndex] = _circleCenter + (_cosAxis * _cos + _sinAxis * _sin) * _curRadius;

            _angle += _additiveAngle;
        }

        return _pointArr;
    }

    /// <summary> Returns Vector3 points forming a circle with specific radius along specific axes. </summary>
    public static Vector3[] SimulateCircleCustom(int _pointCount, Vector3 _circleCenter, float _constantRadius, Vector3 _cosAxis, Vector3 _sinAxis)
        => SimulateCirclesCustom(_pointCount, 1, _circleCenter, _constantRadius, _constantRadius, _cosAxis, _sinAxis);

    /// <summary> Returns Vector3 points forming specific number of circles starting from a minimum radius to a maximum one along the global axes, right and forward. </summary>
    public static Vector3[] SimulateCirclesGlobal(int _pointCount, int _circleCount, Vector3 _circleCenter, float _minRadius, float _maxRadius, bool _smoothRadius = false)
        => SimulateCirclesCustom(_pointCount, _circleCount, _circleCenter, _minRadius, _maxRadius, Vector3.right, Vector3.forward, _smoothRadius);

    /// <summary> Returns Vector3 points forming a circle with specific radius along the global axes, right and forward. </summary>
    public static Vector3[] SimulateCircleGlobal(int _pointCount, Vector3 _circleCenter, float _constantRadius)
        => SimulateCirclesGlobal(_pointCount, 1, _circleCenter, _constantRadius, _constantRadius);
    #endregion
    //=================================================================================================

    //=================================================================================================
    #region Sphere Simulation Methods
    //The complexity isn't working somehow, maybe because of the game loop?
    /// <summary> Initializes the default sphere vertices if it's not initialized yet, for further spherical simulations. </summary>
    public static void InitializeDefaultSphereVertexArray()
    {
        if (p_defaultSphereVertexArr != null) return;

        GameObject _sampleSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        Mesh _sampleSphereMesh = _sampleSphere.GetComponent<MeshFilter>().mesh;

        byte _sphereComplexityCount = 1;
        float _sphereComplexityRotationAngle = 20.0f;

        p_defaultSphereVertexArr = new Vector3[_sampleSphereMesh.vertices.Length * _sphereComplexityCount];
        int _curStoredVertexIndex = 0;

        for (byte sphereComplexityIndex = 0; sphereComplexityIndex < _sphereComplexityCount; ++sphereComplexityIndex)
        {
            byte _randVal = (byte)UnityEngine.Random.Range(1, 4);
            _sampleSphere.transform.Rotate(_randVal == 1 ? Vector3.right : _randVal == 2 ? Vector3.up : Vector3.forward, _sphereComplexityRotationAngle, Space.World);

            _sampleSphereMesh.RecalculateNormals();
            _sampleSphereMesh.RecalculateTangents();
            _sampleSphereMesh.RecalculateBounds();
            _sampleSphereMesh.OptimizeReorderVertexBuffer();

            for (int vertexIndex = 0; vertexIndex < _sampleSphereMesh.vertices.Length; ++vertexIndex)
            {
                p_defaultSphereVertexArr[_curStoredVertexIndex] = Vector3.ClampMagnitude(_sampleSphereMesh.vertices[vertexIndex], 1.0f);
                ++_curStoredVertexIndex;
            }
        }

        GameObject.Destroy(_sampleSphere);
    }

    /// <summary> Returns Vector3 points forming specific number of spheres starting from a minimum radius to a maximum one. </summary>
    public static Vector3[] SimulateSpheresCustom(int _sphereCount, Vector3 _sphereCenter, float _minSphereRadius, float _maxSphereRadius)
    {
        InitializeDefaultSphereVertexArray();

        Vector3[] _pointArr = new Vector3[_sphereCount * p_defaultSphereVertexArr.Length];
        bool _sphereRadiusIsConstant = _minSphereRadius == _maxSphereRadius;
        int _curStoredPointIndex = 0;

        for (int sphereIndex = 0; sphereIndex < _sphereCount; ++sphereIndex)
        {
            float _curSphereRadiusRelation = _sphereCount == 1 ? 1.0f : (float)sphereIndex / (float)(_sphereCount - 1);
            float _curSphereRadius = _sphereRadiusIsConstant ? _maxSphereRadius : Mathf.Lerp(_minSphereRadius, _maxSphereRadius, _curSphereRadiusRelation);
            for (int vertexIndex = 0; vertexIndex < p_defaultSphereVertexArr.Length; ++vertexIndex)
            {
                _pointArr[_curStoredPointIndex] = _sphereCenter + p_defaultSphereVertexArr[vertexIndex] * _curSphereRadius;
                ++_curStoredPointIndex;
            }
        }

        return _pointArr;
    }

    /// <summary> Returns Vector3 points forming a sphere with specific radius. </summary>
    public static Vector3[] SimulateSphere(Vector3 _sphereCenter, float _constantRadius)
    {
        return SimulateSpheresCustom(1, _sphereCenter, _constantRadius, _constantRadius);
    }
    #endregion
    //=================================================================================================

    //=================================================================================================
    #region Extra Methods
    /// <summary> Returns the closest global vector to _vec. [returns Vector3.forward * MathUtility.Signf(_vec.z) if the components have the same value] </summary>
    public static Vector3 RawVector(Vector3 _vec)
    {
        Vector3 _curVec = _vec;

        float _absX = Mathf.Abs(_curVec.x);
        float _absY = Mathf.Abs(_curVec.y);
        float _absZ = Mathf.Abs(_curVec.z);

        if (_absY == _absX && _absZ == _absX)
            return Vector3.forward * MathUtility.Signf(_curVec.z);

        float _largestAbsComponent = Mathf.Max(_absX, _absY, _absZ);

        Vector3 _rawVec = _largestAbsComponent == _absX ? Vector3.right * MathUtility.Signf(_curVec.x) :
            _largestAbsComponent == _absY ? Vector3.up * MathUtility.Signf(_curVec.y) :
            Vector3.forward * MathUtility.Signf(_curVec.z);

        return _rawVec;
    }
    #endregion
    //=================================================================================================
}
