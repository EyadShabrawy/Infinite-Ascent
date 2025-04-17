using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [Header("Managers")]
    public FuelManager fuelManager;
    public VideoCutsceneManager videoCutsceneManager;
    public SceneTransitionManager sceneTransitionManager;
    
    [Header("Scene Transition")]
    public string nextSceneName;
    
    void Start()
    {
        sceneTransitionManager.targetSceneName = nextSceneName;
        
        ConnectEvents();
    }
    
    void ConnectEvents()
    {
        fuelManager.onFuelEmpty.AddListener(videoCutsceneManager.PlayCutscene);
        
        videoCutsceneManager.onVideoComplete.AddListener(sceneTransitionManager.TransitionToScene);
    }
    
    public void TriggerFuelEmpty()
    {
        fuelManager.ConsumeFuel(fuelManager.currentFuel);
    }
}
