using UnityEngine;
using System.Net;
//

//
public sealed class ServerSend : MonoBehaviour
{
    //---
    public static ServerSend instance;
    //---

    //
    private void Awake()
    {
        instance = this;
    }

    //
    public void SendData(IPEndPoint _toIPEndPoint, Packet _packet)
        => Server.instance.SendData(_toIPEndPoint, _packet);

    //
    public void SendData(int _toClientID, Packet _packet)
        => ClientManager.instance.clientList[_toClientID].SendData(_packet);

    //
    public void SendMessageBox(IPEndPoint _toIPEndPoint, string _message, float _duration = 1.5f, bool _displayImmediately = false)
    {
        using (Packet _packet = new Packet((int)Packet.ServerPackets.SendMessageBox))
        {
            _packet.Write(_message);
            _packet.Write(_duration);
            _packet.Write(_displayImmediately);
            SendData(_toIPEndPoint, _packet);
        }
    }

    //
    public void SendMessageBox(int _toClientId, string _message, float _duration)
    {
        using (Packet _packet = new Packet((int)Packet.ServerPackets.SendMessageBox))
        {
            _packet.Write(_message);
            _packet.Write(_duration);
            SendData(_toClientId, _packet);
        }
    }

    //
    public void AllowConnection(int _toClientID)
    {
        using (Packet _packet = new Packet((int)Packet.ServerPackets.AllowConnection))
        {
            _packet.Write(_toClientID);
            SendData(_toClientID, _packet);
        }
        SendMessageBox(_toClientID, "You are now connected to the server.", 1.5f);
    }

    //
    public void SignUpAccepted(int _toClientID)
    {
        using (Packet _packet = new Packet((int)Packet.ServerPackets.SignUpAccepted))
        {
            SendData(_toClientID, _packet);
        }
        SendMessageBox(_toClientID, "Signed-up successfully.", 1.5f);
    }

    //
    public void SignInAccepted(int _toClientID)
    {
        using (Packet _packet = new Packet((int)Packet.ServerPackets.SignInAccepted))
        {
            SendData(_toClientID, _packet);
        }
        SendMessageBox(_toClientID, "Signed-in successfully.", 1.5f);
    }

    //
    public void SendDisconnection(int _toClientID, string _reason = null)
    {
        using (Packet _packet = new Packet((int)Packet.ServerPackets.Disconnect))
        {
            SendData(_toClientID, _packet);
        }
        if (_reason != null)
            SendMessageBox(_toClientID, _reason, 1.5f);
    }
}
