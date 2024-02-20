using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    private bool isWalking;
    private bool movePressed;
    private float horizontalPressed;
    private float verticalPressed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        isWalking = gameObject.GetComponent<Animator>().GetBool("isWalking");
        horizontalPressed = Input.GetAxis("Horizontal");
        verticalPressed = Input.GetAxis("Vertical");
        movePressed = (horizontalPressed != 0) || (verticalPressed != 0);

        if (! isWalking && movePressed) {
            Debug.Log(transform.rotation.eulerAngles.y);
            gameObject.GetComponent<Animator>().SetBool("isWalking", true);
        }

        if (isWalking && ! movePressed) {
            gameObject.GetComponent<Animator>().SetBool("isWalking", false);
        }

        Move();
        RotateWithDirection();
    }

    private void Move()
    {
        transform.Translate(new Vector3(
            Time.deltaTime * 2f * horizontalPressed,
            0,
            Time.deltaTime * 2f * verticalPressed
        ), Space.World);
    }

    private void RotateWithDirection()
    {
        int degree = 0;
        int direction = 0;
        float eulerY = transform.rotation.eulerAngles.y;
        int[] rotation = new int[2] {0, 0};

        if (verticalPressed > 0) {
            rotation = GetRotationByAxis("up", eulerY);
        } else if (verticalPressed < 0) {
            rotation = GetRotationByAxis("down", eulerY);
        } else if (horizontalPressed > 0) {
            rotation = GetRotationByAxis("right", eulerY);
        } else if (horizontalPressed < 0) {
            rotation = GetRotationByAxis("left", eulerY);
        }

        direction = rotation[0];
        degree = rotation[1];

        // If current degree reached correct one, stop rotate.
        if (Math.Round(eulerY) == degree) direction = 0;

        transform.Rotate(new Vector3(
            0,
            Time.deltaTime * 150f * direction,
            0
        ), Space.World);
    }

    private int[] GetRotationByAxis(string axis, float eulerY)
    {
        int[] rotation = new int[2];
        int upDirection = 0;
        int downDirection = 0;
        int leftDirection = 0;
        int rightDirection = 0;

        if (InFloatRange(eulerY, 0, 90)) {
            upDirection = -1;
            downDirection = 1;
            leftDirection = -1;
            rightDirection = 1;
        } else if (InFloatRange(eulerY, 90, 180)) {
            upDirection = -1;
            downDirection = 1;
            leftDirection = 1;
            rightDirection = -1;
        } else if (InFloatRange(eulerY, 180, 270)) {
            upDirection = 1;
            downDirection = -1;
            leftDirection = 1;
            rightDirection = -1;
        } else if (InFloatRange(eulerY, 270, 360)) {
            upDirection = 1;
            downDirection = -1;
            leftDirection = -1;
            rightDirection = 1;
        }

        if (axis == "up") {
            rotation[0] = upDirection;
            rotation[1] = 0;
        } else if (axis == "down") {
            rotation[0] = downDirection;
            rotation[1] = 180;
        }  else if (axis == "right") {
            rotation[0] = rightDirection;
            rotation[1] = 90;
        }  else if (axis == "left") {
            rotation[0] = leftDirection;
            rotation[1] = 270;
        }

        return rotation;
    }

    /// <summary>
    /// Determines whether a float value is in float range.
    /// </summary>
    /// <param name="needle"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    private bool InFloatRange(float needle, float from, float to)
    {
        return needle >= from && needle <= to;
    }
}
