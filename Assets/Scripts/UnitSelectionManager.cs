using UnityEngine;
using System.Collections.Generic;

public class UnitSelectionManager : MonoBehaviour
{
    public LayerMask unitLayer;
    public RectTransform selectionBox;
    public List<GameObject> selectedUnits = new List<GameObject>();

    private Vector2 startMousePos;
    private Vector2 endMousePos;
    private bool isSelecting = false;

    void Start()
    {
        // Убедитесь, что selectionBox отключен при старте
        selectionBox.gameObject.SetActive(false);
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
    }

    void SelectUnits()
    {
        Vector2 min = selectionBox.anchoredPosition - (selectionBox.sizeDelta / 2);
        Vector2 max = selectionBox.anchoredPosition + (selectionBox.sizeDelta / 2);

        Vector3 minWorld = Camera.main.ScreenToWorldPoint(new Vector3(min.x, min.y, Camera.main.nearClipPlane));
        Vector3 maxWorld = Camera.main.ScreenToWorldPoint(new Vector3(max.x, max.y, Camera.main.nearClipPlane));

        Collider[] colliders = Physics.OverlapBox(minWorld + (maxWorld - minWorld) / 2, (maxWorld - minWorld) / 2, Quaternion.identity, unitLayer);

        selectedUnits.Clear();
        foreach (Collider collider in colliders)
        {
            GameObject unit = collider.gameObject;
            if (unit.CompareTag("Unit"))
            {
                selectedUnits.Add(unit);
            }
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
            }
        }
    }
}
