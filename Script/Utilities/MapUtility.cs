using UnityEngine;
//

//
public static class MapUtility
{
    //
    public static RaycastHit SimulateRaycastHitOnPlane(Vector3 _point)
    {
        RaycastHit _downwardRaycastHit;
        bool _canHitDownward = Physics.Raycast
            (_point, Vector3.down, out _downwardRaycastHit, float.PositiveInfinity, Physics.AllLayers, QueryTriggerInteraction.Ignore);

        RaycastHit _resultRaycastHit = new RaycastHit();
        _resultRaycastHit.point = Vector3.positiveInfinity;

        if (_canHitDownward)
            _resultRaycastHit = _downwardRaycastHit;
        else
        {
            RaycastHit[] _raycastHitArr = new RaycastHit[2];
            int _raycastHitCount = Physics.RaycastNonAlloc
                (_point, Vector3.up, _raycastHitArr, float.PositiveInfinity, Physics.AllLayers, QueryTriggerInteraction.Ignore);

            Vector3 _newOrigin = Vector3.positiveInfinity;
            if (_raycastHitCount == 1)
                _newOrigin = _raycastHitArr[0].point + Vector3.up * 40.0f;
            else if (_raycastHitCount == 2)
                _newOrigin = _raycastHitArr[1].point + Vector3.down * 0.2f;

            RaycastHit _newRaycastHit;
            bool _canHit = Physics.Raycast(_newOrigin, Vector3.down, out _newRaycastHit, float.PositiveInfinity, Physics.AllLayers, QueryTriggerInteraction.Ignore);
            if (_canHit)
                _resultRaycastHit = _newRaycastHit;
        }

        return _resultRaycastHit;
    }

    //
    public static Vector3 SimulatePointOnPlane(Vector3 _point)
        => SimulateRaycastHitOnPlane(_point).point;
}
