using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Video;

public class SliderSeeking : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField][Range(0f, 1f)] private float seekSlider;
    public VideoPlayer vp;
    public Slider slider;
    public Slider previewSlider;
    public bool videoIsJumping = false;
    private Coroutine seekWaitCoroutine = null;
    public VideoPlayerControls videoPlayerControls;
    public long noframe;

    private void OnEnable()
    {

    }

    private void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = 0f;
        previewSlider.value = 0f;
        seekSlider = 0f;
        SeekUI();
    }

    private void Update()
    {
        noframe = vp.frame;

        if (!videoIsJumping)
        {
            previewSlider.value = (float)vp.frame / vp.frameCount;
            slider.value = previewSlider.value;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        videoIsJumping = true;
        previewSlider.targetGraphic.gameObject.SetActive(false);
        slider.targetGraphic.gameObject.SetActive(true);
        if (vp.isPlaying)
        {
            vp.Pause();
        }
        VideoJump();

    }
    public void OnPointerUp(PointerEventData eventData)
    {
        videoIsJumping = false;
        previewSlider.targetGraphic.gameObject.gameObject.SetActive(true);
        slider.targetGraphic.gameObject.gameObject.SetActive(false);
        //KnobOnRelease();
        VideoJump();
    }

    public void nextFrame()
    {
        Debug.Log("Step forward 1 frame");
        vp.time += 10;
        if (vp.isPlaying)
            vp.Play();
    }

    public void previousFrame()
    {
        Debug.Log("Step backward 1 frame");
        vp.time -= 10;
        if (vp.isPlaying)
            vp.Play();
    }

    public void SeekUI()
    {
        slider.onValueChanged.AddListener((dragValue) =>
        {
            seekSlider = dragValue;
        });
    }

    public void KnobOnRelease()
    {
        if (seekWaitCoroutine != null)
            StopCoroutine(seekWaitCoroutine);
        seekWaitCoroutine = StartCoroutine(DelayedSetVideoIsJumpingToFalse());
    }

    private IEnumerator DelayedSetVideoIsJumpingToFalse()
    {
        yield return new WaitForSeconds(0.01f);
        VideoJump();
    }

    private void VideoJump()
    {
        var frame = vp.frameCount * seekSlider;
        vp.frame = (long)frame;
        //videoIsJumping = false;
        if (videoPlayerControls.isPlaying)
            vp.Play();
    }


    public void OnDrag(PointerEventData eventData)
    {
        vp.frame = (long)(vp.frameCount * seekSlider);
    }



}


