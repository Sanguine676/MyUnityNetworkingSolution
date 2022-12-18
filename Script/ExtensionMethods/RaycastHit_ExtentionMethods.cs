using UnityEngine;
//

//
public static class RaycastHit_ExtentionMethods
{
    //Avoid hitInfo.transform, returns the root if the root game object has rigidbody

    //
    public static int GetLayer(this RaycastHit raycastHit)
    {
        return raycastHit.collider.gameObject.layer;
    }

    //
    public static GameObject GetObject(this RaycastHit raycastHit)
    {
        return raycastHit.collider.gameObject;
    }

    //
    public static Transform GetRoot(this RaycastHit raycastHit)
    {
        return raycastHit.transform.root;
    }

    //
    public static GameObject GetRootObj(this RaycastHit raycastHit)
    {
        return raycastHit.GetRoot().gameObject;
    }

    //
    public static int GetRootLayer(this RaycastHit raycastHit)
    {
        return raycastHit.GetRootObj().layer;
    }
}
