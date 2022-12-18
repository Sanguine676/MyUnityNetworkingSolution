using System.Collections.Generic;
using UnityEngine;
//

//
public static class Array_ExtentionMethods
{
    //
    public static bool Contains<T>(this T[] _elementArr, T _targetElement, out int _elementIndex)
    {
        for (int i = 0; i < _elementArr.Length; ++i)
            if (EqualityComparer<T>.Default.Equals(_elementArr[i], _targetElement))
            {
                _elementIndex = i;
                return true;
            }

        _elementIndex = -1;
        return false;
    }

    //
    public static bool Contains<T>(this T[] _elementArr, T _targetElement)
    {
        int _elementIndex;
        return _elementArr.Contains<T>(_targetElement, out _elementIndex);
    }

    //
    public static int IndexOf<T>(this T[] _elementArr, T _targetElement)
    {
        int _elementIndex;
        _elementArr.Contains<T>(_targetElement, out _elementIndex);

        return _elementIndex;
    }
}
