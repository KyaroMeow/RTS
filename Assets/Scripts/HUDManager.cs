using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance;
    public Transform hudParent;
    public GameObject unithudElementPrefab;
    public List<GameObject> hudElements = new List<GameObject>();

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

    public void ShowUnitHUD(List<GameObject> selectedUnits)
    {
        foreach (GameObject unit in selectedUnits)
        {
            // Создаем экземпляр префаба
            GameObject hudElement = Instantiate(unithudElementPrefab, hudParent);
            Image unitIcon = hudElement.GetComponentInChildren<Image>();
            Slider healthBar = hudElement.GetComponentInChildren<Slider>();

            UnitScript unitScript = unit.GetComponent<UnitScript>();
            if (unitScript != null)
            {
                unitIcon.sprite = unitScript.unitIcon;
                healthBar.maxValue = unitScript.maxHP;
                healthBar.value = unitScript.GetCurrentHP();
            }

            hudElements.Add(hudElement);
        }
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
