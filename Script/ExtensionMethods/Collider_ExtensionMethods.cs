using System.Collections.Generic;
using UnityEngine;
//

//
public static class Collider_ExtensionMethods
{
    //
    public static Vector3 GetBottomPoint(this Collider _collider, bool _alongTransform = true)
    {
        float _extentY = _collider.bounds.extents.y;
        Vector3 _center = _collider.bounds.center;
        Vector3 _downward = _alongTransform ? -_collider.transform.up : Vector3.down;
        Vector3 _bottomPoint = _center + _downward * _extentY;

        return _bottomPoint;
    }

    //
    public static Vector3 GetTopPoint(this Collider _collider, bool _alongTransform = true)
    {
        float _extentY = _collider.bounds.extents.y;
        Vector3 _center = _collider.bounds.center;
        Vector3 _upward = _alongTransform ? _collider.transform.up : Vector3.up;
        Vector3 _topPoint = _center + _upward * _extentY;

        return _topPoint;
    }

    //
    public static Vector3 GetRightPoint(this Collider _collider, bool _alongTransform = true)
    {
        float _extentX = _collider.bounds.extents.x;
        Vector3 _center = _collider.bounds.center;
        Vector3 _rightward = _alongTransform ? _collider.transform.right : Vector3.right;
        Vector3 _rightPoint = _center + _rightward * _extentX;

        return _rightPoint;
    }

    //
    public static Vector3 GetLeftPoint(this Collider _collider, bool _alongTransform = true)
    {
        float _extentX = _collider.bounds.extents.x;
        Vector3 _center = _collider.bounds.center;
        Vector3 _leftward = _alongTransform ? -_collider.transform.right : Vector3.left;
        Vector3 _leftPoint = _center + _leftward * _extentX;

        return _leftPoint;
    }

    //
    public static Collider[] GetIntersectingColliderArr(this Collider _collider, int _maxHitColliderCount = 64)
    {
        Vector3 _boundsCenter = _collider.bounds.center;
        Vector3 _boundsSize = _collider.bounds.size;
        float _overlapSphereRadius = Mathf.Max(_boundsSize.x, _boundsSize.y, _boundsSize.z) * 3.4f;
        Collider[] _hitColliderArr = new Collider[_maxHitColliderCount];
        int _hitColliderCount = Physics.OverlapSphereNonAlloc(_boundsCenter, _overlapSphereRadius, _hitColliderArr, Physics.AllLayers, QueryTriggerInteraction.Ignore);
        List<Collider> _hitColliderList = new List<Collider>();

        for (int hitColliderIndex = 0; hitColliderIndex < _hitColliderCount; ++hitColliderIndex)
        {
            Collider _curHitCollider = _hitColliderArr[hitColliderIndex];
            if (_curHitCollider == _collider)
                continue;

            if (_curHitCollider.bounds.Intersects(_collider.bounds))
                _hitColliderList.Add(_curHitCollider);
        }

        return _hitColliderList.ToArray();
    }

    //
    public static bool Intersects(this Collider _collider)
        => _collider.GetIntersectingColliderArr().Length != 0;

