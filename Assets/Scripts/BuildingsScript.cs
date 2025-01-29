using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsScript : MonoBehaviour
{
    public string Name;
    public int MaxHP;
    public int woodCost;
    public int stoneCost;
    public float buildTime;
    public int CurrentHP { get; private set; }
    public Sprite BuildingsIcon;
    void Start()
    {
        CurrentHP = MaxHP;
    }
}
