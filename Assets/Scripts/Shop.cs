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

    public void PurchaseKnightUnit()
    {
        Debug.Log("Knight Unit Purchased");
        buildManager.SetUnitToBuild(buildManager.KnightPrefab);
    }

    public void PurchasePeasantUnit()
    {
        Debug.Log("Peasant Purchased");
        buildManager.SetUnitToBuild(buildManager.PeasantPrefab);
    }

    public void PurchasePriestUnit()
    {
        Debug.Log("Priest Purchased");
        buildManager.SetUnitToBuild(buildManager.PriestPrefab);        
    }
}
