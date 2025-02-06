using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public Transform spawnPoint;
    public float spawnInterval = 1.0f; 

    void Start()
    {
        StartCoroutine(SpawnWithInterval());
    }

    IEnumerator SpawnWithInterval()
    {
        while (true)
        {
            Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
