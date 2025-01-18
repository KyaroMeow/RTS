using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance;
    public Transform hudParent;
    public GameObject unithudElementPrefab;
    private List<GameObject> hudElements = new List<GameObject>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowUnitHUD(UnitScript unit)
    {
        
        Image unitIcon = unithudElementPrefab.GetComponentInChildren<Image>();
        Slider healthBar = unithudElementPrefab.GetComponentInChildren<Slider>();

        unitIcon.sprite = unit.unitIcon;
        healthBar.maxValue = unit.maxHP;
        healthBar.value = unit.GetCurrentHP();

        hudElements.Add(unithudElementPrefab);
    }

    public void ClearHUD()
    {
        foreach (GameObject hudElement in hudElements)
        {
            Destroy(hudElement);
        }
        hudElements.Clear();
    }
}
