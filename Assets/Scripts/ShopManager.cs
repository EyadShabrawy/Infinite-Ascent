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
        if (coinText != null && userManager != null)
        {
            coinText.text = userManager.Coins.ToString();
        }
    }
    
    void ShowMessage(string message)
    {
        if (errorText != null)
        {
            if (messageCoroutine != null)
            {
                StopCoroutine(messageCoroutine);
            }
            
            errorText.text = message;
            
            messageCoroutine = StartCoroutine(ClearMessageAfterDelay());
        }
    }
    
    IEnumerator ClearMessageAfterDelay()
    {
        yield return new WaitForSeconds(messageDuration);
        errorText.text = "";
        messageCoroutine = null;
    }
    
    void PurchaseArmorUpgrade()
    {
        if (userManager.Coins >= armorUpgradeCost)
        {
            userManager.UpdateCoins(userManager.Coins - armorUpgradeCost);
            
            // Apply armor upgrade effect
            
            ShowMessage("Successfully purchased armor upgrade!");
            
            UpdateCoinDisplay();
        }
        else
        {
            ShowMessage("Not enough coins for armor upgrade!");
        }
    }
    
    void PurchaseSpeedUpgrade()
    {
        if (userManager.Coins >= speedUpgradeCost)
        {
            userManager.UpdateCoins(userManager.Coins - speedUpgradeCost);
            
            // Apply speed upgrade effect
            
            ShowMessage("Successfully purchased speed upgrade!");
            
            UpdateCoinDisplay();
        }
        else
        {
            ShowMessage("Not enough coins for speed upgrade!");
        }
    }
    
    void PurchaseDamageUpgrade()
    {
        if (userManager.Coins >= damageUpgradeCost)
        {
            userManager.UpdateCoins(userManager.Coins - damageUpgradeCost);
            
            // Apply damage upgrade effect
            
            ShowMessage("Successfully purchased damage upgrade!");
            
            UpdateCoinDisplay();
        }
        else
        {
            ShowMessage("Not enough coins for damage upgrade!");
        }
    }
}
