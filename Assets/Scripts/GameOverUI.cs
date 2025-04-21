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
    
    void Start()
    {
        
        retryButton.onClick.RemoveAllListeners();
        retryButton.onClick.AddListener(() => {gameManager.RetryGame();});
        
        mainMenuButton.onClick.RemoveAllListeners();
        mainMenuButton.onClick.AddListener(() => {gameManager.GoToMainMenu();});
    }
    
    
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    
    public void OnRetryButtonClicked()
    {
        gameManager.RetryGame();

    }
    
    public void OnMainMenuButtonClicked()
    {
        gameManager.GoToMainMenu();

    }
    
    public void UpdateCoinsText(int coins)
    {
        coinsCollectedText.text = "Collected: " + coins.ToString();
    }
}
