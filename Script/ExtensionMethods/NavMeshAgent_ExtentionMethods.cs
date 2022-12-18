using UnityEngine;
using UnityEngine.AI;
//

//
public static class NavMeshAgent_ExtentionMethods
{
    //
    public static float GetPathDistanceToPoint(this NavMeshAgent _navMeshAgent, Vector3 _toPoint)
    {
        if (_navMeshAgent.path != null)
        {
            Vector3[] _cornerArr = new Vector3[byte.MaxValue];

            byte _cornerCount = (byte)_navMeshAgent.path.GetCornersNonAlloc(_cornerArr);
            float _pathDist = 0.0f;
            Vector3 _previousCorner = _cornerArr[0];
            for (byte i = 0; i < _cornerCount; ++i)
            {
                _pathDist += (_cornerArr[i] - _previousCorner).magnitude;
                _previousCorner = _cornerArr[i];
            }
            _pathDist += (_toPoint - _previousCorner).magnitude;

            return _pathDist;
        }

        return float.PositiveInfinity;
    }

    //
    public static float GetPathDistance(this NavMeshAgent _navMeshAgent)
    {
        if (_navMeshAgent.path != null)
        {
            Vector3[] _cornerArr = new Vector3[byte.MaxValue];

            byte _cornerCount = (byte)_navMeshAgent.path.GetCornersNonAlloc(_cornerArr);
            float _pathDist = 0.0f;
            Vector3 _previousCorner = _cornerArr[0];
            for (byte i = 0; i < _cornerCount; ++i)
            {
                _pathDist += (_cornerArr[i] - _previousCorner).magnitude;
                _previousCorner = _cornerArr[i];
            }

            return _pathDist;
        }

        return float.PositiveInfinity;
    }

    //
    public static bool PathIsAllReady(this NavMeshAgent _navMeshAgent)
    {
        return _navMeshAgent.hasPath && !_navMeshAgent.pathPending && _navMeshAgent.pathStatus == NavMeshPathStatus.PathComplete && !_navMeshAgent.isPathStale;
    }

    //
    public static NavMeshHit FindClosestEdge(this NavMeshAgent _navMeshAgent)
    {
        NavMeshHit _navMeshHit;
        _navMeshAgent.FindClosestEdge(out _navMeshHit);

        return _navMeshHit;
    }

    //
    public static NavMeshHit SamplePathPosition(this NavMeshAgent _navMeshAgent)
    {
        NavMeshHit _navMeshHit;
        _navMeshAgent.SamplePathPosition(_navMeshAgent.areaMask, float.PositiveInfinity, out _navMeshHit);

        return _navMeshHit;
    }

    //
    public static Vector3 SimulatePointOnNavMesh(this NavMeshAgent _navMeshAgent, Vector3 _point)
    {
        NavMeshHit _navMeshHit;
        if (NavMesh.SamplePosition(_point, out _navMeshHit, float.PositiveInfinity, _navMeshAgent.areaMask))
            return _navMeshHit.position;
        else
            return Vector3.positiveInfinity;
    }

    //
    public static float GetDistanceTo(this NavMeshAgent _navMeshAgent, Vector3 _toPoint)
    {
        NavMeshHit _navMeshHit;
        NavMesh.SamplePosition(_toPoint, out _navMeshHit, float.PositiveInfinity, _navMeshAgent.areaMask);

        return (_toPoint - _navMeshAgent.nextPosition).magnitude;
    }
}
