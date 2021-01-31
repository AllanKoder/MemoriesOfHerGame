using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    public GameObject Thecursor;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Thecursor.transform.position = Input.mousePosition;
    }
}
