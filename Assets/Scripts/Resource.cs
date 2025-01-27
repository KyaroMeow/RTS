using UnityEngine;

public class Resource : MonoBehaviour
{
    public int resourceAmount = 10;
    public ResourceType resourceType;

    public void Collect(GameObject unit)
    {
        resourceAmount--;
        if (resourceAmount <= 0)
        {
            Destroy(gameObject);
        }

        PlayerResources playerResources = FindObjectOfType<PlayerResources>();
        switch (resourceType)
        {
            case ResourceType.Food:
                playerResources.AddResources(1, 0, 0, 0);
                break;
            case ResourceType.Wood:
                playerResources.AddResources(0, 1, 0, 0);
                break;
            case ResourceType.Iron:
                playerResources.AddResources(0, 0, 1, 0);
                break;
            case ResourceType.Stone:
                playerResources.AddResources(0, 0, 0, 1);
                break;
        }
    }
}

public enum ResourceType
{
    Food,
    Wood,
    Iron,
    Stone
}
