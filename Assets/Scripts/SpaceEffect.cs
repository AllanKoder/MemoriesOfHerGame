using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceEffect : MonoBehaviour
{
    public float Speed;
    MeshRenderer mr;
    // Use this for initialization
    void Start()
    {
        mr = GetComponent<MeshRenderer>();

    }

    // Update is called once per frame
    void Update()
    {

        Material mat = mr.material;

        Vector2 offset = mat.mainTextureOffset;

        offset.x = transform.position.x / transform.localScale.x / Speed;
        offset.y = transform.position.y / transform.localScale.y / Speed;

        mat.mainTextureOffset = offset;
    }
}