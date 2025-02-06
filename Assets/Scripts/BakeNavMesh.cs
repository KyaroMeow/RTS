using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.AI.Navigation;
using UnityEngine;

public class BakeNavMesh : MonoBehaviour
{
    public GameObject plane;
     
    void Start()
    {
        
        BakeNavMeshSurface();
    }
        void BakeNavMeshSurface()
        {
            NavMeshSurface navMeshSurface = plane.GetComponent<NavMeshSurface>();
            if (navMeshSurface == null)
            {
                navMeshSurface = plane.AddComponent<NavMeshSurface>();
            }
            navMeshSurface.BuildNavMesh();
        }
}
