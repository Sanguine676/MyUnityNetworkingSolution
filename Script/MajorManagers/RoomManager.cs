using System.Collections.Generic;
using UnityEngine;
//

//
public sealed class RoomManager : MonoBehaviour
{
    //---
    [SerializeField]
    private byte maxRoomCount = 64;

    public List<Room> roomList = new List<Room>();

    public static RoomManager instance;
    //---

    //
    public List<Room> p_validRoomList
    {
        get
        {
            List<Room> _validRoomList = new List<Room>();
            for (int i = 0; i < roomList.Count; ++i)
            {
                Room _curRoom = roomList[i];
                if (_curRoom.p_isValid)
                    _validRoomList.Add(_curRoom);
            }

            return _validRoomList;
        }
    }

    //
    public List<Room> p_invalidRoomList
    {
        get
        {
            List<Room> _invalidRoomList = new List<Room>();
            for (int i = 0; i < roomList.Count; ++i)
            {
                Room _curRoom = roomList[i];
                if (!_curRoom.p_isValid)
                    _invalidRoomList.Add(_curRoom);
            }

            return _invalidRoomList;
        }
    }

    //
    public int p_curValidRoomCount => p_validRoomList.Count;

    //
    public bool p_serverIsFullOfRooms => p_curValidRoomCount == maxRoomCount;

    //
    private void Awake()
    {
        instance = this;
    }

    //
    public void InitializeRoomData()
    {
        for (int i = 0; i < maxRoomCount; ++i)
            roomList.Add(new Room(i));
    }

    //
    public Room GetRoom(string _name)
    {
        for (int i = 0; i < roomList.Count; ++i)
            if (roomList[i].name == _name)
                return roomList[i];

        return null;
    }

    //
    public Room GetRoom(string _name, string _password)
    {
        Room _room = GetRoom(_name);
        if (_room != null && _room.password == _password)
            return _room;

        return null;
    }

    //
    public bool RoomExists(string _name)
        => GetRoom(_name) != null;

    //
    public bool RoomExists(string _name, string _password)
        => GetRoom(_name, _password) != null;

    //
    public void CloseAllRooms(string _reason = null)
    {
        List<Room> _validRoomList = p_validRoomList;
        for (int i = 0; i < _validRoomList.Count; ++i)
            _validRoomList[i].Close();
    }
}
