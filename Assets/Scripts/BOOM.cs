using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOOM : MonoBehaviour
{
    public float explosionForce = 1000.0f; // Сила взрыва
    public float explosionRadius = 5.0f; // Радиус взрыва
    public float upwardsModifier = 1.0f; // Множитель для вертикальной составляющей силы
  

    

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
           Explode();
        }
    }

    void Explode()
    {
        // Получаем позицию бомбы
        Vector3 explosionPosition = transform.position;

        // Находим все объекты с Rigidbody в радиусе взрыва
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, explosionRadius);

        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Применяем силу взрыва к объекту
                rb.AddExplosionForce(explosionForce, explosionPosition, explosionRadius, upwardsModifier, ForceMode.Impulse);
            }
        }

       
    }
}
