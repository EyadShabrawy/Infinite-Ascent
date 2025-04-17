using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Events;

public class VideoCutsceneManager : MonoBehaviour
{
    [Header("Video Settings")]
    public VideoPlayer videoPlayer;
    public string videoFileName;
    public RenderTexture renderTexture;

    
    [Header("UI References")]
    public GameObject videoCutscenePanel;
    
    [Header("Events")]
    public UnityEvent onVideoStart;
    public UnityEvent onVideoComplete;
    
    private bool isPlaying = false;
    
    void Start()
    {
        SetupVideoPlayer();
        
        videoCutscenePanel.SetActive(false);
        
        videoPlayer.loopPointReached += OnVideoComplete;
    }
    
    void SetupVideoPlayer()
    {
        string videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, videoFileName);
        videoPlayer.url = videoPath;
        
        videoPlayer.targetTexture = renderTexture;
        
        videoPlayer.playOnAwake = false;
        videoPlayer.waitForFirstFrame = true;
        videoPlayer.isLooping = false;
    }
    
    public void PlayCutscene()
    {
        if (isPlaying) return;
        
        StartCoroutine(PlayVideoSequence());
    }
    
    private IEnumerator PlayVideoSequence()
    {
        videoPlayer.time = 0;
        
        videoPlayer.Prepare();
        
        while (!videoPlayer.isPrepared)
        {
            yield return null;
        }
        
        Time.timeScale = 0f;
        
        videoPlayer.frame = 0;
        videoPlayer.Play();
        
        videoCutscenePanel.SetActive(true);
        isPlaying = true;
        
        onVideoStart.Invoke();
    }
    
    void OnVideoComplete(VideoPlayer vp)
    {
        isPlaying = false;
        
        Time.timeScale = 1f;
        
        videoCutscenePanel.SetActive(false);
        
        onVideoComplete.Invoke();
    }
    
    public void SkipVideo()
    {
        if (!isPlaying) return;
        
        videoPlayer.Stop();
        OnVideoComplete(videoPlayer);
    }
}
