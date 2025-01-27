using UnityEngine;

public class BasePlacer : MonoBehaviour
{
    public GameObject playerBasePrefab;
    public GameObject enemyBasePrefab;
    public int numberOfBases = 4;
    public float radius = 50.0f;
    public float baseClearRadius = 10.0f;
    public Transform planeCenter; // Центр Plane

    void Start()
    {
        PlaceBases();
    }

    void PlaceBases()
    {
        float angleStep = 360.0f / numberOfBases;
        float angle = 0.0f;

        for (int i = 0; i < numberOfBases; i++)
        {
            float x = radius * Mathf.Cos(angle * Mathf.Deg2Rad);
            float z = radius * Mathf.Sin(angle * Mathf.Deg2Rad);

            Vector3 position = planeCenter.position + new Vector3(x, 0, z);
            GameObject baseObj = (i == 0) ? Instantiate(playerBasePrefab, position, Quaternion.identity) : Instantiate(enemyBasePrefab, position, Quaternion.identity);

            // Удаление ресурсов в радиусе вокруг базы
            Collider[] colliders = Physics.OverlapSphere(position, baseClearRadius);
            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Resource"))
                {
                    Destroy(collider.gameObject);
                }
            }

            angle += angleStep;
        }
    }
}
