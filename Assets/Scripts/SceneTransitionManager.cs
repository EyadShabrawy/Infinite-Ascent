using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    [Header("Transition Settings")]
    public string targetSceneName;
    public float transitionDelay = 1f;
    
    private bool isTransitioning = false;
    
    public void TransitionToScene()
    {
        if (isTransitioning) return;
        
        isTransitioning = true;
        
        StartCoroutine(DelayedSceneLoad());
    }
    
    public void TransitionToScene(string sceneName)
    {
        targetSceneName = sceneName;
        TransitionToScene();
    }
    
    IEnumerator DelayedSceneLoad()
    {
        yield return new WaitForSecondsRealtime(transitionDelay);
        SceneManager.LoadScene(targetSceneName);
    }
}
