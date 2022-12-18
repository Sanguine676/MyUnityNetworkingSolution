using System;
using System.IO;
//

//
public static class FileInfo_ExtensionMethods
{
    /// <summary> Returns the name of the file to which the FileInfo instance is dedicated with out its extension at the end. </summary>
    public static string NameWithOutExtension(this FileInfo _fileInfo)
    {
        return Path.GetFileNameWithoutExtension(_fileInfo.Name);
    }

    /// <summary> Returns the full name of the file to which the FileInfo instance is dedicated with out its extension at the end. </summary>
    public static string FullNameWithOutExtension(this FileInfo _fileInfo)
    {
        return Path.GetFileNameWithoutExtension(_fileInfo.FullName);
    }


    /// <summary> Returns whether the file is in any use or not, with the maximum seconds duration of check given. </summary>
    public static bool FileIsLocked(this FileInfo _fileInfo, float _maxCheckDuration = 0.01f)
    {
        try
        {
            DateTime _dateTimeUTCNow = DateTime.UtcNow;

            while ((DateTime.UtcNow - _dateTimeUTCNow).TotalMilliseconds <= _maxCheckDuration)
                using (FileStream _fileStream = _fileInfo.Open(FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    _fileStream.Close();
                }
        }
        catch (IOException)
        {
            return true;
        }

        return false;
    }
}
