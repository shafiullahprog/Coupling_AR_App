using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoPlayerControls : MonoBehaviour
{
    public static event Action<VideoPlayer> OnVideoPlay;
    public static event Action<VideoPlayer> OnVideoPause;

    public VideoPlayer videoPlayer;
    public bool isPlaying;
    public bool IsPlaying
    {
        get { return isPlaying; }
        set
        {
            isPlaying = value;
            if (!isPlaying)
                OnVideoPause?.Invoke(videoPlayer);
            else if (isPlaying)
                OnVideoPlay?.Invoke(videoPlayer);
        }
    }
    double lastTimePlayed;
    public Slider slider;
    public RenderTexture _rt;
    public GameObject loader;
    public GameObject play;
    public GameObject pause;
    public GameObject audioP;   
    public int audioSpriteIndex;
    public List<Sprite> volumeSprites = new List<Sprite>();
    public TMP_Text currentTime;
    public TMP_Text videoTime;
    public CanvasGroup stripTimeline;

    private void Awake()
    {
        //slider.gameObject.SetActive(false);
        audioP.GetComponent<Image>().sprite = volumeSprites[volumeSprites.Count - 1];
        audioSpriteIndex = volumeSprites.IndexOf(audioP.GetComponent<Image>().sprite);
    }

    private void OnEnable()
    {
        //OnVideoPlay += Play;
        //OnVideoPause += Pause;
        videoPlayer.prepareCompleted += OnPrepare;
        videoPlayer.loopPointReached += Pause;
    }

    private void OnDisable()
    {
        //OnVideoPlay -= Play;
        //OnVideoPause -= Pause;
        videoPlayer.prepareCompleted -= OnPrepare;
        videoPlayer.loopPointReached -= Pause;
    }

    private void Update()
    {
        if (videoPlayer.isPlaying && (Time.frameCount % (int)(videoPlayer.frameRate + 1)) == 0)
        {
            //if the video time is the same as the previous check, that means it's buffering cuz the video is Playing.
            if (lastTimePlayed == videoPlayer.time)//buffering
            {
                //Debug.Log("buffering");
                if (!loader.activeInHierarchy)
                    loader.SetActive(true);
            }
            else//not buffering
            {
                //Debug.Log("Not buffering");
                if (loader.activeInHierarchy)
                    loader.SetActive(false);
            }
            lastTimePlayed = videoPlayer.time;
        }

        if (videoPlayer.clip != null)
        {
            currentTime.text = FormatTime(videoPlayer.time);
        }
    }

    public void PlayVideoURL(string url)
    {
        //StartCoroutine(GetPathAndAddtoVideoPlayer(url));
        videoPlayer.source = VideoSource.Url;
        videoPlayer.Stop();
        videoPlayer.url = url;
        loader.SetActive(true);
     
        /*if (videoPlayer.isPrepared)
        {
            videoPlayer.Play();
        }
        else
        {*/
            videoPlayer.Prepare();
        //}
    }

    public void CloseVideo()
    {
        videoPlayer.url = null;
        videoPlayer.Stop();
        videoPlayer.SetDirectAudioMute(0, false);
        _rt.Release();
        slider.value = 0;
        IsPlaying = false;
        gameObject.SetActive(false);
    }

    public void PlayPause(VideoPlayer vp)
    {
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Pause();
            play.SetActive(true);
            pause.SetActive(false);
            IsPlaying = false;
        }
        else
        {
            videoPlayer.Play();
            play.SetActive(false);
            pause.SetActive(true);
            IsPlaying = true;
        }
    }

    public void OnPrepare(VideoPlayer videoPlayer)
    {
        videoPlayer.targetTexture = _rt;
        videoPlayer.GetComponent<RawImage>().texture = _rt;
        //_rt = videoPlayer.GetComponent<RenderTextureCreate>()._rtexture;
        videoTime.text = FormatTime(videoPlayer.length);
        loader.SetActive(false);
        Play(videoPlayer);
    }

    public void Play(VideoPlayer videoPlayer)
    {
        videoPlayer.Play();
        play.SetActive(false);
        pause.SetActive(true);
        IsPlaying = true;
    }

    public void Pause(VideoPlayer videoPlayer)
    {
        videoPlayer.Pause();
        play.SetActive(true);
        pause.SetActive(false);
        IsPlaying = false;

    }


    public void AudioSpriteSwitch()
    {
        if (audioSpriteIndex < volumeSprites.Count - 1)
        {
            audioSpriteIndex++;
        }
        else
        {
            audioSpriteIndex = 0;
        }
        audioP.GetComponent<Image>().sprite = volumeSprites[audioSpriteIndex];        
        audioP.GetComponent<AudioSource>().volume = (float)audioSpriteIndex / (volumeSprites.Count - 1);
    }
    
    public string FormatTime(double videoTIme)
    {
        string mins = Mathf.FloorToInt((float)videoTIme / 60).ToString("00");
        string secs = Mathf.FloorToInt((float)videoTIme % 60).ToString("00");
        return mins + ":" + secs;
    }

}
