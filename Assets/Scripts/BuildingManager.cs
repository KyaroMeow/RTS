using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour
{
    public PlayerResources playerResources; // Ссылка на ресурсы игрока
    public List<ButtonBuildingPair> buttonBuildingPairs = new List<ButtonBuildingPair>();
    public LayerMask planeMask;
    public string[] obstacleTags; // Массив тегов для препятствий
    private GameObject instance;
    private Camera mainCamera;
    private bool isFollowing = false;
    private SpriteRenderer selectionSprite;
    private GameObject selectedUnit;
    private bool isBuilding = false;

    public static List<GameObject> SelectedUnits { get; private set; } = new List<GameObject>();

    void Start()
    {
        mainCamera = Camera.main;

        // Подписываемся на события нажатия кнопок
        foreach (var pair in buttonBuildingPairs)
        {
            pair.button.onClick.AddListener(() => OnButtonClick(pair.building.gameObject));
        }
    }

    void Update()
    {
        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found!");
            return;
        }

        UpdateButtonStates();

        // Если следование за курсором активировано
        if (isFollowing)
        {
            // Создаем луч от камеры через позицию курсора
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Проверяем, попал ли луч в какой-либо объект
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, planeMask))
            {
                // Обновляем позицию модельки в точке попадания луча
                instance.transform.position = hit.point;

                // Проверяем пересечения с другими объектами
                CheckCollisions();
            }
        }

        // Если юнит строит здание
        if (isBuilding && selectedUnit != null)
        {
            // Перемещаем юнит к зданию
            MoveUnitToBuilding();
        }
    }

    // Метод для обработки нажатия кнопки
    private void OnButtonClick(GameObject prefab)
    {
        ToggleFollowing(prefab);
    }

    // Метод для переключения следования за курсором
    public void ToggleFollowing(GameObject prefab)
    {
        isFollowing = !isFollowing;

        if (isFollowing && instance == null)
        {
            // Создаем экземпляр префаба
            instance = Instantiate(prefab);
            selectionSprite = instance.transform.Find("SelectionSprite").GetComponent<SpriteRenderer>();
            if (selectionSprite != null)
            {
                selectionSprite.gameObject.SetActive(true);
            }
        }
        else if (!isFollowing && instance != null)
        {
            // Уничтожаем экземпляр префаба
            Destroy(instance);
            instance = null;
        }
    }

    // Метод для проверки пересечений
    private void CheckCollisions()
    {
        if (selectionSprite == null) return;

        // Получаем BoxCollider объекта
        BoxCollider boxCollider = instance.GetComponent<BoxCollider>();
        if (boxCollider == null) return;

        // Используем OverlapBoxNonAlloc для проверки пересечений
        Collider[] colliders = new Collider[10];
        int numColliders = Physics.OverlapBoxNonAlloc(boxCollider.bounds.center, boxCollider.bounds.extents, colliders, instance.transform.rotation);

        // Проверяем, есть ли пересечения с объектами с тегами из массива obstacleTags
        bool isColliding = false;
        for (int i = 0; i < numColliders; i++)
        {
            if (colliders[i] == boxCollider) continue; // Исключаем самого себя

            foreach (var tag in obstacleTags)
            {
                if (colliders[i].CompareTag(tag))
                {
                    isColliding = true;
                    break;
                }
            }
            if (isColliding) break;
        }

        // Если есть пересечения, устанавливаем цвет в красный
        if (isColliding)
        {
            selectionSprite.color = Color.red;
        }
        else
        {
            // Если нет пересечений, устанавливаем цвет в зеленый
            selectionSprite.color = Color.green;
        }
    }

    void UpdateButtonStates()
    {
        foreach (var pair in buttonBuildingPairs)
        {
            bool canBuild = playerResources.woodCount >= pair.building.woodCost &&
                            playerResources.stoneCount >= pair.building.stoneCost;
            if (canBuild)
            {
                // Кнопка активна
                pair.button.interactable = true;
                var colors = pair.button.colors;
                colors.normalColor = Color.white; // Цвет активной кнопки
                pair.button.colors = colors;
            }
            else
            {
                // Кнопка неактивна
                pair.button.interactable = false;
                var colors = pair.button.colors;
                colors.normalColor = Color.gray; // Цвет неактивной кнопки
                pair.button.colors = colors;
            }
        }
    }

    // Метод для перемещения юнита к зданию и строительства
    private void MoveUnitToBuilding()
    {
        if (selectedUnit == null || instance == null) return;

        // Перемещаем юнит к зданию
        selectedUnit.transform.position = Vector3.MoveTowards(selectedUnit.transform.position, instance.transform.position, Time.deltaTime * 5f);

        // Проверяем, достиг ли юнит здания
        if (Vector3.Distance(selectedUnit.transform.position, instance.transform.position) < 0.1f)
        {
            // Начинаем строительство
            StartCoroutine(BuildBuilding());
        }
    }

    // Корутина для строительства здания
    private IEnumerator BuildBuilding()
    {
        isBuilding = true;
        float buildTime = instance.GetComponent<BuildingsScript>().buildTime;
        float elapsedTime = 0f;

        while (elapsedTime < buildTime)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Строительство завершено
        isBuilding = false;
        selectedUnit = null;
        instance = null;
    }

    // Метод для выбора юнита
    public void SelectUnit(GameObject unit)
    {
        if (isBuilding) return; // Нельзя выбрать юнита во время строительства

        selectedUnit = unit;
        SelectedUnits.Add(unit);
    }

    // Метод для снятия выбора с юнита
    public void DeselectUnit(GameObject unit)
    {
        if (isBuilding) return; // Нельзя снять выбор с юнита во время строительства

        selectedUnit = null;
        SelectedUnits.Remove(unit);
    }
}

[System.Serializable]
public class ButtonBuildingPair
{
    public Button button;
    public BuildingsScript building;
}
            