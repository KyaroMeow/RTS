using UnityEngine;
using UnityEngine.UI;

public class SliderValueDisplay : MonoBehaviour
{
    public Slider slider;
    public Text displayText;

    void Update()
    {
        float minValue = slider.minValue;
        float maxValue = slider.maxValue;
        float currentValue = slider.value;
        string displayString = $"{currentValue}/{maxValue}";
        displayText.text = displayString;
    }
}
