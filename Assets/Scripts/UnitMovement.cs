using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System.Collections.Generic;

public class UnitMovement : MonoBehaviour
{
    public RectTransform selectionBoxUI; // RectTransform для визуализации selection box
    public Camera mainCamera;

    private List<GameObject> selectedUnits = new List<GameObject>(); // Список выделенных юнитов
    private Vector2 startMousePos; // Начальная позиция мыши
    private Vector2 endMousePos; // Конечная позиция мыши
    private bool isSelecting = false; // Флаг для отслеживания состояния выделения

    void Update()
    {
        HandleSelection();
        HandleMovement();
    }

    void HandleSelection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startMousePos = Input.mousePosition;
            isSelecting = true;
            selectionBoxUI.gameObject.SetActive(true);
            selectionBoxUI.sizeDelta = Vector2.zero; // Сброс размера
            selectionBoxUI.anchoredPosition = startMousePos; // Установите начальную позицию
        }

        if (Input.GetMouseButton(0) && isSelecting)
        {
            endMousePos = Input.mousePosition;
            UpdateSelectionBox();
        }

        if (Input.GetMouseButtonUp(0) && isSelecting)
        {
            isSelecting = false;
            selectionBoxUI.gameObject.SetActive(false);
            SelectUnitsInBox();
        }
    }

    void UpdateSelectionBox()
    {
        float width = endMousePos.x - startMousePos.x;
        float height = endMousePos.y - startMousePos.y;
        selectionBoxUI.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));

        // Установите позицию selectionBox в центр между startMousePos и endMousePos
        selectionBoxUI.anchoredPosition = startMousePos + new Vector2(width / 2, height / 2);
    }

    void SelectUnitsInBox()
    {
        DeselectAllUnits();

        Vector2 min = Vector2.Min(startMousePos, endMousePos);
        Vector2 max = Vector2.Max(startMousePos, endMousePos);

        foreach (GameObject unit in GameObject.FindGameObjectsWithTag("Unit"))
        {
            Vector3 screenPos = mainCamera.WorldToScreenPoint(unit.transform.position);

            if (screenPos.x >= min.x && screenPos.x <= max.x &&
                screenPos.y >= min.y && screenPos.y <= max.y)
            {
                SelectUnit(unit);
            }
        }
    }

    void HandleMovement()
    {
        if (selectedUnits.Count > 0 && Input.GetMouseButtonDown(1))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                foreach (GameObject unit in selectedUnits)
                {
                    NavMeshAgent agent = unit.GetComponent<NavMeshAgent>();
                    if (agent != null)
                    {
                        agent.SetDestination(hit.point);
                    }
                }
            }
        }
    }

    void SelectUnit(GameObject unit)
    {
        selectedUnits.Add(unit);
        Transform selectionSprite = unit.transform.Find("SelectionSprite");
        if (selectionSprite != null)
        {
            selectionSprite.gameObject.SetActive(true);
        }
    }

    void DeselectAllUnits()
    {
        foreach (GameObject unit in selectedUnits)
        {
            Transform selectionSprite = unit.transform.Find("SelectionSprite");
            if (selectionSprite != null)
            {
                selectionSprite.gameObject.SetActive(false);
            }
        }
        selectedUnits.Clear();
    }
}
