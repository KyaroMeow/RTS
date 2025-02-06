using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOOM : MonoBehaviour
{
    public float explosionForce = 1000.0f; // ���� ������
    public float explosionRadius = 5.0f; // ������ ������
    public float upwardsModifier = 1.0f; // ��������� ��� ������������ ������������ ����
  

    

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
           Explode();
        }
    }

    void Explode()
    {
        // �������� ������� �����
        Vector3 explosionPosition = transform.position;

        // ������� ��� ������� � Rigidbody � ������� ������
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, explosionRadius);

        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // ��������� ���� ������ � �������
                rb.AddExplosionForce(explosionForce, explosionPosition, explosionRadius, upwardsModifier, ForceMode.Impulse);
            }
        }

       
    }
}
