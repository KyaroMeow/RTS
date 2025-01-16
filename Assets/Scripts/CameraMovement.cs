using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float speed = 5.0f; // �������� ����������� ������

    void Update()
    {
        // �������� ������� ������� ������
        Vector3 position = transform.position;

        // ������������ ������� ������ WASD
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

        // ��������� ������� ������
        transform.position = position;
    }
}
