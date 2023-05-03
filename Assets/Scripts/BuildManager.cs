using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("More than one BuildManager");
        }
        instance = this;
    }

    private GameObject unitToBuild;

    public GameObject KnightPrefab;
    public GameObject PeasantPrefab;

    public GameObject GetUnitToBuild()
    {
        return unitToBuild;
    }

    public void SetUnitToBuild (GameObject unit)
    {
        unitToBuild = unit;
    }
}
