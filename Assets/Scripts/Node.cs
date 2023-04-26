using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Color hoverColor;

    private GameObject unit;

    private Renderer rend;
    private Color startColor;


    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    private void OnMouseDown()
    {
        if(unit != null)
        {
            return;
        }

        GameObject unitToBuild = BuildManager.instance.GetTurretToBuild();
        int unitCost = unitToBuild.GetComponent<AlliedAI>().cost;

        if (unitCost <= PlayerStats.money)
        {
            unit = Instantiate(unitToBuild, transform.position, transform.rotation);
            PlayerStats.money -= unitCost;
        }
        else
        {
            Debug.Log("Not Enough Money");
        }
    }

    void OnMouseEnter()
    {
        rend.material.color = hoverColor;
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
