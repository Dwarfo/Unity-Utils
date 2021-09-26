using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private WeightedRandomGen<int> wrg;
    void Start()
    {
        Dictionary<int,int> itemToWeight = new Dictionary<int,int>{
            {2 , 10},
            {7 , 30},
            {3 , 20},
            {1 , 10},
            {4, 20}
        };
        wrg = new WeightedRandomGen<int>(itemToWeight);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(wrg.GetRandomItem());
    }
}
