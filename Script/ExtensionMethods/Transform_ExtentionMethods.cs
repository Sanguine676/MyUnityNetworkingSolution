using UnityEngine;
//

//
public static class Transform_ExtentionMethods
{
    //
    public static void ResetPosAndRot(this Transform _transform)
    {
        _transform.localPosition = Vector3.zero;
        _transform.localRotation = Quaternion.identity;
    }

    //
    public static void SetPosAndRot(this Transform transform, Transform targetTrans)
    {
        Transform targetTransform = targetTrans;
        transform.SetPositionAndRotation(targetTransform.position, targetTransform.rotation);
    }

    //
    public static void SetPosAxis(this Transform trans, byte axisIndex, bool world, float newAxis)
    {
        Vector3 pos;
        if (world)
        {
            pos = trans.position;
            pos[axisIndex] = newAxis;

            trans.position = pos;
        }
        else
        {
            pos = trans.localPosition;
            pos[axisIndex] = newAxis;

            trans.localPosition = pos;
        }
    }

    //
    public static void SetScaleAxis(this Transform trans, byte axisIndex, float newAxis)
    {
        Vector3 scale = trans.localScale;
        scale[axisIndex] = newAxis;
        trans.localScale = scale;
    }

    //
    public static void ClosestTransformTo(this Transform[] transArr, Transform targetTrans, out Transform closestTrans, out int closestIndex, out float closestSqrDist,
        float maxTransDist = float.PositiveInfinity)
    {
        Vector3 targetPos = targetTrans.position;
        float maxTransSqrDist = maxTransDist * maxTransDist;
        float minSqrDist = float.PositiveInfinity;
        Transform nearestTrans = null;
        int nearestIndex = -1;

        for (int i = 0; i < transArr.Length; ++i)
        {
            float curSqrDist = (targetPos - transArr[i].position).sqrMagnitude;
            if (curSqrDist > maxTransSqrDist)
                continue;

            if (minSqrDist > curSqrDist)
            {
                minSqrDist = curSqrDist;
                nearestTrans = transArr[i];
                nearestIndex = i;
            }
        }

        closestTrans = nearestTrans;
        closestIndex = nearestIndex;
        closestSqrDist = minSqrDist;
    }

    //
    public static void FurthestTransformTo(this Transform[] transArr, Transform targetTrans, out Transform furthestTrans, out int furthestIndex, out float furthestSqrDist,
        float minTransDist = float.NegativeInfinity)
    {
        Vector3 targetPos = targetTrans.position;
        float minTransSqrDist = minTransDist * minTransDist;
        float maxSqrDist = float.NegativeInfinity;
        Transform farthestTrans = null;
        int farthestIndex = -1;

        for (int i = 0; i < transArr.Length; ++i)
        {
            float curSqrDist = (targetPos - transArr[i].position).sqrMagnitude;
            if (!float.IsNegativeInfinity(minTransDist) && curSqrDist < minTransSqrDist)
                continue;

            if (maxSqrDist < curSqrDist)
            {
                maxSqrDist = curSqrDist;
                farthestTrans = transArr[i];
                farthestIndex = i;
            }
        }

        furthestTrans = farthestTrans;
        furthestIndex = farthestIndex;
        furthestSqrDist = maxSqrDist;
    }

    //
    public static Transform ClosestTransformTo(this Transform[] transArr, Transform targetTrans, float maxTransDist = float.PositiveInfinity)
    {
        Transform closestTrans;
        int closestIndex;
        float closestSqrDist;
        transArr.ClosestTransformTo(targetTrans, out closestTrans, out closestIndex, out closestSqrDist, maxTransDist);

        return closestTrans;
    }

    //
    public static Transform FurthestTransformTo(this Transform[] transArr, Transform targetTrans, float minTransDist = float.NegativeInfinity)
    {
        Transform furthestTrans;
        int furthestIndex;
        float furthestSqrDist;
        transArr.FurthestTransformTo(targetTrans, out furthestTrans, out furthestIndex, out furthestSqrDist, minTransDist);

        return furthestTrans;
    }

    //
    public static Transform ClosestTransformTo(this Transform[] transArr, Transform targetTrans, out float sqrDist, float maxTransDist = float.PositiveInfinity)
    {
        Transform closestTrans;
        int closestIndex;
        float closestSqrDist;
        transArr.ClosestTransformTo(targetTrans, out closestTrans, out closestIndex, out closestSqrDist, maxTransDist);

        sqrDist = closestSqrDist;
        return closestTrans;
    }

    //
    public static Transform FurthestTransformTo(this Transform[] transArr, Transform targetTrans, out float sqrDist, float minTransDist = float.NegativeInfinity)
    {
        Transform furthestTrans;
        int furthestIndex;
        float furthestSqrDist;
        transArr.FurthestTransformTo(targetTrans, out furthestTrans, out furthestIndex, out furthestSqrDist, minTransDist);

        sqrDist = furthestSqrDist;
        return furthestTrans;
    }

    //
    public static float ClosestTransformSqrDistTo(this Transform[] transArr, Transform targetTrans, float maxTransDist = float.PositiveInfinity)
    {
        Transform closestTrans;
        int closestIndex;
        float closestSqrDist;
        transArr.ClosestTransformTo(targetTrans, out closestTrans, out closestIndex, out closestSqrDist, maxTransDist);

        return closestSqrDist;
    }

    //
    public static float FurthestTransformSqrDistTo(this Transform[] transArr, Transform targetTrans, float minTransDist = float.NegativeInfinity)
    {
        Transform furthestTrans;
        int furthestIndex;
        float furthestSqrDist;
        transArr.FurthestTransformTo(targetTrans, out furthestTrans, out furthestIndex, out furthestSqrDist, minTransDist);

        return furthestSqrDist;
    }

    //
    public static float ClosestTransformDistTo(this Transform[] transArr, Transform targetTrans, float maxTransDist = float.PositiveInfinity)
    {
        return Mathf.Sqrt(transArr.ClosestTransformSqrDistTo(targetTrans, maxTransDist));
    }

    //
    public static float FurthestTransformDistTo(this Transform[] transArr, Transform targetTrans, float minTransDist = float.NegativeInfinity)
    {
        return Mathf.Sqrt(transArr.FurthestTransformSqrDistTo(targetTrans, minTransDist));
    }

    //
    public static Transform FindInChildren(this Transform trans, string objName, bool containsName = false)
    {
        Transform curTrans = trans;
        Transform[] childrenTransArr = curTrans.GetComponentsInChildren<Transform>();

        for (int i = 0; i < childrenTransArr.Length; ++i)
            if (childrenTransArr[i] != curTrans && (containsName ? childrenTransArr[i].name.Contains(objName) : childrenTransArr[i].name == objName))
                return childrenTransArr[i];

        return null;
    }
}
