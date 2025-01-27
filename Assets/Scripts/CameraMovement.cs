using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float scrollSpeed = 5.0f; // �������� ����������� � ���������
    public float panSpeed = 10.0f; // �������� ����������� ������
    public float arrowPanSpeed = 10.0f; // �������� ����������� ������ ��� ������������� �������
    public float minFOV = 15.0f; // ����������� �������� FOV
    public float maxFOV = 90.0f; // ������������ �������� FOV

    private Camera mainCamera;
    private bool isPanning = false;
    private Vector3 lastMousePosition;

    void Start()
    {
        mainCamera = GetComponent<Camera>();
        if (mainCamera == null)
        {
            Debug.LogError("Camera component is not attached to the GameObject.");
        }
        else if (!mainCamera.orthographic)
        {
            Debug.Log("Camera is set to perspective projection.");
        }
    }

    void Update()
    {
        HandleZoom();
        HandlePan();
        HandleArrowKeys();
    }

    void HandleZoom()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput != 0)
        {
            float newFOV = mainCamera.fieldOfView - scrollInput * scrollSpeed;
            newFOV = Mathf.Clamp(newFOV, minFOV, maxFOV);
            mainCamera.fieldOfView = newFOV;
        }
    }

    void HandlePan()
    {
        if (Input.GetMouseButtonDown(2)) // ������� ������ ����
        {
            isPanning = true;
            lastMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(2))
        {
            isPanning = false;
        }

        if (isPanning)
        {
            Vector3 mouseDelta = Input.mousePosition - lastMousePosition;
            // �������� ����������� ������
            Vector3 move = new Vector3(mouseDelta.x, 0, mouseDelta.y) * panSpeed * Time.deltaTime;
            transform.Translate(-move, Space.World); // �������� �����������
            lastMousePosition = Input.mousePosition;
        }
    }

    void HandleArrowKeys()
    {
        Vector3 move = Vector3.zero;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            move.z += 1;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            move.z -= 1;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            move.x -= 1;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            move.x += 1;
        }

        if (move != Vector3.zero)
        {
            transform.Translate(move.normalized * arrowPanSpeed * Time.deltaTime, Space.World);
        }
    }
}
