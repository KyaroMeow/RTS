using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float scrollSpeed = 5.0f; // Скорость приближения и отдаления
    public float panSpeed = 10.0f; // Скорость перемещения камеры
    public float arrowPanSpeed = 10.0f; // Скорость перемещения камеры при использовании стрелок
    public float minFOV = 15.0f; // Минимальное значение FOV
    public float maxFOV = 90.0f; // Максимальное значение FOV

    private Camera mainCamera;
    private bool isPanning = false;
    private Vector3 lastMousePosition;

    void Start()
    {
        mainCamera = GetComponent<Camera>();
       
    }

    void Update()
    {
        HandleZoom();
       
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
