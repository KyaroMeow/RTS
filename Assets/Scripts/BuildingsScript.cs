using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsScript : MonoBehaviour
{
    public string Name;
    public int MaxHP;
    public int CurrentHP { get; private set; }
    public Sprite BuildingsIcon;
    void Start()
    {
        CurrentHP = MaxHP;
    }
}
