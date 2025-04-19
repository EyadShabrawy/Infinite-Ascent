using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [Header("Game Over UI")]
    public GameObject gameOverPanel;
    
    public GameOverUI gameOverUI;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        gameOverUI = gameOverPanel.GetComponent<GameOverUI>();
        gameOverPanel.SetActive(false);
    }
    
    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        rb.simulated = false;
        Time.timeScale = 0f;


    }
    
    public void RetryGame()
    {
        Time.timeScale = 1f;

        gameOverUI.Hide();
        gameOverPanel.SetActive(false);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        

        gameOverUI.Hide();
        gameOverPanel.SetActive(false);
        
        SceneManager.LoadScene("Main Menu");
    }
}
