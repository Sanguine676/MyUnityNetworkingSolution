using UnityEngine;
//

//
public static class RandomUtility
{
    //
    public static int RandInt => Random.Range(int.MinValue, int.MaxValue);

    //
    public static sbyte RandSign
    {
        get
        {
            return (sbyte)RandSignf;
        }
    }

    //
    public static float RandSignf
    {
        get
        {
            return Mathf.Sign(Random.Range(-1, 1));
        }
    }

    //
    public static byte RandChance
    {
        get
        {
            return (byte)Random.Range(0, 101);
        }
    }

    //
    public static float RandChancef
    {
        get
        {
            return Random.Range(0.0f, 100.0f);
        }
    }

    //
    public static byte RandChance01 => (byte)Random.Range(0, 2);

    //
    public static float RandChance01f => Random.Range(0.0f, 1.0f);

    //
    public static bool RandBool => RandChance01 == 0;

    //
    public static bool RandBool2 => RandChance == RandChance;

    //
    public static Vector3 PointInsideUpwardHorizontalHemisphere
    {
        get
        {
            Vector3 _randPoint = Random.insideUnitSphere;
            _randPoint.y = Mathf.Abs(_randPoint.y);

            return _randPoint;
        }
    }

    //
    public static Vector3 PointInsideDownwardHorizontalHemisphere
    {
        get
        {
            Vector3 _randPoint = Random.insideUnitSphere;
            _randPoint.y = -Mathf.Abs(_randPoint.y);

            return _randPoint;
        }
    }

    //
    public static Vector3 PointInsideHorizontalCircle
    {
        get
        {
            Vector3 _randPoint = Random.insideUnitCircle;
            _randPoint.z = _randPoint.y;
            _randPoint.y = 0.0f;

            return _randPoint;
        }
    }

    //
    public static Vector3 PointInsideHorizontalCircle1(float _minRadius, float _maxRadius)
    {
        return PointInsideHorizontalCircle * Random.Range(_minRadius, _maxRadius);
    }

    //
    public static Vector3 PointInsideHorizontalCircle2(float _minRadius, float _maxRadius)
    {
        Vector3 _randPoint = new Vector3(RandSignf * Random.Range(_minRadius, _maxRadius), 0.0f, RandSignf * Random.Range(_minRadius, _maxRadius));
        _randPoint /= 1.4142135f; //Sqrt(2)

        return _randPoint;
    }

    //
    public static bool CheckChance(float _chance, bool _integralChance = true)
    {
        float _randVal = _integralChance ? RandChancef : RandChance;

        return _randVal <= _chance;
    }
}
