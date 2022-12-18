using UnityEngine;
using UnityEngine.AI;
//

//
public static class NavMeshUtility
{
    //
    public static NavMeshHit SimulateHitOnNavMesh(Vector3 _point, int _areaMask)
    {
        NavMeshHit _navMeshHit;
        bool _simulated = NavMesh.SamplePosition(_point, out _navMeshHit, float.PositiveInfinity, _areaMask);
        if (!_simulated) _navMeshHit.distance = float.PositiveInfinity;

        return _navMeshHit;
    }

    //
    public static NavMeshHit SimulateHitOnNavMesh(Vector3 _point)
    {
        return SimulateHitOnNavMesh(_point, NavMesh.AllAreas);
    }

    //
    public static Vector3 SimulatePointOnNavMesh(Vector3 _point, int _areaMask)
    {
        NavMeshHit _navMeshHit = SimulateHitOnNavMesh(_point, _areaMask);

        if (float.IsPositiveInfinity(_navMeshHit.distance)) return Vector3.positiveInfinity;
        else return _navMeshHit.position;
    }

    //
    public static Vector3 SimulatePointOnNavMesh(Vector3 _point)
    {
        NavMeshHit _navMeshHit = SimulateHitOnNavMesh(_point);

        if (float.IsPositiveInfinity(_navMeshHit.distance)) return Vector3.positiveInfinity;
        else return _navMeshHit.position;
    }

    //
    public static float GetPathDistance(Vector3 _fromPoint, Vector3 _toPoint, int _areaMask)
    {
        Vector3 _fromPointOnNavMesh = SimulatePointOnNavMesh(_fromPoint, _areaMask);
        Vector3 _toPointOnNavMesh = SimulatePointOnNavMesh(_toPoint, _areaMask);

        if (!_fromPointOnNavMesh.IsValid() || !_toPointOnNavMesh.IsValid())
            return float.PositiveInfinity;

        NavMeshPath _navMeshPath = new NavMeshPath();
        if (!NavMesh.CalculatePath(_fromPointOnNavMesh, _toPointOnNavMesh, _areaMask, _navMeshPath) || _navMeshPath.status != NavMeshPathStatus.PathComplete)
            return float.PositiveInfinity;

        Vector3[] _cornerArr = new Vector3[byte.MaxValue];

        byte _cornerCount = (byte)_navMeshPath.GetCornersNonAlloc(_cornerArr);
        float _pathDist = 0.0f;
        Vector3 _previousCorner = _cornerArr[0];
        for (byte i = 0; i < _cornerCount; ++i)
        {
            _pathDist += (_cornerArr[i] - _previousCorner).magnitude;
            _previousCorner = _cornerArr[i];
        }

        return _pathDist;
    }
}
