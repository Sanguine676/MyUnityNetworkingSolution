using System;
using System.Net;
using UnityEngine;
//

//
public sealed class Client
{
    //---
    public int id;

    public IPEndPoint ipEndPoint;
    public UserAccountManager.UserAccountInfo userAccountInfo;
    public ClientResponseManager.ClientResponse clientResponse;

    public bool isConnected;

    public Room enteredRoom;
    //---

    //
    public Client(int _clientID)
    {
        id = _clientID;
    }

    //
    public bool p_isValid => ipEndPoint != null;

    //
    public bool p_isInRoom => enteredRoom != null;

    //
    public string p_username => userAccountInfo != null ? userAccountInfo.p_username : "-NA-";

    //
    public string p_password => userAccountInfo != null ? userAccountInfo.p_password : "-NA-";

    //
    public string p_usernameAndID => "(USERNAME: " + p_username + ", ID: " + id + ")";

    //
    public string p_usernameAndIDAndIP => "(USERNAME: " + p_username + ", ID: " + id + ", IP: " + ipEndPoint +")";

    //
    public string p_usernameAndPasswordAndIDAndIP => "(USERNAME: " + p_username + ", PASSWORD: " + p_password + ", ID: " + id + ", IP: " + ipEndPoint + ")";

    //
    public void ConnectToServer(IPEndPoint _ipEndPoint, UserAccountManager.UserAccountInfo _userAccountInfo)
    {
        ipEndPoint = _ipEndPoint;
        userAccountInfo = _userAccountInfo;
        clientResponse = new ClientResponseManager.ClientResponse();

        isConnected = true;
    }

    //
    public void Sign(UserAccountManager.UserAccountInfo _userAccountInfo)
    {
        userAccountInfo = _userAccountInfo;
    }

    //
    public void EnterRoom(Room _room)
    {
        enteredRoom = _room;
    }

    //
    public void SendData(Packet _packet)
    {
        Server.instance.SendData(ipEndPoint, _packet);
    }

    //
    public void HandleData(int _packetID, Packet _packet)
    {
        try
        {
            ThreadManager.ExecuteOnMainThread(() =>
            {
                Server.instance.packetHandlerDic[_packetID](id, _packet);
            }
            );
        }
        catch (Exception _ex)
        {
            Debug.Log("Error handling data for client " + p_username + ": " + _ex);
        }
    }

    //
    public void Disconnect(string _reason = null)
    {
        if (!isConnected)
            return;

        Debug.Log(ipEndPoint + " " + p_username + " has disconnected, reason: " + _reason);

        if (_reason != null)
            ServerSend.instance.SendMessageBox(id, "You were disconnected: " + _reason, 1.5f);
        ServerSend.instance.SendDisconnection(id);

        if (enteredRoom != null && enteredRoom.hostClient == this)
            enteredRoom.Close();

        ipEndPoint = null;
        userAccountInfo = null;
        clientResponse = null;

        enteredRoom = null;

        isConnected = false;
    }
}
