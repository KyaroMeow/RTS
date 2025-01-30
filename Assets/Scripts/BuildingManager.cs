using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour
{
    public PlayerResources playerResources; // ������ �� ������� ������
    public List<ButtonBuildingPair> buttonBuildingPairs = new List<ButtonBuildingPair>();
    public LayerMask planeMask;
    public string[] obstacleTags; // ������ ����� ��� �����������
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

        // ������������� �� ������� ������� ������
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

        // ���� ���������� �� �������� ������������
        if (isFollowing)
        {
            // ������� ��� �� ������ ����� ������� �������
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // ���������, ����� �� ��� � �����-���� ������
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, planeMask))
            {
                // ��������� ������� �������� � ����� ��������� ����
                instance.transform.position = hit.point;

                // ��������� ����������� � ������� ���������
                CheckCollisions();
            }
        }

        // ���� ���� ������ ������
        if (isBuilding && selectedUnit != null)
        {
            // ���������� ���� � ������
            MoveUnitToBuilding();
        }
    }

    // ����� ��� ��������� ������� ������
    private void OnButtonClick(GameObject prefab)
    {
        ToggleFollowing(prefab);
    }

    // ����� ��� ������������ ���������� �� ��������
    public void ToggleFollowing(GameObject prefab)
    {
        isFollowing = !isFollowing;

        if (isFollowing && instance == null)
        {
            // ������� ��������� �������
            instance = Instantiate(prefab);
            selectionSprite = instance.transform.Find("SelectionSprite").GetComponent<SpriteRenderer>();
            if (selectionSprite != null)
            {
                selectionSprite.gameObject.SetActive(true);
            }
        }
        else if (!isFollowing && instance != null)
        {
            // ���������� ��������� �������
            Destroy(instance);
            instance = null;
        }
    }

    // ����� ��� �������� �����������
    private void CheckCollisions()
    {
        if (selectionSprite == null) return;

        // �������� BoxCollider �������
        BoxCollider boxCollider = instance.GetComponent<BoxCollider>();
        if (boxCollider == null) return;

        // ���������� OverlapBoxNonAlloc ��� �������� �����������
        Collider[] colliders = new Collider[10];
        int numColliders = Physics.OverlapBoxNonAlloc(boxCollider.bounds.center, boxCollider.bounds.extents, colliders, instance.transform.rotation);

        // ���������, ���� �� ����������� � ��������� � ������ �� ������� obstacleTags
        bool isColliding = false;
        for (int i = 0; i < numColliders; i++)
        {
            if (colliders[i] == boxCollider) continue; // ��������� ������ ����

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

        // ���� ���� �����������, ������������� ���� � �������
        if (isColliding)
        {
            selectionSprite.color = Color.red;
        }
        else
        {
            // ���� ��� �����������, ������������� ���� � �������
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
                // ������ �������
                pair.button.interactable = true;
                var colors = pair.button.colors;
                colors.normalColor = Color.white; // ���� �������� ������
                pair.button.colors = colors;
            }
            else
            {
                // ������ ���������
                pair.button.interactable = false;
                var colors = pair.button.colors;
                colors.normalColor = Color.gray; // ���� ���������� ������
                pair.button.colors = colors;
            }
        }
    }

    // ����� ��� ����������� ����� � ������ � �������������
    private void MoveUnitToBuilding()
    {
        if (selectedUnit == null || instance == null) return;

        // ���������� ���� � ������
        selectedUnit.transform.position = Vector3.MoveTowards(selectedUnit.transform.position, instance.transform.position, Time.deltaTime * 5f);

        // ���������, ������ �� ���� ������
        if (Vector3.Distance(selectedUnit.transform.position, instance.transform.position) < 0.1f)
        {
            // �������� �������������
            StartCoroutine(BuildBuilding());
        }
    }

    // �������� ��� ������������� ������
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

        // ������������� ���������
        isBuilding = false;
        selectedUnit = null;
        instance = null;
    }

    // ����� ��� ������ �����
    public void SelectUnit(GameObject unit)
    {
        if (isBuilding) return; // ������ ������� ����� �� ����� �������������

        selectedUnit = unit;
        SelectedUnits.Add(unit);
    }

    // ����� ��� ������ ������ � �����
    public void DeselectUnit(GameObject unit)
    {
        if (isBuilding) return; // ������ ����� ����� � ����� �� ����� �������������

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
            