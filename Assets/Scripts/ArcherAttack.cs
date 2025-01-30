using UnityEngine;

public class ArcherAttack : MonoBehaviour
{
    public GameObject arrowPrefab; // ���������� ������ ������ ���� � ����������
    public Transform arrowSpawnPoint; // ���������� ������ ������ ���� � ����������
    public float launchForce = 10f; // ���� ������� ������
    public Vector3 initialAngularVelocity = Vector3.zero; // ��������� ������� �������� ������

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // ������: ������ ������ ��� ������� �������
        {
            LaunchArrow();
        }
    }

    void LaunchArrow()
    {
        
            // ������� ������ � ������� � � ��������� ArrowSpawnPoint
            GameObject newArrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, arrowSpawnPoint.rotation);

            // �������� ��������� Rigidbody ������
            Rigidbody rb = newArrow.GetComponent<Rigidbody>();

            if (rb != null)
            {
                // ��������� ���� �������
                rb.AddForce(arrowSpawnPoint.forward * launchForce, ForceMode.Impulse);

                // ������������� ��������� ������� ��������
                rb.angularVelocity = initialAngularVelocity;
            }
        
    
    }
}
