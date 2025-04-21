using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Proyecto26;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager Instance { get; private set; }
    
    [SerializeField] private string serverUrl = "http://10.10.1.164:3000";
    
    [Header("Authentication UI")]
    public GameObject loginPanel;
    public GameObject signupPanel;
    
    [Header("Login Fields")]
    public TMP_InputField loginUsernameField;
    public TMP_InputField loginPasswordField;
    public Button loginButton;
    
    [Header("Signup Fields")]
    public TMP_InputField signupUsernameField;
    public TMP_InputField signupPasswordField;
    public TMP_InputField confirmPasswordField;
    public Button signupButton;
    
    [Header("Messaging")]
    public TextMeshProUGUI errorText;
    
    [Serializable]
    private class UserModel
    {
        public int id;
        public string username;
        public string password;
        public int coins;
        public int armor_level;
        public int damage_level;
        public int speed_level;
        public string error;
    }
    
    private void Awake()
    {
        if (Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); }
        else { Destroy(gameObject); }
    }
    
    private void Start()
    {
        loginButton.onClick.AddListener(Login);
        signupButton.onClick.AddListener(Register);
        errorText.text = "";
    }
    
    public void Login()
    {
        string username = loginUsernameField.text;
        string password = loginPasswordField.text;
        
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            ShowError("Please enter both username and password");
            return;
        }
        
        LoginUser(username, password, 
            (userId, username, coins, armorLevel, damageLevel, speedLevel) => {
                UserManager.Instance.SetUserData(userId, username, coins, armorLevel, damageLevel, speedLevel);
                ShowError("Login successful!");
                StartCoroutine(LoadSceneAfterDelay("Main Menu", 1.5f));
            },
            (error) => ShowError("Login failed: " + error)
        );
    }
    
    public void Register()
    {
        string username = signupUsernameField.text;
        string password = signupPasswordField.text;
        string confirmPassword = confirmPasswordField.text;
        
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
        {
            ShowError("Please fill in all fields");
            return;
        }
        
        if (password != confirmPassword)
        {
            ShowError("Passwords do not match");
            return;
        }
        
        RegisterUser(username, password,
            (userId, username) => {
                ShowError("Registration successful! Please login.");
                signupPanel.SetActive(false);
                loginPanel.SetActive(true);
            },
            (error) => ShowError("Registration failed: " + error)
        );
    }
    
    public void ShowError(string message)
    {
        errorText.text = message;
        StartCoroutine(ClearErrorAfterDelay(3f));
    }
    
    private string ExtractErrorMessage(Exception err)
    {
        if (err is RequestException reqErr && !string.IsNullOrEmpty(reqErr.Response))
        {
            try
            {
                var errorObj = JsonUtility.FromJson<UserModel>(reqErr.Response);
                if (!string.IsNullOrEmpty(errorObj.error))
                    return errorObj.error;
            }
            catch {}
            return reqErr.Response;
        }
        return err.Message;
    }
    
    public void RegisterUser(string username, string password, Action<int, string> onSuccess, Action<string> onError)
    {
        var request = new UserModel { username = username, password = password };
        
        RestClient.Post<UserModel>(serverUrl + "/register", request)
            .Then(response => onSuccess(response.id, response.username))
            .Catch(err => onError(ExtractErrorMessage(err)));
    }
    
    public void LoginUser(string username, string password, Action<int, string, int, int, int, int> onSuccess, Action<string> onError)
    {
        var request = new UserModel { username = username, password = password };
        
        RestClient.Post<UserModel>(serverUrl + "/login", request)
            .Then(response => onSuccess(response.id, response.username, response.coins, 
                                       response.armor_level, response.damage_level, response.speed_level))
            .Catch(err => onError(ExtractErrorMessage(err)));
    }
    
    public void UpdateCoins(int userId, int coins, Action<bool> callback)
    {
        var request = new UserModel { id = userId, coins = coins };
        
        RestClient.Put(serverUrl + "/update-coins", request)
            .Then(_ => callback?.Invoke(true))
            .Catch(_ => callback?.Invoke(false));
    }
    
    public void UpdateArmorLevel(int userId, int level, Action<bool> callback)
    {
        var request = new UserModel { id = userId, armor_level = level };
        
        RestClient.Put(serverUrl + "/update-armor", request)
            .Then(_ => callback?.Invoke(true))
            .Catch(_ => callback?.Invoke(false));
    }
    
    public void UpdateDamageLevel(int userId, int level, Action<bool> callback)
    {
        var request = new UserModel { id = userId, damage_level = level };
        
        RestClient.Put(serverUrl + "/update-damage", request)
            .Then(_ => callback?.Invoke(true))
            .Catch(_ => callback?.Invoke(false));
    }
    
    public void UpdateSpeedLevel(int userId, int level, Action<bool> callback)
    {
        var request = new UserModel { id = userId, speed_level = level };
        
        RestClient.Put(serverUrl + "/update-speed", request)
            .Then(_ => callback?.Invoke(true))
            .Catch(_ => callback?.Invoke(false));
    }
    
    private IEnumerator ClearErrorAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        errorText.text = "";
    }
    
    private IEnumerator LoadSceneAfterDelay(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}
