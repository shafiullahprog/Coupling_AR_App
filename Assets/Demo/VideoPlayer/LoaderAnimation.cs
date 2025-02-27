using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderAnimation : MonoBehaviour
{
    event Action fillLoader;

    [SerializeField] UnityEngine.UI.Image loaderImage;

    private float fillAmount;
    public float FillAmount
    {
        get { return fillAmount; }
        set
        {
            fillAmount = value;
            if (fillAmount == 0)
            {
                StartCoroutine(LerpFillAmount(1f, true, 0f, 1f));
            }
            else if(fillAmount == 1)
            {
                StartCoroutine(LerpFillAmount(1f, false, 1f, 0f));
            }
        }
    }


    // Start is called before the first frame update
    void OnEnable()
    {        
        FillAmount = 0;
    }    

    IEnumerator LerpFillAmount(float time, bool clockwise, float startValue, float endValue)
    {
        float timeElapsed = 0;
        float duration = time;
        loaderImage.fillClockwise = clockwise;
        while (timeElapsed < duration)
        {
            loaderImage.fillAmount = Mathf.Lerp(startValue, endValue, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        loaderImage.fillAmount = endValue;
        FillAmount = endValue;
    }
}
