using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    #region Public Members 

    #endregion

    #region Private Members 

    #endregion

    #region Unity CallBacks 

    #endregion

    #region Private Methods

    #endregion

    #region public Methods

    #endregion

    [SerializeField] private TMP_InputField _userNameInputField;
    [SerializeField] private TMP_InputField _emailInputField;
    [SerializeField] private Button _submitBt;
    [SerializeField] private GameObject _registrationPanel;
    [SerializeField] private GameObject _dragAndDropPanel;

    private void Awake()
    {
        _submitBt.onClick.RemoveAllListeners();
        _submitBt.onClick.AddListener(OnSubmit);
    }

    [ContextMenu("Load scene")]
    private void LoadScene()
    {
        SceneManager.LoadScene(1);
    }
    private void OnSubmit()
    {
        if (_userNameInputField.text == string.Empty)
        {
            Debug.Log("fill the user name ");
            return;
        }
        if (_emailInputField.text == string.Empty)
        {
            Debug.Log("fill the email ");
            return;
        }
        if (GetSubmitedUserData() != string.Empty)
        {
            SaveToFile(GetSubmitedUserData());
            _registrationPanel.SetActive(false);
            _dragAndDropPanel.SetActive(true);
        }
    }
    private string GetSubmitedUserData()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine(_userNameInputField.text);
        sb.AppendLine(_emailInputField.text);
        if (!Regex.IsMatch(_emailInputField.text, "^([a-zA-Z0-9]{1,20})[@]([a-zA-Z0-9]{2,10})[.]([a-zA-Z0-9]{2,10})$"))
        {
            Debug.Log("error in email name validation ");
            return string.Empty;
        }
        return sb.ToString();
    }

    // Save the user data in a*.CSV file
    private void SaveToFile(string content)
    {
        // The target file path 
#if UNITY_EDITOR
        var folder = Application.streamingAssetsPath;

        if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
#else
    var folder = Application.persistentDataPath;
#endif

        var filePath = Path.Combine(folder, "export.csv");

        if (!File.Exists(filePath))
            File.WriteAllText(filePath, content);
        else
            File.AppendAllText(filePath, content);

        Debug.Log($"CSV file written to \"{filePath}\"");

#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif
    }
}
