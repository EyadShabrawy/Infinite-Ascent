using UnityEngine;

public class UserManager : MonoBehaviour
{
    public static UserManager Instance { get; private set; }

    public int UserId { get; private set; }
    public string Username { get; private set; }
    public int Coins { get; private set; }
    public int ArmorLevel { get; private set; }
    public int DamageLevel { get; private set; }
    public int SpeedLevel { get; private set; }

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
        ArmorLevel = PlayerPrefs.GetInt("ArmorLevel", 1);
        DamageLevel = PlayerPrefs.GetInt("DamageLevel", 1);
        SpeedLevel = PlayerPrefs.GetInt("SpeedLevel", 1);
    }

    void SaveUserData()
    {
        PlayerPrefs.SetInt("UserId", UserId);
        PlayerPrefs.SetString("Username", Username);
        PlayerPrefs.SetInt("Coins", Coins);
        PlayerPrefs.SetInt("ArmorLevel", ArmorLevel);
        PlayerPrefs.SetInt("DamageLevel", DamageLevel);
        PlayerPrefs.SetInt("SpeedLevel", SpeedLevel);
        PlayerPrefs.Save();
    }

    public void SetUserData(int id, string username, int coins = 0, int armorLevel = 1, int damageLevel = 1, int speedLevel = 1)
    {
        UserId = id;
        Username = username;
        Coins = coins;
        ArmorLevel = armorLevel;
        DamageLevel = damageLevel;
        SpeedLevel = speedLevel;
        SaveUserData();
    }

    public void UpdateArmorLevel(int level)
    {
        ArmorLevel = Mathf.Clamp(level, 1, 5);
        SaveUserData();
        
        NetworkManager.Instance?.UpdateArmorLevel(UserId, ArmorLevel, null);
    }

    public void UpdateDamageLevel(int level)
    {
        DamageLevel = Mathf.Clamp(level, 1, 5);
        SaveUserData();
        
        NetworkManager.Instance?.UpdateDamageLevel(UserId, DamageLevel, null);
    }

    public void UpdateSpeedLevel(int level)
    {
        SpeedLevel = Mathf.Clamp(level, 1, 5);
        SaveUserData();
        
        NetworkManager.Instance?.UpdateSpeedLevel(UserId, SpeedLevel, null);
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
