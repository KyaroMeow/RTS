using UnityEngine;
using UnityEngine.UI;

public class SliderValueDisplayPercent : MonoBehaviour
{
    public Slider slider;
    public Text displayText;

    void Update()
    {
        float maxValue = slider.maxValue;
        float minValue = slider.minValue;
        float currentValue = slider.value;
        float percentValue = ((currentValue - minValue) / (maxValue - minValue)) * 100;
        string displayString = $"{percentValue:F0}%";
        displayText.text = displayString;
    }
}
