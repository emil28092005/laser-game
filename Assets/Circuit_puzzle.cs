using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Circuit_puzzle : MonoBehaviour
{
    public List<Circuit> battaries;

    public LevelManager levelManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool allOn = true;
        foreach (var battery in battaries)
        {
            if (!battery.isOn)
            {
                allOn = false;
                break;
            }
        }

        if (allOn)
        {
            levelManager.LevelFinishEvent();
        }
    }

}
