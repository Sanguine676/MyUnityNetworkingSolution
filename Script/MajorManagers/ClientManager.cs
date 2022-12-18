using System.Collections.Generic;
using UnityEngine;
//

//
public sealed class ClientManager : MonoBehaviour
{
    //---
    [SerializeField]
    private ushort maxClientCount = 1024;
    public List<Client> clientList = new List<Client>();

    public static ClientManager instance;
    //---

    //
    public List<Client> p_validClientList
    {
        get
        {
            List<Client> _validClientList = new List<Client>();
            for (int i = 0; i < clientList.Count; ++i)
            {
                Client _curClient = clientList[i];
                if (_curClient.p_isValid)
                    _validClientList.Add(_curClient);
            }

            return _validClientList;
        }
    }

    //
    public List<Client> p_invalidClientList
    {
        get
        {
            List<Client> _invalidClientList = new List<Client>();
            for (int i = 0; i < clientList.Count; ++i)
            {
                Client _curClient = clientList[i];
                if (!_curClient.p_isValid)
                    _invalidClientList.Add(_curClient);
            }

            return _invalidClientList;
        }
    }

    //
    public int p_curValidClientCount => p_validClientList.Count;

    //
    public bool p_serverIsFullOfClients => p_curValidClientCount == maxClientCount;

    //
    private void Awake()
    {
        instance = this;
    }

    //
    public void InitializeClientData()
    {
        for (int i = 0; i < maxClientCount; ++i)
            clientList.Add(new Client(i));
    }

    //
    public bool ClientIsValid(int _id)
    {
        List<Client> _validClientList = p_validClientList;
        for (int i = 0; i < _validClientList.Count; ++i)
            if (_validClientList[i].id == _id)
                return true;

        return false;
    }

    //
    public bool ClientExists(string _username)
    {
        List<Client> _validClientList = p_validClientList;
        for (int i = 0; i < _validClientList.Count; ++i)
        {
            if (_validClientList[i].userAccountInfo == null)
                continue;

            if (_validClientList[i].userAccountInfo.p_username == _username)
                return true;
        }

        return false;
    }

    //
    public void DisconnectAllClients(string _reason = null)
    {
        List<Client> _validClientList = p_validClientList;
        for (int i = 0; i < _validClientList.Count; ++i)
            _validClientList[i].Disconnect(_reason);
    }
}
