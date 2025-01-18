using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitScript : MonoBehaviour
{
    public Sprite unitIcon;
    public int maxHP;
    private int currentHP;

    void Start()
    {
        currentHP = maxHP;
    }
    public int GetCurrentHP()
    {
        return currentHP;
    }
    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            currentHP = 0;
        }
    }
}
