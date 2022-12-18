using System;
//

//
public static class String_ExtensionMethods
{
    /// <summary> Does this string not equal "N/A"? [just a short form] </summary>
    public static bool IsAvailable(this string _string)
    {
        return _string != "N/A";
    }

    public static bool ContainsIgnoreCase(this string _string, string toCheck)
    {
        return _string?.IndexOf(toCheck, StringComparison.OrdinalIgnoreCase) >= 0;
    }
}
