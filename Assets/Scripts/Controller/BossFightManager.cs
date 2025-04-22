using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossFightManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject gameOverPanel;
    public GameObject victoryPanel;
    
    void Start()
    {
        gameOverPanel.SetActive(false);
        victoryPanel.SetActive(false);
    }
    
    public void PlayerDied()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }
    
    public void BossDefeated()
    {
        victoryPanel.SetActive(true);
        Time.timeScale = 0f;
    }
    
    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }
}
