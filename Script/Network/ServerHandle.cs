using System.Collections.Generic;
using UnityEngine;
//

/// <summary> A static class containing methods invoked by packets received from clients. </summary>
public sealed class ServerHandle : MonoBehaviour
{
    //---
    public static ServerHandle instance;
    //---

    //
    private void Awake()
    {
        instance = this;
    }

    //
    public void OnDisconnectionReceived(int _fromClientID, Packet _packet)
    {
        string _reason = _packet.ReadString();
        Client _targetClient = ClientManager.instance.clientList[_fromClientID];
        Debug.Log(_targetClient.p_usernameAndPasswordAndIDAndIP + " disconnected: " + _reason);
        _targetClient.Disconnect();
    }
}
