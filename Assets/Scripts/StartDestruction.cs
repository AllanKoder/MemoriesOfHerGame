using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDestruction : MonoBehaviour
{
    public float ChanceToSpawn;
    // Start is called before the first frame update
    void Start()
    {
        float RandChance = Random.Range(0f, 1f);
        if (RandChance >= ChanceToSpawn)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
