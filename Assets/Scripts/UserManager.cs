using UnityEngine;

public class UserManager : MonoBehaviour
{
    public static UserManager Instance { get; private set; }

    public int UserId { get; private set; }
    public string Username { get; private set; }
    public int Coins { get; private set; }

    void Awake()
    {
        if (Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); LoadUserData(); }
        else { Destroy(gameObject); }
    }

    void LoadUserData()
    {
        UserId = PlayerPrefs.GetInt("UserId", 0);
        Username = PlayerPrefs.GetString("Username", "");
        Coins = PlayerPrefs.GetInt("Coins", 0);
    }

    void SaveUserData()
    {
        PlayerPrefs.SetInt("UserId", UserId);
        PlayerPrefs.SetString("Username", Username);
        PlayerPrefs.SetInt("Coins", Coins);
        PlayerPrefs.Save();
    }

    public void SetUserData(int id, string username, int coins = 0)
    {
        UserId = id;
        Username = username;
        Coins = coins;
        SaveUserData();
    }

    public void UpdateCoins(int value, bool isAddition = false)
    {
        Coins = isAddition ? Coins + value : value;
        SaveUserData();
        
        NetworkManager.Instance?.UpdateCoins(UserId, Coins, null);
    }

    public void AddCoins(int amount) => UpdateCoins(amount, true);

    public void Logout()
    {
        UserId = 0;
        Username = "";
        Coins = 0;
        SaveUserData();
    }
}
