using UnityEngine;

public class Des : MonoBehaviour
{
    bool isHit;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isHit = GameObject.FindGameObjectsWithTag("Enemy").Length > 0;
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit");
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
