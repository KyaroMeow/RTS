using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float speed = 5.0f; // Скорость перемещения камеры

    void Update()
    {
        // Получаем текущую позицию камеры
        Vector3 position = transform.position;

        // Обрабатываем нажатия клавиш WASD
        if (Input.GetKey(KeyCode.W))
        {
            position.z += speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            position.z -= speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            position.x -= speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            position.x += speed * Time.deltaTime;
        }

        // Обновляем позицию камеры
        transform.position = position;
    }
}
