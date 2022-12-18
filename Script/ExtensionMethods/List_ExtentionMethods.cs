using System.Collections.Generic;
//

//
public static class List_ExtentionMethods
{
    //
    public static int ContainCount<T>(this List<T> _list, T _targetElement)
    {
        int _count = 0;

        List<T> _list2 = new List<T>();
        _list2.AddRange(_list);

        while (_list2.Contains(_targetElement))
        {
            _list2.Remove(_targetElement);
            ++_count;
        }

        return _count;
    }
}
