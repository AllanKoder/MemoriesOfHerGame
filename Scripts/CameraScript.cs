using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Vector3 Offset;
    public float Max;
    public float AddOnMultiplier;
    public float SpeedFloat;
    public Transform Object;
    private Vector3 TrueDirection;
    private Vector2 Direction;

    // Use this for initialization
    void Start()
    {

    }
    private void Update()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var mousePos = Input.mousePosition;
        mousePos.z = 10;
        if (!UsingController.ControllerConnected)
        {
            Direction = Camera.main.ScreenToWorldPoint(mousePos) - transform.position;
        }
        else
        {
            Direction = new Vector3(Input.GetAxis("RightJoystickX") * 30, Input.GetAxis("RightJoystickY") * -30, 0);
        }
        TrueDirection = Vector2.ClampMagnitude(Direction,Max) * AddOnMultiplier;
        if (!PlayerController.PlayerFalling)
        {
            transform.position = Vector3.Lerp(transform.position, Object.position + Offset + TrueDirection, SpeedFloat * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, Object.position + Offset, SpeedFloat * Time.deltaTime);
        }
    }

    public IEnumerator Shake(float Duration, float Magnitude)
    {

        float Elapsed = 0f;

        while (Elapsed < Duration)
        {
            float ShakeX = Random.Range(-1, 2) * Magnitude;
            float ShakeY = Random.Range(-1, 2) * Magnitude;
            transform.localPosition += new Vector3(ShakeX, ShakeY, 0);

            Elapsed += Time.deltaTime;

            yield return null;
        }

    }
}