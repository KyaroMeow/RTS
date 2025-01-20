using UnityEngine;
using System.Collections.Generic;

public class UnitSelectionManager : MonoBehaviour
{
    public LayerMask unitLayer; // Слой для юнитов
    public RectTransform selectionBox; // RectTransform для визуализации selection box
    public List<GameObject> selectedUnits = new List<GameObject>(); // Список выделенных юнитов

    private Vector2 startMousePos; // Начальная позиция мыши
    private Vector2 endMousePos; // Конечная позиция мыши
    private bool isSelecting = false; // Флаг для отслеживания состояния выделения
    private GameObject visualBox; // Временный объект для визуализации

    void Start()
    {
        // Убедитесь, что selectionBox отключен при старте
        selectionBox.gameObject.SetActive(false);

        // Создайте временный объект для визуализации
        visualBox = GameObject.CreatePrimitive(PrimitiveType.Cube);
        visualBox.name = "SelectionVisualBox";
        visualBox.SetActive(false);
        Destroy(visualBox.GetComponent<Collider>()); // Удалите коллайдер
        Material mat = new Material(Shader.Find("Transparent/Diffuse"));
        mat.color = new Color(1, 0, 0, 0.5f); // Полупрозрачный красный цвет
        visualBox.GetComponent<MeshRenderer>().material = mat;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startMousePos = Input.mousePosition;
            isSelecting = true;
            selectionBox.gameObject.SetActive(true);
            selectionBox.sizeDelta = Vector2.zero; // Сброс размера
            selectionBox.anchoredPosition = startMousePos; // Установите начальную позицию
        }

        if (Input.GetMouseButton(0) && isSelecting)
        {
            endMousePos = Input.mousePosition;
            UpdateSelectionBox();
        }

        if (Input.GetMouseButtonUp(0) && isSelecting)
        {
            isSelecting = false;
            selectionBox.gameObject.SetActive(false);
            visualBox.SetActive(false); // Отключите временный объект
            SelectUnits();
        }
    }

    void UpdateSelectionBox()
    {
        float width = endMousePos.x - startMousePos.x;
        float height = endMousePos.y - startMousePos.y;
        selectionBox.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));

        // Установите позицию selectionBox в центр между startMousePos и endMousePos
        selectionBox.anchoredPosition = startMousePos + new Vector2(width / 2, height / 2);

        // Обновите временный объект для визуализации
        Vector2 min = selectionBox.anchoredPosition - (selectionBox.sizeDelta / 2);
        Vector2 max = selectionBox.anchoredPosition + (selectionBox.sizeDelta / 2);

        Vector3 minWorld = Camera.main.ScreenToWorldPoint(new Vector3(min.x, min.y, Camera.main.nearClipPlane));
        Vector3 maxWorld = Camera.main.ScreenToWorldPoint(new Vector3(max.x, max.y, Camera.main.nearClipPlane));

        visualBox.transform.position = (minWorld + maxWorld) / 2;
        visualBox.transform.localScale = maxWorld - minWorld;
        visualBox.SetActive(true);

        // Отладка: вывод размеров и позиции selectionBox
        Debug.Log("Selection Box Size: " + selectionBox.sizeDelta);
        Debug.Log("Selection Box Position: " + selectionBox.anchoredPosition);
        Debug.Log("Visual Box Position: " + visualBox.transform.position);
        Debug.Log("Visual Box Scale: " + visualBox.transform.localScale);
    }

    void SelectUnits()
    {
        Vector2 min = selectionBox.anchoredPosition - (selectionBox.sizeDelta / 2);
        Vector2 max = selectionBox.anchoredPosition + (selectionBox.sizeDelta / 2);

        Vector3 minWorld = Camera.main.ScreenToWorldPoint(new Vector3(min.x, min.y, Camera.main.nearClipPlane));
        Vector3 maxWorld = Camera.main.ScreenToWorldPoint(new Vector3(max.x, max.y, Camera.main.nearClipPlane));

        Debug.Log("Selection Box Min: " + minWorld);
        Debug.Log("Selection Box Max: " + maxWorld);

        Collider[] colliders = Physics.OverlapBox((minWorld + maxWorld) / 2, (maxWorld - minWorld) / 2, Quaternion.identity, unitLayer);

        selectedUnits.Clear();
        foreach (Collider collider in colliders)
        {
            GameObject unit = collider.gameObject;
            Debug.Log("Checking unit: " + unit.name);
            if (unit.CompareTag("Unit"))
            {
                selectedUnits.Add(unit);
                Debug.Log("Selected unit: " + unit.name); // Отладка: вывод имени выделенного юнита
            }
        }

        if (selectedUnits.Count == 0)
        {
            Debug.Log("No units selected."); // Отладка: вывод, если ни один юнит не выделен
        }

        UpdateHUD();
    }

    void UpdateHUD()
    {
       HUDManager.Instance.ClearHUD();

        foreach (GameObject unit in selectedUnits)
        {
            UnitScript unitComponent = unit.GetComponent<UnitScript>();
            if (unitComponent != null)
            {
                HUDManager.Instance.ShowUnitHUD(unitComponent);
                Debug.Log("Displaying HUD for unit: " + unit.name); // Отладка: вывод имени юнита, для которого отображается HUD
            }
        }
    }
}
