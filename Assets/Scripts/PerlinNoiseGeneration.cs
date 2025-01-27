using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoiseGeneration : MonoBehaviour
{
    public GameObject[] prefabs;    // ������ �������� (��������, �����, �������)
    public int mapWidth = 100;      // ������ �����
    public int mapHeight = 100;     // ������ �����
    public float noiseScale = 20f;  // ������� ����
    public float threshold = 0.5f;  // ����� ����, ��� ������� ������ ������������
    public Transform planeCenter;   // ����� Plane

    void Start()
    {
        GenerateObjects();
    }

    void GenerateObjects()
    {
        for (int x = 0; x < mapWidth; x++)
        {
            for (int z = 0; z < mapHeight; z++)
            {
                // ��������� �������� ���� �������
                float xCoord = (float)x / mapWidth * noiseScale;
                float zCoord = (float)z / mapHeight * noiseScale;

                float perlinValue = Mathf.PerlinNoise(xCoord, zCoord);

                // ���������, ��������� �� �������� ���� �����
                if (perlinValue > threshold)
                {
                    // �������� ��������� ������
                    GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];

                    // ������� ������ �� ����� ������������ ������ Plane
                    Vector3 position = planeCenter.position + new Vector3(x - mapWidth / 2, 0f, z - mapHeight / 2);
                    Instantiate(prefab, position, Quaternion.Euler(0, Random.Range(0, 180), 0));
                }
            }
        }
    }
}
