using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.instance;
    }


    public void PurchaseStandardUnit()
    {
        Debug.Log("Standard Unit Purchased");
        buildManager.SetUnitToBuild(buildManager.standardUnitPrefab);
    }

    public void PurchaseOtherUnit()
    {
        Debug.Log("Other Unit Purchased");
        buildManager.SetUnitToBuild(buildManager.otherUnitPrefab);
    }
}