    //
    public static Collider[] GetIntersectingColliderArr(this Collider[] _colliderArr, int _maxHitColliderCount = 64)
    {
        List<Collider> _hitColliderList = new List<Collider>();
        for (int colliderIndex = 0; colliderIndex < _colliderArr.Length; ++colliderIndex)
        {
            Collider _curCollider = _colliderArr[colliderIndex];
            Vector3 _curColliderBoundsCenter = _curCollider.bounds.center;
            Vector3 _curColliderBoundsSize = _curCollider.bounds.size;
            float _curColliderOverlapSphereRadius = Mathf.Max(_curColliderBoundsSize.x, _curColliderBoundsSize.y, _curColliderBoundsSize.z) * 2.2f;
            Collider[] _hitColliderArr = new Collider[_maxHitColliderCount];
            int _hitColliderCount = Physics.OverlapSphereNonAlloc
                (_curColliderBoundsCenter, _curColliderOverlapSphereRadius, _hitColliderArr, Physics.AllLayers, QueryTriggerInteraction.Ignore);

            for (int hitColliderIndex = 0; hitColliderIndex < _hitColliderCount; ++hitColliderIndex)
            {
                Collider _curHitCollider = _hitColliderArr[hitColliderIndex];
                bool _selfHit = false;
                for (int otherColliderIndex = 0; otherColliderIndex < _colliderArr.Length; ++otherColliderIndex)
                {
                    Collider _curOtherCollider = _colliderArr[otherColliderIndex];
                    if (_curOtherCollider == _curHitCollider)
                    {
                        _selfHit = true;
                        break;
                    }
                }

                if (_selfHit)
                    continue;

                if (!_hitColliderList.Contains(_curCollider) && _curHitCollider.bounds.Intersects(_curCollider.bounds))
                    _hitColliderList.Add(_curHitCollider);
            }
        }

        return _hitColliderList.ToArray();
    }

    //
    public static bool Intersects(this Collider[] _colliderArr)
        => _colliderArr.GetIntersectingColliderArr().Length != 0;

    //
    public static Collider[] BoxOverlapsColliderArr(this Collider _collider, int _maxHitColliderCount = 64)
    {
        Vector3 _boundsCenter = _collider.bounds.center;
        Vector3 _boundsExtents = _collider.bounds.extents;
        Collider[] _hitColliderArr = new Collider[_maxHitColliderCount];
        int _hitColliderCount = Physics.OverlapBoxNonAlloc(_boundsCenter, _boundsExtents, _hitColliderArr, Quaternion.identity, Physics.AllLayers, QueryTriggerInteraction.Ignore);

        List<Collider> _hitColliderList = new List<Collider>();
        for (int i = 0; i < _hitColliderCount; ++i)
        {
            Collider _curHitCollider = _hitColliderArr[i];
            if (_curHitCollider)
                _hitColliderList.Add(_curHitCollider);
        }

        return _hitColliderList.ToArray();
    }

    //
    public static bool BoxOverlaps(this Collider _collider)
        => _collider.BoxOverlapsColliderArr().Length != 0;

    //
    public static Collider[] BoxOverlapsColliderArr(this Collider[] _colliderArr, int _maxHitPerColliderCount = 64)
    {
        List<Collider> _hitColliderList = new List<Collider>();
        for (int colliderIndex = 0; colliderIndex < _colliderArr.Length; ++colliderIndex)
        {
            Collider _curCollider = _colliderArr[colliderIndex];
            Collider[] _curHitColliderArr = _curCollider.BoxOverlapsColliderArr(_maxHitPerColliderCount);
            for (int hitColliderIndex = 0; hitColliderIndex < _curHitColliderArr.Length; ++hitColliderIndex)
            {
                Collider _curHitCollider = _curHitColliderArr[hitColliderIndex];
                bool _selfHit = false;
                for (int otherColliderIndex = 0; otherColliderIndex < _colliderArr.Length; ++otherColliderIndex)
                {
                    Collider _curOtherCollider = _colliderArr[otherColliderIndex];
                    if (_curOtherCollider == _curHitCollider)
                    {
                        _selfHit = true;
                        break;
                    }
                }

                if (_selfHit)
                    continue;

                if (!_hitColliderList.Contains(_curHitCollider))
                    _hitColliderList.Add(_curHitCollider);
            }
        }

        for (int i = 0; i < _colliderArr.Length; ++i)//Test
            if (_hitColliderList.Contains(_colliderArr[i]))
                _hitColliderList.RemoveAt(i--);

        return _hitColliderList.ToArray();
    }

    //
    public static bool BoxOverlaps(this Collider[] _colliderArr)
        => _colliderArr.BoxOverlapsColliderArr().Length != 0;
}
