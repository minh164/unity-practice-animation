using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class MainController : MonoBehaviour
{
    public float movingSpeed = 4f;
    public float rotateSpeed = 190f;

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
            gameObject.GetComponent<Animator>().SetBool("isWalking", true);
        }

        if (isWalking && ! movePressed) {
            gameObject.GetComponent<Animator>().SetBool("isWalking", false);
        }

        Move();
        RotateWithDirection();
    }

    /// <summary>
    /// Object moving.
    /// </summary>
    private void Move()
    {
        transform.Translate(new Vector3(
            Time.deltaTime * movingSpeed * horizontalPressed,
            0,
            Time.deltaTime * movingSpeed * verticalPressed
        ), Space.World);
    }

    /// <summary>
    /// Rotates object by direction when moving.
    /// </summary>
    private void RotateWithDirection()
    {
        int degree = 0;
        int direction = 0;
        float eulerY = transform.rotation.eulerAngles.y;
        int[] rotation = new int[2] {0, 0};

        if (verticalPressed > 0) {
            rotation = GetRotationByAxis(MovementAxis.UP, eulerY);
        } else if (verticalPressed < 0) {
            rotation = GetRotationByAxis(MovementAxis.DOWN, eulerY);
        } else if (horizontalPressed > 0) {
            rotation = GetRotationByAxis(MovementAxis.RIGHT, eulerY);
        } else if (horizontalPressed < 0) {
            rotation = GetRotationByAxis(MovementAxis.LEFT, eulerY);
        }

        direction = rotation[0];
        degree = rotation[1];

        // If current degree reached, stop rotate.
        if (Math.Round(eulerY) == degree) direction = 0;

        transform.Rotate(new Vector3(
            0,
            Time.deltaTime * rotateSpeed * direction,
            0
        ), Space.World);
    }

    /// <summary>
    /// Get rotation info (rotate direction, rotate degree) by current axis of object.
    /// </summary>
    /// <param name="axis"></param>
    /// <param name="eulerY"></param>
    /// <returns></returns>
    private int[] GetRotationByAxis(string axis, float eulerY)
    {
        int[] rotation = new int[2];

        // Rotate directions when they are pressed.
        int upDirection = 0;
        int downDirection = 0;
        int leftDirection = 0;
        int rightDirection = 0;

        // Seperate the circle to some euler parts and define direction base on each part.
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

        // Get rotation by pressed axis.
        switch (axis) {
            case MovementAxis.UP:
                rotation[0] = upDirection;
                rotation[1] = 0;
                break;
            case MovementAxis.DOWN:
                rotation[0] = downDirection;
                rotation[1] = 180;
                break;
            case MovementAxis.RIGHT:
                rotation[0] = rightDirection;
                rotation[1] = 90;
                break;
            case MovementAxis.LEFT:
                rotation[0] = leftDirection;
                rotation[1] = 270;
                break;
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
