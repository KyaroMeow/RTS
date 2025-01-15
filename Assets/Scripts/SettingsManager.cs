using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SettingsManager : MonoBehaviour
{
    public Toggle fullscreenToggle;
    public Dropdown resolutionDropdown;
    public Slider volumeSlider;
    public Button saveButton;

    private Resolution[] customResolutions = new Resolution[]
    {
        new Resolution { width = 1280, height = 720 },
        new Resolution { width = 1366, height = 768 },
        new Resolution { width = 1600, height = 900 },
        new Resolution { width = 1920, height = 1080 }
    };

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
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < customResolutions.Length; i++)
        {
            string option = customResolutions[i].width + " x " + customResolutions[i].height;
            options.Add(option);

            if (customResolutions[i].width == Screen.currentResolution.width &&
                customResolutions[i].height == Screen.currentResolution.height)
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

        // Инициализация кнопки сохранения
        saveButton.onClick.AddListener(delegate { SaveSettings(); });
    }

    void OnFullscreenToggle()
    {
        Screen.fullScreen = fullscreenToggle.isOn;
    }

    void OnResolutionChange()
    {
        Resolution resolution = customResolutions[resolutionDropdown.value];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    void OnVolumeChange()
    {
        AudioListener.volume = volumeSlider.value;
    }

    void SaveSettings()
    {
        PlayerPrefs.SetInt(FullscreenKey, fullscreenToggle.isOn ? 1 : 0);
        PlayerPrefs.SetInt(ResolutionKey, resolutionDropdown.value);
        PlayerPrefs.SetFloat(VolumeKey, volumeSlider.value);
        PlayerPrefs.Save();
        Debug.Log("Settings saved!");
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
            if (resolutionIndex >= 0 && resolutionIndex < customResolutions.Length)
            {
                Resolution resolution = customResolutions[resolutionIndex];
                Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
            }
        }

        if (PlayerPrefs.HasKey(VolumeKey))
        {
            AudioListener.volume = PlayerPrefs.GetFloat(VolumeKey);
        }
    }
}
