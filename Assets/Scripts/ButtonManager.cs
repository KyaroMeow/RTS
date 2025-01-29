using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ButtonManager : MonoBehaviour
{
    public PlayerResources playerResources; // ������ �� ������� ������
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
                // ������ �������
                pair.button.interactable = true;
                var colors = pair.button.colors;
                colors.normalColor = Color.white; // ���� �������� ������
                pair.button.colors = colors;
            }
            else
            {
                // ������ ���������
                pair.button.interactable = false;
                var colors = pair.button.colors;
                colors.normalColor = Color.gray; // ���� ���������� ������
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
