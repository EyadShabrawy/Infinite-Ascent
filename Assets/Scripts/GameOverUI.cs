using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI gameOverText;
    public Button retryButton;
    public Button mainMenuButton;
    
    
    [Header("References")]
    public GameManager gameManager;
    
    void Start()
    {
        if (gameManager == null)
        {
            gameManager = GameManager.Instance;
        }
        
        if (retryButton != null)
        {
            retryButton.onClick.RemoveAllListeners();
            retryButton.onClick.AddListener(() => {gameManager.RetryGame();});
        }

        
        if (mainMenuButton != null)
        {
            mainMenuButton.onClick.RemoveAllListeners();
            mainMenuButton.onClick.AddListener(() => {gameManager.GoToMainMenu();});
        }
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
}
