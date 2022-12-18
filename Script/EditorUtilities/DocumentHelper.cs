using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class DocEnabler : MonoBehaviour
{
    //Replace both with the proper paths on your system

    // .NET 4.x
    static string unityFrameworkPath = @"H:\Softwares\Unity\2021.3.6f1\Editor\Data\MonoBleedingEdge\lib\mono\4.7.1-api";
    static string microsoftFrameworkPath = @"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.X";

    // .NETSTANDARD 2.0
    static string unityNetStandardPath = @"H:\Softwares\Unity\2021.3.6f1\Editor\Data\NetStandard\ref\2.1.0";
    static string microsoftNetStandardPath = @"C:\Program Files\dotnet\packs\NETStandard.Library.Ref\2.1.0\ref\netstandard2.1";

    [MenuItem("Programmer/Enable Core Documentation")]
    static void EnableCoreDoc()
    {
        CopyFilesByExt(microsoftFrameworkPath, unityFrameworkPath, "xml");
        CopyFilesByExt(microsoftNetStandardPath, unityNetStandardPath, "xml");
    }

    [MenuItem("Programmer/Disable Core Documentation")]
    static void DisableCoreDoc()
    {
        DeleteFilesByExt(unityFrameworkPath, "xml");
        DeleteFilesByExt(unityNetStandardPath, "xml");
    }

    static void DeleteFilesByExt(string path, string ext)
    {
        DirectoryInfo dirInfo = new DirectoryInfo(path);
        FileInfo[] files = dirInfo.GetFiles("*." + ext)
          .Where(p => p.Extension == "." + ext).ToArray();

        foreach (FileInfo file in files)
        {
            try
            {
                file.Attributes = FileAttributes.Normal;
                file.Delete();
            }
            catch (Exception e)
            {
                Debug.Log("Error while deleting file: " + file.Name + "\r\n" + e.Message);
            }
        }
        DoneMessage();
    }

    static void CopyFilesByExt(string source, string destPath, string ext)
    {
        DirectoryInfo dirInfo = new DirectoryInfo(source);
        FileInfo[] files = dirInfo.GetFiles("*." + ext)
          .Where(p => p.Extension == "." + ext).ToArray();

        foreach (FileInfo file in files)
        {
            try
            {
                string toPath = Path.Combine(destPath, file.Name);
                file.CopyTo(toPath, true);
            }
            catch (Exception e)
            {
                Debug.Log("Error while Copying file: " + file.Name + "\r\n" + e.Message);
            }
        }
        DoneMessage();
    }

    static void DoneMessage()
    {
        Debug.Log("Action complete. Restart Visual Studio for the changes to take effect");
    }
}
