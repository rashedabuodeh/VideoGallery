using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DataManager : MonoBehaviour
{
    #region Private Members 
    [SerializeField] private Button _submitBt;
    [SerializeField] private GameObject _registrationPanel;
    [SerializeField] private GameObject _dragAndDropPanel;
    [SerializeField] private UserInputValidation _inputValidation;
    private static DataManager _instance;
    #endregion

    #region Public Members 
    public TMP_InputField UserNameInputField;
    public TMP_InputField EmailInputField;
    public TextMeshProUGUI UserNameWarningTxt;
    public TextMeshProUGUI EmailWarningTxt;
    public static DataManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<DataManager>();
            }

            return _instance;
        }
        private set { _instance = value; }
    }

    #endregion

    #region Unity CallBacks 
    private void Awake()
    {
        _submitBt.onClick.RemoveAllListeners();
        _submitBt.onClick.AddListener(OnSubmit);
    }

    #endregion

    #region Private Methods

    private void OnSubmit()
    {
        if (UserNameInputField.text == string.Empty)
            Debug.Log("fill the user name ");
        if (EmailInputField.text == string.Empty)
            Debug.Log("fill the email ");

        if (_inputValidation.UsernameIsValid)
        {
            SaveToFile(GetSubmitedUserData());
            _registrationPanel.SetActive(false);
            _dragAndDropPanel.SetActive(true);
        }
    }
    private string GetSubmitedUserData()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine(UserNameInputField.text);
        sb.AppendLine(EmailInputField.text);

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
    #endregion


}
