using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathCollider : MonoBehaviour
{
    public Vector2 Origin;
    public Vector2 EndPoint;
    public LayerMask PlayerMask;
    public Material EndColor, NoColor;
    public int Value;
    private PlayerController PlayerControl;
    public bool PathToggle = true;
    private Renderer lr;
    LineRenderer PathwayLR;
    // Start is called before the first frame update
    private void Awake()
    {
        lr = GetComponent<LineRenderer>();

    }

    void Start()
    {
        PlayerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        PathwayLR = GetComponent<LineRenderer>();
        lr.material.color = Color.clear;
    }

    // Update is called once per frame
    void Update()
    {
        PathwayLR.SetPosition(0, Origin);
        PathwayLR.SetPosition(1, EndPoint);
        if (Physics2D.Linecast(Origin, EndPoint, PlayerMask) && PathToggle)
        {
            PlayerControl.OnPath[Value] = true;
        }
        else
        {
            PlayerControl.OnPath[Value] = false;
        }

    }
    public void ChangeColor(float speed)
    {
        lr.material.color = Color.Lerp(lr.material.color, EndColor.color, speed * Time.deltaTime);

    }
    public void RemoveColor()
    {
        lr.material.color = Color.clear;
    }
}
