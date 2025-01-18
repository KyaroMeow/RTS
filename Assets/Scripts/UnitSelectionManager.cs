using UnityEngine;
using System.Collections.Generic;

public class UnitSelectionManager : MonoBehaviour
{
    public LayerMask unitLayer;
    public RectTransform selectionBox;
    public List<GameObject> selectedUnits = new List<GameObject>();

    private Vector2 startPos;
    private bool isSelecting = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
            isSelecting = true;
            selectionBox.gameObject.SetActive(true);
            selectionBox.anchoredPosition = startPos;
            selectionBox.sizeDelta = Vector2.zero;
        }

        if (Input.GetMouseButton(0) && isSelecting)
        {
            Vector2 currentPos = Input.mousePosition;
            Vector2 boxSize = currentPos - startPos;
            selectionBox.sizeDelta = new Vector2(Mathf.Abs(boxSize.x), Mathf.Abs(boxSize.y));
            selectionBox.anchoredPosition = startPos + boxSize / 2;
        }

        if (Input.GetMouseButtonUp(0) && isSelecting)
        {
            isSelecting = false;
            selectionBox.gameObject.SetActive(false);
            SelectUnits();
        }
    }

    void SelectUnits()
    {
        Vector2 min = selectionBox.anchoredPosition - (selectionBox.sizeDelta / 2);
        Vector2 max = selectionBox.anchoredPosition + (selectionBox.sizeDelta / 2);

        Collider[] colliders = Physics.OverlapBox(Camera.main.ScreenToWorldPoint(new Vector3(min.x, min.y, Camera.main.nearClipPlane)),
                                                  new Vector3(Mathf.Abs(max.x - min.x), Mathf.Abs(max.y - min.y), 0),
                                                  Quaternion.identity,
                                                  unitLayer);

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
