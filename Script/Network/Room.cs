using System.Collections.Generic;
using UnityEngine;
//

//
public sealed class Room
{
    //---
    [HideInInspector]
    public int id;
    [HideInInspector]
    public string name;
    [HideInInspector]
    public string password;

    public Client hostClient;
    public List<Client> clientList = new List<Client>();

    public enum Types
    {
        None = -1,
        Coop = 1,
        PVP
    }
    public Types type = Types.None;
    //---

    //
    public bool p_isValid => type != Types.None;

    //
    public Room(int _roomID)
    {
        id = _roomID;
    }

    //
    public void Open(string _name, string _password, Client _hostClient, Types _type)
    {
        name = _name;
        password = _password;
        hostClient = _hostClient;
        type = _type;
    }

    //
    public void AllowClientToEnter(Client _client)
    {
        clientList.Add(_client);
    }

    //
    public void SendDataToClient(int _toClientID, Packet _packet)
    {
        for (int i = 0; i < clientList.Count; ++i)
        {
            if (clientList[i].id == _toClientID)
                ServerSend.instance.SendData(_toClientID, _packet);
        }
    }

    //
    public void SendDataToAllClients(Packet _packet)
    {
        for (int i = 0; i < clientList.Count; ++i)
            ServerSend.instance.SendData(clientList[i].id, _packet);
    }

    //
    public void SendDataToAllClientsExcept(int _exceptClientID, Packet _packet)
    {
        for (int i = 0; i < clientList.Count; ++i)
        {
            Client _curClient = clientList[i];
            if (_curClient.id != _exceptClientID)
                ServerSend.instance.SendData(_curClient.id, _packet);
        }
    }

    //
    public void Close()
    {
        name = string.Empty;
        password = string.Empty;
        hostClient = null;
        clientList.Clear();
        type = Types.None;
    }
}
