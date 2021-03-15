using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeCursor : MonoBehaviour
{
    public GameObject CursorObject;
    // Start is called before the first frame update
    void Start()
    {
        if(GameObject.FindGameObjectWithTag("Cursor") == null)
        {
            Instantiate(CursorObject, CursorObject.transform.position, CursorObject.transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
