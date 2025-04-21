using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [Header("Managers")]
    public FuelManager fuelManager;
    public VideoCutsceneManager videoCutsceneManager;
    
    private bool isTransitioning = false;
    
    void Start()
    {
        ConnectEvents();
    }
    
    void ConnectEvents()
    {
        fuelManager.onFuelEmpty.AddListener(videoCutsceneManager.PlayCutscene);
        
        videoCutsceneManager.onVideoComplete.AddListener(TransitionToScene);
    }
    
    public void TriggerFuelEmpty()
    {
        fuelManager.ConsumeFuel(fuelManager.currentFuel);
    }
    
    public void TransitionToScene()
    {
        if (isTransitioning) return;
        
        isTransitioning = true;
        SceneManager.LoadScene("Boss Fight");
    }
}
