
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnManager : MonoBehaviour
{
    // Update is called once per frame
    public float enemy = 0;
    public GameObject[] dogPrefebs;
    bool isSpawn = true;
    float x;
    int index;
    float nextSpawnTime;
    float spawnCoolDown = 2.5f;
    public InputAction restartAction;
    

    void Start()
    {
        restartAction = InputSystem.actions.FindAction("Interact");
        restartAction.Enable();
    }
    void Update()
    {        
        DogSpawner();
    }

    void DogSpawner()
    {
        bool isRestart = restartAction.WasPressedThisFrame();
        
        if (isRestart)
        {
            enemy = 0;
            isSpawn = true;
            Debug.Log(isRestart);
        }
        else if(enemy == 10)
        {
            isSpawn = false;
        }

        if (isSpawn && Time.time > nextSpawnTime)
        {
            x = Random.Range(-10, 10);
            index = Random.Range(0, dogPrefebs.Length);
            Instantiate(dogPrefebs[index], new Vector3(x, 0, 15), Quaternion.Euler(0,180,0));
            enemy++;
            Debug.Log(enemy);
            nextSpawnTime = Time.time + spawnCoolDown;
        }
    }
}
