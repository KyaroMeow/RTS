using UnityEngine;
using UnityEngine.UI;

public class SliderValueDisplay : MonoBehaviour
{
    public Slider slider;
    public Text displayText;
  
    void Update()
    {
        float maxValue = slider.maxValue;
        float currentValue = slider.value;
        displayText.text = $"{currentValue}/{maxValue}";
    }
}
