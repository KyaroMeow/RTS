using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SettingsManager : MonoBehaviour
{
    public Toggle fullscreenToggle;
    public Dropdown resolutionDropdown;
    public Slider volumeSlider;

    private Resolution[] resolutions;

    private const string FullscreenKey = "Fullscreen";
    private const string ResolutionKey = "Resolution";
    private const string VolumeKey = "Volume";

    void Start()
    {
        // Загрузка сохраненных настроек
        LoadSettings();

        // Инициализация переключателя полноэкранного режима
        fullscreenToggle.isOn = Screen.fullScreen;
        fullscreenToggle.onValueChanged.AddListener(delegate { OnFullscreenToggle(); });

        // Инициализация выпадающего списка разрешений
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        resolutionDropdown.onValueChanged.AddListener(delegate { OnResolutionChange(); });

        // Инициализация слайдера громкости
        volumeSlider.value = AudioListener.volume;
        volumeSlider.onValueChanged.AddListener(delegate { OnVolumeChange(); });
    }

    void OnFullscreenToggle()
    {
        Screen.fullScreen = fullscreenToggle.isOn;
        PlayerPrefs.SetInt(FullscreenKey, fullscreenToggle.isOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    void OnResolutionChange()
    {
        Resolution resolution = resolutions[resolutionDropdown.value];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt(ResolutionKey, resolutionDropdown.value);
        PlayerPrefs.Save();
    }

    void OnVolumeChange()
    {
        AudioListener.volume = volumeSlider.value;
        PlayerPrefs.SetFloat(VolumeKey, volumeSlider.value);
        PlayerPrefs.Save();
    }

    void LoadSettings()
    {
        if (PlayerPrefs.HasKey(FullscreenKey))
        {
            Screen.fullScreen = PlayerPrefs.GetInt(FullscreenKey) == 1;
        }

        if (PlayerPrefs.HasKey(ResolutionKey))
        {
            int resolutionIndex = PlayerPrefs.GetInt(ResolutionKey);
            if (resolutionIndex >= 0 && resolutionIndex < resolutions.Length)
            {
                Resolution resolution = resolutions[resolutionIndex];
                Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
            }
        }

        if (PlayerPrefs.HasKey(VolumeKey))
        {
            AudioListener.volume = PlayerPrefs.GetFloat(VolumeKey);
        }
    }
}
