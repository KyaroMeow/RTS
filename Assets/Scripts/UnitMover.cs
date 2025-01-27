using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class UnitMover : MonoBehaviour
{
    public Camera mainCamera;
    private GameObject targetResource;

    void Update()
    {
        HandleMovement();
        CheckResourceCollection();
    }

    void HandleMovement()
    {
        if (UnitSelector.SelectedUnits.Count > 0 && Input.GetMouseButtonDown(1))
        {
            if (UnitSelector.SelectedUnits[0].CompareTag("Unit"))
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    foreach (GameObject unit in UnitSelector.SelectedUnits)
                    {
                        NavMeshAgent agent = unit.GetComponent<NavMeshAgent>();
                        if (agent != null)
                        {
                            agent.SetDestination(hit.point);
                        }
                    }
                }
            }
        }
    }

    public void MoveToResource(GameObject resource)
    {
        targetResource = resource;
        foreach (GameObject unit in UnitSelector.SelectedUnits)
        {
            NavMeshAgent agent = unit.GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                agent.SetDestination(resource.transform.position);
            }
        }
    }

    void CheckResourceCollection()
    {
        if (targetResource != null)
        {
            foreach (GameObject unit in UnitSelector.SelectedUnits)
            {
                float distance = Vector3.Distance(unit.transform.position, targetResource.transform.position);
                if (distance <= 1f)
                {
                    Resource resourceScript = targetResource.GetComponent<Resource>();
                    resourceScript.Collect(unit);
                    targetResource = null;
                }
            }
        }
    }
}
