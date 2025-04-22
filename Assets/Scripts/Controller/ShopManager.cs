using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class ShopManager : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI errorText;
    public Button armorUpgradeButton;
    public Button speedUpgradeButton;
    public Button damageUpgradeButton;
    
    [Header("Upgrade Costs")]
    public int armorUpgradeCost = 10;
    public int speedUpgradeCost = 10;
    public int damageUpgradeCost = 10;
    
    [Header("Message Settings")]
    public float messageDuration = 3f;
    
    private UserManager userManager;
    private Coroutine messageCoroutine;
    
    void Start()
    {
        userManager = UserManager.Instance;
        
        armorUpgradeButton.onClick.AddListener(PurchaseArmorUpgrade);
        speedUpgradeButton.onClick.AddListener(PurchaseSpeedUpgrade);
        damageUpgradeButton.onClick.AddListener(PurchaseDamageUpgrade);
        
        UpdateCoinDisplay();
    }
    
    void UpdateCoinDisplay()
    {
        coinText.text = userManager.Coins.ToString();
    }
    
    void ShowMessage(string message)
    {
        if (messageCoroutine != null)
        {
            StopCoroutine(messageCoroutine);
        }
        
        errorText.text = message;
        
        messageCoroutine = StartCoroutine(ClearMessageAfterDelay());
    }
    
    IEnumerator ClearMessageAfterDelay()
    {
        yield return new WaitForSeconds(messageDuration);
        errorText.text = "";
        messageCoroutine = null;
    }
    
    void PurchaseArmorUpgrade()
    {
        if (userManager.Coins >= armorUpgradeCost && userManager.ArmorLevel < 5)
        {
            userManager.UpdateCoins(userManager.Coins - armorUpgradeCost);
            userManager.UpdateArmorLevel(userManager.ArmorLevel + 1);
            
            ShowMessage($"Successfully purchased armor upgrade! Level: {userManager.ArmorLevel}");
            
            UpdateCoinDisplay();
        }
        else if (userManager.ArmorLevel >= 5)
        {
            ShowMessage("Armor already at maximum level!");
        }
        else
        {
            ShowMessage("Not enough coins for armor upgrade!");
        }
    }
    
    void PurchaseSpeedUpgrade()
    {
        if (userManager.Coins >= speedUpgradeCost && userManager.SpeedLevel < 5)
        {
            userManager.UpdateCoins(userManager.Coins - speedUpgradeCost);
            userManager.UpdateSpeedLevel(userManager.SpeedLevel + 1);
            
            ShowMessage($"Successfully purchased speed upgrade! Level: {userManager.SpeedLevel}");
            
            UpdateCoinDisplay();
        }
        else if (userManager.SpeedLevel >= 5)
        {
            ShowMessage("Speed already at maximum level!");
        }
        else
        {
            ShowMessage("Not enough coins for speed upgrade!");
        }
    }
    
    void PurchaseDamageUpgrade()
    {
        if (userManager.Coins >= damageUpgradeCost && userManager.DamageLevel < 5)
        {
            userManager.UpdateCoins(userManager.Coins - damageUpgradeCost);
            userManager.UpdateDamageLevel(userManager.DamageLevel + 1);
            
            ShowMessage($"Successfully purchased damage upgrade! Level: {userManager.DamageLevel}");
            
            UpdateCoinDisplay();
        }
        else if (userManager.DamageLevel >= 5)
        {
            ShowMessage("Damage already at maximum level!");
        }
        else
        {
            ShowMessage("Not enough coins for damage upgrade!");
        }
    }
}
