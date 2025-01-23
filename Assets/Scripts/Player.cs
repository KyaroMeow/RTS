using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string Name;
    public int unitCount { get; private set;}
    public int food;
    public int wood;
    public int iron;
    public int stone;
    public List<GameObject> buildings = new List<GameObject>();
    public List<GameObject> units = new List<GameObject>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        unitCount = units.Count;
    }
}
