using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayPause : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler
{
    VideoPlayerControls vpControls;
    [SerializeField]
    private AnimationCurve inAnimation = AnimationCurve.EaseInOut(0, 0, 1, 1);
    [SerializeField]
    private AnimationCurve outAnimation = AnimationCurve.EaseInOut(0, 1, 1, 0);
    private Coroutine tweenRoutine;


    private void OnEnable()
    {
        VideoPlayerControls.OnVideoPlay += OnVideoPlay;
    }

    private void OnDisable()
    {
        VideoPlayerControls.OnVideoPlay -= OnVideoPlay;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (vpControls.stripTimeline.alpha == 0)
        {
            //StartCoroutine(TweenFader(0f, 1f, inAnimation, 0f, 1f));
        }
        else
        {
            if (vpControls.isPlaying || !vpControls.isPlaying)
            {
                vpControls.PlayPause(vpControls.videoPlayer);
            }
            //else
            //{
            //    StartCoroutine(TweenFader(0f, 1f, outAnimation, 1f, 0f));
            //}
        }
        //if (vpControls.isPlaying) vpControls.IsPlaying = true;
    }

#if UNITY_STANDALONE || UNITY_EDITOR
    public void OnPointerEnter(PointerEventData eventData)
    {
        //if (vpControls.stripTimeline.alpha == 1)
        //    return;

        //StartCoroutine(TweenFader(0f, 1f, inAnimation, 0f, 1f));
    }
#elif UNITY_ANDROID
public void OnPointerEnter(PointerEventData eventData)
{

}
#endif
    public void OnVideoPlay(UnityEngine.Video.VideoPlayer videoPlayer)
    {
        //StartCoroutine(TweenFader(0f, 1f, outAnimation, 1f, 0f));
    }
    IEnumerator TweenFader(float holdTime, float time, AnimationCurve curve, float startValue, float endValue)
    {
        yield return new WaitForSeconds(holdTime);
        float timeElapsed = 0;
        float duration = time;
        while (timeElapsed < duration)
        {
            vpControls.stripTimeline.alpha = Mathf.LerpUnclamped(startValue, endValue, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        vpControls.stripTimeline.alpha = endValue;
        tweenRoutine = null;
    }


    // Start is called before the first frame update
    private void Start()
    {
        vpControls = GetComponentInParent<VideoPlayerControls>();
    }

    private void Update()
    {
        if (vpControls.stripTimeline.alpha == 1 && vpControls.isPlaying)
        {            
            //tweenRoutine ??= StartCoroutine(TweenFader(10f, 1f, outAnimation, 1f, 0f));
        }
    }

}
