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

    public GameObject standardUnitPrefab;

    private void Start()
    {
        unitToBuild = standardUnitPrefab;
    }

    public GameObject GetTurretToBuild()
    {
        return unitToBuild;
    }
}
