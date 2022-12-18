using System.Collections.Generic;
using UnityEngine;
//

//
public sealed class ClientResponseManager : MonoBehaviour
{
    //---
    public float maxResponseDuration = 3.2f;

    public sealed class ClientResponse
    {
        //---
        public float responseTimer;
        //---

        //
        public ClientResponse()
        {
            responseTimer = ClientResponseManager.instance.maxResponseDuration;
            ClientResponseManager.instance.clientResponseList.Add(this);
        }

        //
        public void OnResponseReceived()
        {
            responseTimer = ClientResponseManager.instance.maxResponseDuration;
        }

        //
        public void OnUpdate()
        {
            MathUtility.ToZeroGradually(ref responseTimer);
        }
    }
    public List<ClientResponse> clientResponseList = new List<ClientResponse>();

    [SerializeField]
    private float sendResponseRate = 0.5f;
    private float sendResponseTimer;

    public static ClientResponseManager instance;
    //---

    //
    private void Awake()
    {
        instance = this;
    }

    //
    public void OnResponseReceived(int _fromClientID, Packet _packet)
    {
        ClientManager.instance.clientList[_fromClientID].clientResponse.OnResponseReceived();
    }

    //
    public void SendResponse(int _toClientID)
    {
        using (Packet _packet = new Packet((int)Packet.ServerPackets.Respond))
        {
            ServerSend.instance.SendData(_toClientID, _packet);
        }
    }

    //
    private void FixedUpdate()
    {
        List<Client> _validClientList = ClientManager.instance.p_validClientList;
        for (int i = 0; i < _validClientList.Count; ++i)
        {
            Client _curValidClient = _validClientList[i];

            _curValidClient.clientResponse.OnUpdate();
            if (_curValidClient.clientResponse.responseTimer == 0.0f)
                _curValidClient.Disconnect("Server received no response from you.");
        }

        MathUtility.ToZeroBySpeed(ref sendResponseTimer, sendResponseRate);
        if (sendResponseTimer == 0.0f)
        {
            for (int i = 0; i < _validClientList.Count; ++i)
                SendResponse(_validClientList[i].id);
            sendResponseTimer = 1.0f;
        }
    }
}
