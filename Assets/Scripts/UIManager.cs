using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button collectButton;
    private UnitSelector unitSelector;
    private UnitMover unitMover;
    private GameObject selectedResource;
    private bool isSelectingResource = false; // ���� ��� ������ ������ �������

    void Start()
    {
        unitSelector = FindObjectOfType<UnitSelector>();
        unitMover = FindObjectOfType<UnitMover>();
        collectButton.onClick.AddListener(OnCollectButtonClick);
    }

    void Update()
    {
        if (isSelectingResource)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.CompareTag("Resource"))
                    {
                        selectedResource = hit.collider.gameObject;
                        isSelectingResource = false; // ������� �� ������ ������ �������
                        unitMover.MoveToResource(selectedResource);
                    }
                }
            }
        }
    }

    void OnCollectButtonClick()
    {
        if (UnitSelector.SelectedUnits.Count > 0)
        {
            isSelectingResource = true; // ������ � ����� ������ �������
        }
    }
}
