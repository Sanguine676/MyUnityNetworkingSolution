using UnityEngine;
//

//
public sealed class InvokeManager : MonoBehaviour
{
    //---
    public static InvokeManager instance;
    //---

    //
    private void Awake()
    {
        instance = this;
    }

    //
    private void Start()
    {
        Server.instance.StartServer();
    }

    //
    private void Update()
    {

    }

    //
    private void LateUpdate()
    {

    }

    //
    private void FixedUpdate()
    {
        ThreadManager.UpdateMain();
    }

    //
    private void OnApplicationQuit()
    {
        Server.instance.StopServer();
    }
}
