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
            Debug.Log("Can't Build Here");
        }

        GameObject unitToBuild = BuildManager.instance.GetTurretToBuild();
        unit = Instantiate(unitToBuild, transform.position, transform.rotation);
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
