using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceBuildings : MonoBehaviour
{
    public GameObject prefab;
    public LayerMask planeMask;
    public string[] obstacleTags; // Массив тегов для препятствий
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
    }

    // Метод для переключения следования за курсором
    public void ToggleFollowing()
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
}
