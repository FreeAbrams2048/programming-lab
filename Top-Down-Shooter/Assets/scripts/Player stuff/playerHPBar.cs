using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class playerHPBar : MonoBehaviour
{
    public Slider Slider;
    public Color PLow;
    public Color PHigh;

    public playerMoving PlayerMoving;

    private void Start()
    {
        Slider.maxValue = PlayerMoving.playerHPMax;
    }

    private void Update()
    {
        Slider.value = PlayerMoving.playerHPCurrent;
        Slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(PLow, PHigh, Slider.normalizedValue);
    }
}
