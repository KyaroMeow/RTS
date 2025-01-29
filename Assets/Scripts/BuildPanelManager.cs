using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BuildPanelManager : MonoBehaviour
{
    private bool IsActive = false;

    public void SetActivePanel()
    {
        IsActive = !IsActive;
        GameObject go = gameObject;
        go.SetActive(IsActive);
    }
}
