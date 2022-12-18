using System.Collections.Generic;
using System.IO;
using UnityEngine;
//

//
public sealed class UserAccountManager : MonoBehaviour
{
    //---
    [SerializeField]
    private string userAccountFolderDir = @"D:\UserAccounts\";
    public DirectoryInfo userAccountDirectoryInfo;

    //
    public class UserAccountInfo
    {
        //---
        public FileInfo fileInfo;
        //---

        //
        public UserAccountInfo(FileInfo _fileInfo)
        {
            fileInfo = _fileInfo;
        }

        //
        public string p_username => UserAccountManager.instance.GetFieldValue(fileInfo, "Username");

        //
        public string p_password => UserAccountManager.instance.GetFieldValue(fileInfo, "Password");
    }
    public List<UserAccountInfo> userAccountInfoList;

    public static UserAccountManager instance;
    //---

    //
    private void Awake()
    {
        instance = this;
    }

    //
    private void Start()
    {
        InitializeUserAccount();
        LoadAllUserAccounts();
    }

    //
    private void InitializeUserAccount()
    {
        if (!Directory.Exists(userAccountFolderDir))
            Directory.CreateDirectory(userAccountFolderDir);
        userAccountDirectoryInfo = new DirectoryInfo(userAccountFolderDir);
    }

    //
    public void LoadAllUserAccounts()
    {
        FileInfo[] _userAccountFileInfoArr = userAccountDirectoryInfo.GetFiles();
        List<UserAccountInfo> _userAccountInfoList = new List<UserAccountInfo>();
        for (int i = 0; i < _userAccountFileInfoArr.Length; ++i)
        {
            FileInfo _curUserAccountFileInfo = _userAccountFileInfoArr[i];
            _userAccountInfoList.Add(new UserAccountInfo(_curUserAccountFileInfo));
        }
        userAccountInfoList = _userAccountInfoList;
    }

    //
    public string GetFieldValue(FileInfo _fileInfo, string _fieldName)
    {
        using (StreamReader _streamReader = _fileInfo.OpenText())
        {
            string _curLine;
            while ((_curLine = _streamReader.ReadLine()) != null)
            {
                string _curFieldName = _curLine.Remove(_fieldName.Length);
                if (_curFieldName == _fieldName)
                {
                    string _curFieldValue = _curLine.Remove(0, (_fieldName + ": ").Length);
                    return _curFieldValue;
                }
            }
        }

        return null;
    }

    //
    public UserAccountInfo GetUserAccountInfo(string _username)
    {
        for (int i = 0; i < userAccountInfoList.Count; ++i)
        {
            UserAccountInfo _curUserAccountInfo = userAccountInfoList[i];
            if (_curUserAccountInfo.p_username == _username)
                return _curUserAccountInfo;
        }

        return null;
    }

    public UserAccountInfo GetUserAccountInfo(string _username, string _password)
    {
        UserAccountInfo _userAccountInfo = GetUserAccountInfo(_username);
        if (_userAccountInfo != null && _userAccountInfo.p_password == _password)
            return _userAccountInfo;

        return null;
    }

    //
    public bool UserAccountExists(string _username)
        => GetUserAccountInfo(_username) != null;

    //
    public bool UserAccountExists(string _username, string _password)
        => GetUserAccountInfo(_username, _password) != null;

    //
    public UserAccountInfo CreateUserAccount(string _username, string _password)
    {
        string _userAccountFilePath = userAccountFolderDir + "UserAccount_" + _username + ".txt";
        using (StreamWriter _streamWriter = new StreamWriter(_userAccountFilePath))
        {
            _streamWriter.WriteLine("Username: " + _username);
            _streamWriter.WriteLine("Password: " + _password);
        }
        FileInfo _userAccountFileInfo = new FileInfo(_userAccountFilePath);
        UserAccountInfo _userAccountInfo = new UserAccountInfo(_userAccountFileInfo);

        userAccountInfoList.Add(_userAccountInfo);
        return _userAccountInfo;
    }
}
