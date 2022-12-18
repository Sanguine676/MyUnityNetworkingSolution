using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
//

//
public sealed class Server : MonoBehaviour
{
    //---
    [SerializeField]
    private uint port = 587;

    private UdpClient udpClient;
    [HideInInspector]
    public bool protocolIsOpen;

    public delegate void PacketHandler(int _fromClientID, Packet _packet);
    public Dictionary<int, PacketHandler> packetHandlerDic;

    [HideInInspector]
    public bool serverIsRunning;

    public delegate void ServerHandler();

    public static Server instance;
    //---

    //
    private void Awake()
    {
        instance = this;
    }

    //
    public void StartServer()
    {
        InitializeServerData();

        udpClient = new UdpClient((int)port);

        //avoid SocketException after a client disconnects
        int SIO_UDP_CONNRESET = -1744830452;
        udpClient.Client.IOControl((IOControlCode)SIO_UDP_CONNRESET, new byte[] { 0, 0, 0, 0 }, null);

        udpClient.BeginReceive(OnReceivedData, null);
        protocolIsOpen = true;

        serverIsRunning = true;

        LogManager.instance.Log("The server has started on port: " + port);
    }

    //
    private void OnReceivedData(IAsyncResult _iAsyncResult)
    {
        try
        {
            //avoid System.ObjectDisposedException on StopServer
            if (!protocolIsOpen)
                return;

            IPEndPoint _fromIPEndPoint = null;
            byte[] _data = udpClient.EndReceive(_iAsyncResult, ref _fromIPEndPoint);
            udpClient.BeginReceive(OnReceivedData, null);

            string _fromIPEndPointStr = _fromIPEndPoint.ToString();
            if (_data.Length < 4)
            {
                LogManager.instance.Log("Not enough data received from " + _fromIPEndPointStr + " to process.");
                return;
            }

            //No "using", _packet used in another scope
            Packet _packet = new Packet(_data);
            int _packetID = _packet.ReadInt();
            if (_packetID == (int)Packet.ClientPackets.RequestSign)
            {
                LogManager.instance.Log("Incoming connection from " + _fromIPEndPointStr + "...");

                string[] _usernameAndPasswordAndSignStringArr = _packet.ReadString().Split('_');
                if (_usernameAndPasswordAndSignStringArr.Length != 3)
                {
                    ServerSend.instance.SendMessageBox(_fromIPEndPoint, "Connection failed: Invalid packet length.");
                    LogManager.instance.Log(_fromIPEndPoint + " failed connecting: not enough string array length for signing.");
                    return;
                }

                if (ClientManager.instance.p_serverIsFullOfClients)
                {
                    ServerSend.instance.SendMessageBox(_fromIPEndPoint, "Server is full of clients, try again later.");
                    LogManager.instance.Log(_fromIPEndPoint + " failed to connect: Server is full of clients.");
                    return;
                }

                Client _invalidClient = ClientManager.instance.p_invalidClientList[0];
                Client _newClient = new Client(_invalidClient.id);
                _newClient.ConnectToServer(_fromIPEndPoint, null);
                ClientManager.instance.clientList[_invalidClient.id] = _newClient;

                ServerSend.instance.AllowConnection(_newClient.id);

                LogManager.instance.Log(_fromIPEndPointStr + " is now connected as a client.");

                string _username = _usernameAndPasswordAndSignStringArr[0];
                string _password = _usernameAndPasswordAndSignStringArr[1];
                string _signString = _usernameAndPasswordAndSignStringArr[2];
                if (_signString == "SignIn")
                {
                    if (ClientManager.instance.ClientExists(_username))
                    {
                        _newClient.Disconnect("Account is already in use.");
                        LogManager.instance.Log(_fromIPEndPointStr + " failed to sign-in: Account already in use.");
                        return;
                    }

                    UserAccountManager.UserAccountInfo _userAccountInfo = UserAccountManager.instance.GetUserAccountInfo(_username, _password);
                    if (_userAccountInfo == null)
                    {
                        _newClient.Disconnect("Wrong username or password.");
                        LogManager.instance.Log(_fromIPEndPointStr + " failed to sign-in: Wrong username or password.");
                        return;
                    }

                    _newClient.Sign(_userAccountInfo);
                    ServerSend.instance.SignInAccepted(_newClient.id);
                    LogManager.instance.Log(_newClient.p_usernameAndPasswordAndIDAndIP + " successfully signed-in.");
                }
                else if (_signString == "SignUp")
                {
                    if (UserAccountManager.instance.UserAccountExists(_username))
                    {
                        _newClient.Disconnect("Username already exists.");
                        LogManager.instance.Log(_fromIPEndPointStr + " failed to sign-up: Username already exists.");
                        return;
                    }

                    UserAccountManager.UserAccountInfo _userAccountInfo = UserAccountManager.instance.CreateUserAccount(_username, _password);
                    _newClient.Sign(_userAccountInfo);
                    ServerSend.instance.SignUpAccepted(_newClient.id);
                    LogManager.instance.Log(_newClient.p_usernameAndPasswordAndIDAndIP + " successfully signed-up.");
                }
                else
                {
                    _newClient.Disconnect("Incorrect sign string.");
                    LogManager.instance.Log(_fromIPEndPointStr + " failed signing: incorrect sign flag.");
                    return;
                }
            }
            else
            {
                int _fromClientID = _packet.ReadInt();
                if (!ClientManager.instance.ClientIsValid(_fromClientID))
                {
                    LogManager.instance.Log(_fromIPEndPointStr + " sent packet with an invalid client ID: " + _fromClientID);
                    return;
                }

                ClientManager.instance.clientList[_fromClientID].HandleData(_packetID, _packet);
            }
        }
        catch (Exception _ex)
        {
            LogManager.instance.Log("Error receiving data: " + _ex);
        }
    }

    //
    public void SendData(IPEndPoint _toIPEndPoint, Packet _packet)
    {
        try
        {
            udpClient.BeginSend(_packet.ToArray(), _packet.Length(), _toIPEndPoint, null, null);
        }
        catch (Exception _ex)
        {
            LogManager.instance.Log("Error sending data to " + _toIPEndPoint + ": " + _ex);
        }
    }

    //
    private void InitializeServerData()
    {
        ClientManager.instance.InitializeClientData();
        RoomManager.instance.InitializeRoomData();

        packetHandlerDic = new Dictionary<int, PacketHandler>()
        {
            { (int)Packet.ClientPackets.Respond, ClientResponseManager.instance.OnResponseReceived },
            { (int)Packet.ClientPackets.Disconnect, ServerHandle.instance.OnDisconnectionReceived },
        };
    }

    //
    public void StopServer(string _reason = null)
    {
        if (!serverIsRunning)
            return;

        ClientManager.instance.DisconnectAllClients(_reason);
        RoomManager.instance.CloseAllRooms(_reason);

        udpClient.Close();
        protocolIsOpen = false;

        serverIsRunning = false;

        LogManager.instance.Log("The server was stopped.");
    }
}
