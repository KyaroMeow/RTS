using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ButtonManager : MonoBehaviour
{
    public PlayerResources playerResources; // —сылка на ресурсы игрока
    public List<ButtonBuildingPair> buttonBuildingPairs = new List<ButtonBuildingPair>();

    void Update()
    {
            UpdateButtonStates();
    }
    void UpdateButtonStates()
    {
        foreach (var pair in buttonBuildingPairs)
        {
            bool canBuild = playerResources.woodCount >= pair.building.woodCost &&
                            playerResources.stoneCount >= pair.building.stoneCost;
            if (canBuild)
            {
                //  нопка активна
                pair.button.interactable = true;
                var colors = pair.button.colors;
                colors.normalColor = Color.white; // ÷вет активной кнопки
                pair.button.colors = colors;
            }
            else
            {
                //  нопка неактивна
                pair.button.interactable = false;
                var colors = pair.button.colors;
                colors.normalColor = Color.gray; // ÷вет неактивной кнопки
                pair.button.colors = colors;
            }
        }
    }
}
[System.Serializable]
public class ButtonBuildingPair
{
    public Button button;
    public BuildingsScript building;
}
