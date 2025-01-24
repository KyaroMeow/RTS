using UnityEngine;
using UnityEngine.AI;

public class Worker : MonoBehaviour
{
    public PlayerResources player; // Экземпляр класса Player
    public float collectionRange = 2f; // Радиус сбора ресурсов
    public float collectionTime = 3f; // Время сбора ресурсов
    public AudioClip collectionSound; // Звук сбора ресурсов
    
    private NavMeshAgent agent;
    private bool isCollecting = false;
    private GameObject targetResource;
    private float collectionTimer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        player = GameObject.FindObjectOfType<PlayerResources>();
    }

    void Update()
    {
        if (isCollecting)
        {
            if (Vector3.Distance(transform.position, targetResource.transform.position) <= collectionRange)
            {
                collectionTimer += Time.deltaTime;
                if (collectionTimer >= collectionTime)
                {
                    CollectResource(targetResource);
                    isCollecting = false;
                    collectionTimer = 0f;
                }
            }
            else
            {
                agent.SetDestination(targetResource.transform.position);
            }
        }
    }

    public void StartCollecting(GameObject resource)
    {
        targetResource = resource;
        isCollecting = true;
        agent.SetDestination(targetResource.transform.position);
    }

    void CollectResource(GameObject resourceObject)
    {
        //if (resourceObject.CompareTag("Wood"))
        //{
        //    player.AddResources(0, 1, 0, 0);
        //}
        //else if (resourceObject.CompareTag("Stone"))
        //{
        //    player.AddResources(0, 0, 0, 1);
        //}
        

        Destroy(resourceObject);
        AudioSource.PlayClipAtPoint(collectionSound, transform.position);
    }
}
