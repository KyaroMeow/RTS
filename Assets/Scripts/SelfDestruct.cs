using Unity.VisualScripting;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    public float destructionDelay = 2.0f; // ����� �������� ����� ������������ � ��������
    
    void Start()
    {
        
        Destroy(gameObject, destructionDelay);
    }

}
