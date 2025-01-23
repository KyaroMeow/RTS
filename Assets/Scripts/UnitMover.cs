using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class UnitMover : MonoBehaviour
{
    public Camera mainCamera;

    void Update()
    {
        HandleMovement();
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
}
