using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class EnemyPanelManager : MonoBehaviour
{
    public Dropdown enemyCountDropdown;
    public GameObject enemyPanelPrefab;
    public Transform enemyPanelsContainer;
    private List<Color> availableColors = new List<Color>();
    private List<GameObject> enemyPanels = new List<GameObject>();

    private void Start()
    {
        // Инициализация доступных цветов
        availableColors.Add(new Color(1, 0, 0)); // Красный
        availableColors.Add(new Color(0, 1, 0)); // Зеленый
        availableColors.Add(new Color(0, 0, 1)); // Синий
        availableColors.Add(new Color(1, 1, 0)); // Желтый
        availableColors.Add(new Color(1, 0, 1)); // Фиолетовый
        availableColors.Add(new Color(0, 1, 1)); // Голубой
        availableColors.Add(new Color(1, 0.5f, 0)); // Оранжевый
        availableColors.Add(new Color(0.5f, 0, 1)); // Розовый
        availableColors.Add(new Color(0.5f, 1, 0)); // Лаймовый
        availableColors.Add(new Color(0, 0.5f, 1)); // Бирюзовый

        // Настройка дропдауна
        enemyCountDropdown.ClearOptions();
        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();
        for (int i = 1; i <= 6; i++)
        {
            options.Add(new Dropdown.OptionData(i.ToString()));
        }
        enemyCountDropdown.AddOptions(options);

        // Назначение начального значения дропдауна
        enemyCountDropdown.value = 1;
        enemyCountDropdown.onValueChanged.AddListener(OnEnemyCountChanged);

        // Создание начальных панелей врагов
        OnEnemyCountChanged(enemyCountDropdown.value);
    }

    private void OnEnemyCountChanged(int count)
    {
        // Удаление старых панелей врагов
        foreach (var panel in enemyPanels)
        {
            Destroy(panel);
        }
        enemyPanels.Clear();

        // Создание новых панелей врагов
        for (int i = 0; i < count + 1; i++)
        {
            GameObject enemyPanel = Instantiate(enemyPanelPrefab, enemyPanelsContainer);
            enemyPanels.Add(enemyPanel);

            // Назначение цвета и текста панели
            Image colorDisplay = enemyPanel.GetComponentInChildren<Image>();
            Button colorButton = colorDisplay.GetComponent<Button>();
            colorButton.onClick.AddListener(() => ChangeColor(enemyPanel));
            colorDisplay.color = availableColors[i % availableColors.Count];

            Text enemyText = enemyPanel.GetComponentInChildren<Text>();
            enemyText.text = "Противник " + (i + 1);
        }
    }

    private void ChangeColor(GameObject enemyPanel)
    {
        Image colorDisplay = enemyPanel.GetComponentInChildren<Image>();
        int currentIndex = availableColors.IndexOf(colorDisplay.color);
        int newIndex = (currentIndex + 1) % availableColors.Count;
        colorDisplay.color = availableColors[newIndex];
    }
}
