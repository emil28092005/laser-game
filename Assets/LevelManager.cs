using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemiesList;
    public UnityEvent onLevelComplete;

    public UnityEvent levelCompleteCondition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

    void Update()
    {
        levelCompleteCondition?.Invoke();
    }

    public void CheckIfNoEnemies()
    {
        if (enemiesList.Count == 0)
        {
            LevelFinishEvent();
        }
    }
    public void AddEnemyToList(GameObject enemy)
    {
        enemiesList.Add(enemy);
    }
    
    public void RemoveEnemyFromList(GameObject enemy)
    {
        if (enemiesList.Contains(enemy))
        {
            enemiesList.Remove(enemy);
        }
    }

    public void LevelFinishEvent()
    {
        onLevelComplete?.Invoke();
    }

    public void OpenDoor(Door door)
    {
        door.isOpened = true;
    }
    
}
