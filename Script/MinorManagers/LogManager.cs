using System;
using System.IO;
using UnityEngine;
//

//
public sealed class LogManager : MonoBehaviour
{
    //---
    [SerializeField]
    private string logFilePath = @"D:\LogInfo\Log.txt";

    [SerializeField]
    private bool enableWritingLog = false;

    public enum LogType
    {
        Normal,
        Warning,
        Error,
        Exception
    }

    public static LogManager instance;
    //---

    //
    private void Awake()
    {
        instance = this;
    }

    //
    private void Start()
    {
        InitializeLog();
    }

    //
    private void InitializeLog()
    {
        string _logFileDirectory = logFilePath.Remove(logFilePath.LastIndexOf('\\'));
        if (!Directory.Exists(_logFileDirectory))
            Directory.CreateDirectory(_logFileDirectory);
        if (!File.Exists(logFilePath))
        {
            using (StreamWriter _streamWriter = new StreamWriter(logFilePath))
            {
                _streamWriter.WriteLine("-[Server Logs]-\n");
            }
        }
    }

    //
    public void Log(string _logStr, LogType _logType = LogType.Normal)
    {
        switch (_logType)
        {
            case LogType.Normal:
                Debug.Log(_logStr);
                break;

            case LogType.Warning:
                Debug.LogWarning(_logStr);
                break;

            case LogType.Error:
                Debug.LogError(_logStr);
                break;

            case LogType.Exception:
                Debug.LogException(new Exception(_logStr));
                break;
        }

        if (enableWritingLog)
            using (StreamWriter _streamWriter = new StreamWriter(logFilePath, true))
            {
                _streamWriter.WriteLine("-" + _logType.ToString() + ": " + _logStr + " - " + DateTime.Now.ToString());
            }
    }
}
