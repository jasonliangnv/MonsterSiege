using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;

    private GameObject unit;

    private Renderer rend;
    private Color startColor;
    private Vector3 offset;

    BuildManager buildManager;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
    }

    private void OnMouseDown()
    {
        if(buildManager.GetUnitToBuild() == null)
        {
            return;
        }

        if(GameManager.paused == true)
        {
            return;
        }

        if(unit != null)
        {
            return;
        }

        GameObject unitToBuild = buildManager.GetUnitToBuild();
        int unitCost = unitToBuild.GetComponent<AlliedAI>().cost;

        if (unitCost <= PlayerStats.money)
        {
            unit = Instantiate(unitToBuild, transform.position, transform.rotation);
            PlayerStats.money -= unitCost;
            GameObject effect = Instantiate(buildManager.buildEffect, gameObject.transform.position , Quaternion.identity);
            Destroy(effect, 5f);
        }
        else
        {
            Debug.Log("Not Enough Money");
        }
    }

    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (buildManager.GetUnitToBuild() == null)
        {
            return;
        }

        rend.material.color = hoverColor;
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
