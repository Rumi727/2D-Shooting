using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteAlways]
public class Slider : MonoBehaviour
{
    public UnityEngine.UI.Slider slider;
    public TMP_Text valueText;

    void Update() => valueText.text = slider.value.ToString();
}
