using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResources : MonoBehaviour
{
    public string Name;
    public int unitCount { get; private set;}
    public int foodCount;
    public int woodCount;
    public int ironCount;
    public int stoneCount;
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
    public void AddResources(int food,int wood,int iron,int stone)
    {
        foodCount += food;
        woodCount += wood;
        ironCount += iron;
        stoneCount += stone;
    }
}
