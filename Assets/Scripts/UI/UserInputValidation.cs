using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.EventSystems;

public class UserInputValidation : MonoBehaviour
{
    #region Public Members 
    public bool UsernameIsValid { get; private set; }
    public bool EmailIsValid { get; private set; }

    #endregion

    #region Private Members 

    #endregion

    #region Unity CallBacks 

    #endregion

    #region Private Methods

    #endregion

    #region public Methods

    #endregion

    private void Awake()
    {
        DataManager.Instance.UserNameInputField.onEndEdit.AddListener(CheckUsernameValidation);
        DataManager.Instance.EmailInputField.onEndEdit.AddListener(CheckEmailValidation);
    }
    private void CheckEmailValidation(string input)
    {
        if (!Regex.IsMatch(input, "^([a-zA-Z0-9]{1,20})[@]([a-zA-Z0-9]{2,10})[.]([a-zA-Z0-9]{2,10})$"))
        {
            if(input == string.Empty)
                SetEmailWarning("This is required field ");
            else
                SetEmailWarning("Please enter valid email (i.e., name@company.com)");
            EmailIsValid = false;
        }
        else
            EmailIsValid = true;
    }
    private void SetEmailWarning(string warning)
    {
        CancelInvoke(nameof(ClearEmailWarning));
        DataManager.Instance.EmailWarningTxt.text = warning;
        Invoke(nameof(ClearEmailWarning), 3f);
    }
    private void ClearEmailWarning()
    {
        DataManager.Instance.EmailWarningTxt.text = string.Empty;
    }


    private void CheckUsernameValidation(string input)
    {
        if (!Regex.IsMatch(input, "^([a-zA-Z]{1,1})([a-zA-Z0-9-._]{2,12})$")  )
        {
            if(input == string.Empty)
                SetUsernameWarning("This is required field ");
            else
                SetUsernameWarning(" Please enter valid usernamer ");
            UsernameIsValid = false;
        }
        else
            UsernameIsValid = true;
    }
    private void SetUsernameWarning(string warning)
    {
        CancelInvoke(nameof(ClearUsernameWarning));
        DataManager.Instance.UserNameWarningTxt.text = warning;
        Invoke(nameof(ClearUsernameWarning), 3f);
    }
    private void ClearUsernameWarning()
    {
        DataManager.Instance.UserNameWarningTxt.text = string.Empty;
    }
}
