/// <summary> A string utility class containing helper fields, properties, and methods. </summary>
public static class StringUtility
{
    //=================================================================================================
    #region Variables
    /// <summary> Short form for "N/A". </summary>
    private const string notAvailable = "N/A";
    #endregion
    //=================================================================================================

    //=================================================================================================
    /// <summary> Checks whether the parameter string is completely in english language. </summary>
    public static bool IsEnglish(string _string)
    {
        for (int i = 0; i < _string.Length; ++i)
        {
            char _curChar = _string[i];
            if (!char.IsLower(_curChar) && !char.IsUpper(_curChar) && !char.IsDigit(_curChar) && !char.IsWhiteSpace(_curChar))
                return false;
        }

        return true;
    }
    //=================================================================================================

    //=================================================================================================
    #region Properties
    /// <summary> Short form for "N/A". </summary>
    public static string NotAvailable => notAvailable;
    #endregion
    //=================================================================================================

    //=================================================================================================
    #region Methods
    //
    public static string ReasonQuote(string _reason) => string.IsNullOrEmpty(_reason) ? string.Empty : (": " + _reason) + ".";
    #endregion
    //=================================================================================================
}
