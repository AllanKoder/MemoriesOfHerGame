using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawnPos : MonoBehaviour
{
    public float IncreaseX, IncreaseY;
    // Start is called before the first frame update
    void Start()
    {
        transform.position += new Vector3(Random.Range(-IncreaseX, IncreaseX), Random.Range(-IncreaseY, IncreaseY), 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
