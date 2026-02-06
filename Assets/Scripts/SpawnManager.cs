
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
    

    void Start()
    {
        StartCoroutine(DogSpawner());
    }
    void Update()
    {
        
        if(enemy == 10)
        {
            isSpawn = false;
        }
    }

    IEnumerator DogSpawner()
    {
        while (isSpawn)
        {
            x = Random.Range(-10, 10);
            index = Random.Range(0, dogPrefebs.Length);
            Instantiate(dogPrefebs[index], new Vector3(x, 0, 15), Quaternion.Euler(0,180,0));
            enemy++;
            yield return new WaitForSeconds(3f);
        }
    }
}
