using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureMenu : MonoBehaviour
{
    public GameObject[] treasures;
    public GameObject treasure1;
    public GameObject treasure2;
    public GameObject treasure3;

    void Awake()
    {
        //treasures = Resources.LoadAll("Prefabs/Treasures", typeof(GameObject));
    }

    // Start is called before the first frame update
    void Start()
    {
        // Saves value of our random treasure
        int temp = Random.Range(0, (treasures.Length - 1));
        
        // Saves the treasure and then removes it from the collection
        GameObject selected = treasures[temp];
        //treasures.RemoveAt(temp);

        // Instantiates the treasure
        treasure1 = selected;
        treasure1.SetActive(true);        

        // Saves value of our random treasure
        temp = Random.Range(0, (treasures.Length - 1));

        // Saves the treasure and then removes it from the collection
        selected = treasures[temp];
        //treasures.RemoveAt(temp);

        // Instantiates the treasure
        treasure2 = selected;
        treasure2.SetActive(true);

        // Saves value of our random treasure
        temp = Random.Range(0, (treasures.Length - 1));
        
        // Saves the treasure and then removes it from the collection
        selected = treasures[temp];
        //treasures.RemoveAt(temp);

        // Instantiates the treasure
        treasure3 = selected;
        treasure3.SetActive(true);
    }
}
