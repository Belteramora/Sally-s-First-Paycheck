using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    private float oldCurrentValue = -1;

    public Image foregroundMainImage;
    public Image foregroundAddImage;
    public TMP_Text barText;

    public Color negativeMoveColor;
    public Color positiveMoveColor;

    public float transitionTime;
    public float delaySubBarTime;
    

    public void SetValue(float currentValue, float maxValue)
    {
        if (oldCurrentValue == -1)
            oldCurrentValue = currentValue;

        if (oldCurrentValue >= currentValue) 
        {
            Sequence seq = DOTween.Sequence();
            foregroundAddImage.color = negativeMoveColor;
            seq.Append(foregroundMainImage.DOFillAmount(currentValue / maxValue, transitionTime));
            seq.AppendInterval(delaySubBarTime);
            seq.Append(foregroundAddImage.DOFillAmount(currentValue / maxValue, transitionTime));
            seq.Play();
        }
        else
        {
            Sequence seq = DOTween.Sequence();
            foregroundAddImage.color = positiveMoveColor;

            seq.Append(foregroundAddImage.DOFillAmount(currentValue / maxValue, transitionTime));
            seq.AppendInterval(delaySubBarTime);
            seq.Append(foregroundMainImage.DOFillAmount(currentValue / maxValue, transitionTime));
            seq.Play();
        }

        oldCurrentValue = currentValue;

        barText.text = currentValue.ToString("F0") + "/" + maxValue.ToString("F0");
        

        //foregroundImage.fillAmount = procents;
    }
}
