using System.Collections.Generic;
using UnityEngine;
//

//
public static class GameObject_ExtentionMethods
{
    //
    public static bool TryGetComponentsInChildren<T>(this GameObject gObj, out T[] compArr, bool includeSelf = false, int maxComponentCount = int.MaxValue)
    {
        Transform[] transArr = gObj.GetComponentsInChildren<Transform>();
        Transform objTrans = includeSelf ? gObj.transform : null;
        List<T> compList = new List<T>();

        for (int i = 0; i < transArr.Length; ++i)
        {
            if (includeSelf && transArr[i] == objTrans)
                continue;

            T comp = default(T);
            bool obtained = transArr[i].TryGetComponent<T>(out comp);

            if (obtained)
                compList.Add(comp);

            if (compList.Count == maxComponentCount)
                break;
        }

        compArr = compList.ToArray();

        return compArr.Length != 0;
    }
}
