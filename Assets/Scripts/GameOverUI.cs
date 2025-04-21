using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    [Header("UI Elements")]
    public Button retryButton;
    public Button mainMenuButton;
    public TextMeshProUGUI coinsCollectedText;
    
    
    [Header("References")]
    public GameManager gameManager;
    private BossFightManager bossFightManager;
    
    void Start()
    {
        bossFightManager = FindObjectOfType<BossFightManager>();
    }
    
    
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    
    public void OnRetryButtonClicked()
    {
        if (gameManager != null)
        {
            gameManager.RetryGame();
        }
    }
    
    public void OnMainMenuButtonClicked()
    {
        if (gameManager != null)
        {
            gameManager.GoToMainMenu();
        }
            
        else
        {
            bossFightManager.ReturnToMainMenu();
        }
            
    }
    
    public void UpdateCoinsText(int coins)
    {
        coinsCollectedText.text = "Collected: " + coins.ToString();
    }
}
