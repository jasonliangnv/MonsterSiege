using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LivesUI : MonoBehaviour
{
    public TMP_Text healthText;

    void Update()
    {
        healthText.text = PlayerStats.health.ToString();
    }
}
