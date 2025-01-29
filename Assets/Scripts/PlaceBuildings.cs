using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceBuildings : MonoBehaviour
{
    public GameObject prefab;
    public LayerMask planeMask;
    public string[] obstacleTags; // ������ ����� ��� �����������
    private GameObject instance;
    private Camera mainCamera;
    private bool isFollowing = false;
    private SpriteRenderer selectionSprite;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found!");
            return;
        }

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
    }

    // ����� ��� ������������ ���������� �� ��������
    public void ToggleFollowing()
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
}
