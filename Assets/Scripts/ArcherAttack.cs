using UnityEngine;

public class ArcherAttack : MonoBehaviour
{
    public GameObject arrowPrefab; // Перетащите префаб стрелы сюда в инспекторе
    public Transform arrowSpawnPoint; // Перетащите пустой объект сюда в инспекторе
    public float launchForce = 10f; // Сила запуска стрелы
    public Vector3 initialAngularVelocity = Vector3.zero; // Начальная угловая скорость стрелы

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Пример: запуск стрелы при нажатии пробела
        {
            LaunchArrow();
        }
    }

    void LaunchArrow()
    {
        
            // Создаем стрелу в позиции и с поворотом ArrowSpawnPoint
            GameObject newArrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, arrowSpawnPoint.rotation);

            // Получаем компонент Rigidbody стрелы
            Rigidbody rb = newArrow.GetComponent<Rigidbody>();

            if (rb != null)
            {
                // Применяем силу запуска
                rb.AddForce(arrowSpawnPoint.forward * launchForce, ForceMode.Impulse);

                // Устанавливаем начальную угловую скорость
                rb.angularVelocity = initialAngularVelocity;
            }
        
    
    }
}
