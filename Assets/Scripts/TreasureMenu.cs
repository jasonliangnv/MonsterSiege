using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureMenu : MonoBehaviour
{
    public List<GameObject> treasures;
    public GameObject treasure1;
    public GameObject treasure2;
    public GameObject treasure3;

    private GameObject panel;

    void Awake()
    {
        //treasures = Resources.LoadAll("Prefabs/Treasures", typeof(GameObject));
    }


    // Start is called before the first frame update
    void Start()
    {
        panel = GameObject.Find("Treasures");
        
        // Saves value of our random treasure
        int temp = Random.Range(0, (treasures.Count - 1));
        
        // Saves the treasure and then removes it from the collection
        GameObject selected = treasures[temp];
        treasures.RemoveAt(temp);

        // Instantiates the treasure
        treasure1 = selected;
        Instantiate(treasure1, panel.transform);    

        // Saves value of our random treasure
        temp = Random.Range(0, (treasures.Count - 1));

        // Saves the treasure and then removes it from the collection
        selected = treasures[temp];
        treasures.RemoveAt(temp);

        // Instantiates the treasure
        treasure2 = selected;
        Instantiate(treasure2, panel.transform);

        // Saves value of our random treasure
        temp = Random.Range(0, (treasures.Count - 1));
        
        // Saves the treasure and then removes it from the collection
        selected = treasures[temp];
        treasures.RemoveAt(temp);

        // Instantiates the treasure
        treasure3 = selected;
        Instantiate(treasure3, panel.transform);
    }
}
