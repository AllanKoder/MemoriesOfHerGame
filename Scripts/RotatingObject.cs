using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObject : MonoBehaviour
{
    public float Speed = 4f;
    public float Range = 360f;

    private Vector3 RandomPower;
    // Start is called before the first frame update
    void Start()
    {
        RandomPower = new Vector3(Random.Range(-Speed, Speed), Random.Range(-Speed, Speed), Random.Range(-Speed, Speed));
        transform.Rotate(new Vector3(Random.Range(-Range, Range), Random.Range(-Range, Range), Random.Range(-Range, Range)));
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(RandomPower * Time.deltaTime);
    }
}
