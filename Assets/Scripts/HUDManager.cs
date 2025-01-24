using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.VisualScripting;
using System;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance;
    public Transform hudParent;
    public GameObject OneUnitSelectedPanel;
    public GameObject unithudElementPrefab;
    public GameObject actionPanelWorker;
    public GameObject actionPanelWarrior;
    public GameObject actionPanelHealer;
    public GameObject resoursesPanel;
    public List<GameObject> hudElements = new List<GameObject>();
    public Text unitCount;
    public Text foodCount;
    public Text woodCount;
    public Text ironCount;
    public Text stoneCount;
    public Player player;

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
    private void Update()
    {
        ShowResurses();
    }

    void ShowResurses()
    {
        unitCount.text = Convert.ToString(player.unitCount);
        foodCount.text = Convert.ToString(player.foodCount);
        woodCount.text = Convert.ToString(player.woodCount);
        ironCount.text = Convert.ToString(player.ironCount);
        stoneCount.text = Convert.ToString(player.stoneCount);
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
            if (unit.CompareTag("Unit"))
            {
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
            if (unit.CompareTag("Buildings"))
            {
                BuildingsScript buildingsScript = unit.GetComponent<BuildingsScript>();
                if (buildingsScript != null)
                {
                    unitIcon.sprite = buildingsScript.BuildingsIcon;
                    unitName.text = buildingsScript.Name;
                    unitSlider.maxValue = buildingsScript.MaxHP;
                    unitSlider.value = buildingsScript.CurrentHP;
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
