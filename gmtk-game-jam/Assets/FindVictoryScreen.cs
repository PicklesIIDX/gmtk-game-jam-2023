using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindVictoryScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var victoryScreen = GameObject.FindWithTag("Victory").GetComponent<VictoryScreen>();
        GetComponent<Hurtable>().onZero.AddListener((_) => victoryScreen.ShowVictory());

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
