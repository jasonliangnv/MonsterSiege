using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    public TMP_Text statsText;
    public Image unitIcon;


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

    private void Update()
    {
        if(unitIcon.sprite == null)
        {
            unitIcon.gameObject.SetActive(false);
        }
        else
        {
            unitIcon.gameObject.SetActive(true);
        }

        if(unitToBuild != null)
        {
            AlliedAI unitStats = unitToBuild.GetComponent<AlliedAI>();
            unitIcon.sprite = unitStats.icon;
            statsText.text = "Unit: " + unitStats.name.ToString() + "\n" +
                "Attack: " + unitStats.damage.ToString() + "\n" + 
                "Range: " + unitStats.range.ToString() + "\n" +
                "Cost: " + unitStats.cost.ToString();
        }
    }
}
