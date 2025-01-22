using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.VisualScripting;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance;
    public Transform hudParent;
    public GameObject OneUnitSelectedPanel;
    public GameObject unithudElementPrefab;
    public GameObject actionPanelWorker;
    public GameObject actionPanelWarrior;
    public GameObject actionPanelHealer;
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
        ClearHUD(); // Очистим текущие элементы HUD

        if (selectedUnits.Count > 1)
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
        else if (selectedUnits.Count == 1)
        {
            OneUnitSelectedPanel.SetActive(true);
            Image unitIcon = OneUnitSelectedPanel.GetComponentInChildren<Image>();
            Text unitName = OneUnitSelectedPanel.GetComponentInChildren<Text>();
            Slider unitSlider = OneUnitSelectedPanel.GetComponentInChildren<Slider>();
            GameObject unit = selectedUnits[0];
            UnitScript unitScript = unit.GetComponent<UnitScript>();
            if (unitScript != null)
            {
                unitIcon.sprite = unitScript.unitIcon;
                unitName.text = unitScript.Name;
                unitSlider.maxValue = unitScript.maxHP;
                unitSlider.value = unitScript.GetCurrentHP();
                switch (unitScript.Name)
                {
                    case "Villager":
                        actionPanelWorker.SetActive(true);
                        break;
                    case "Priest":
                        actionPanelHealer.SetActive(true);
                        break;
                    default:
                        actionPanelWarrior.SetActive(true);
                        break;
                }
            }
        }
    }

    public void ClearHUD()
    {
        foreach (GameObject hudElement in hudElements)
        {
            Destroy(hudElement);
        }
        hudElements.Clear();
        actionPanelHealer.SetActive(false);
        actionPanelWarrior.SetActive(false);
        actionPanelWorker.SetActive(false);
        OneUnitSelectedPanel.SetActive(false);
    }
}
