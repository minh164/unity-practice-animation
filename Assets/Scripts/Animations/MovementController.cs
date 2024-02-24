using System;
using UnityEngine;
using Helpers;
using Constants;
using System.Collections.Generic;

public class MovementController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float rotateSpeed = 190f;

    private float movingSpeed;
    private bool isWalking;
    private bool isRunning;
    private bool movePressed;
    private float horizontalPressed;
    private float verticalPressed;
    private bool runPressed;

    // Start is called before the first frame update
    void Start()
    {
        movingSpeed = walkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        isWalking = gameObject.GetComponent<Animator>().GetBool("isWalking");
        isRunning = gameObject.GetComponent<Animator>().GetBool("isRunning");
        horizontalPressed = Input.GetAxis("Horizontal");
        verticalPressed = Input.GetAxis("Vertical");
        movePressed = (horizontalPressed != 0) || (verticalPressed != 0);
        runPressed = Input.GetKey(KeyCode.LeftShift);

        SwitchMovementState();
        Move();
        RotateWithDirection();
    }

    /// <summary>
    /// Switch between movement states.
    /// </summary>
    private void SwitchMovementState()
    {
        // If state is idle and movement is pressed, switch to walking.
        if (! isWalking && movePressed) {
            gameObject.GetComponent<Animator>().SetBool("isWalking", true);
        }

        // If state is walking and release movement, switch to idle.
        if (isWalking && ! movePressed) {
            gameObject.GetComponent<Animator>().SetBool("isWalking", false);
        }

        // If state is walking and press run, switch to running.
        if (! isRunning && movePressed && runPressed) {
            movingSpeed = runSpeed;
            gameObject.GetComponent<Animator>().SetBool("isRunning", true);
        }

        // If state is running and release movement/run, switch to walking
        if (isRunning && (! movePressed || ! runPressed)) {
            movingSpeed = walkSpeed;
            gameObject.GetComponent<Animator>().SetBool("isRunning", false);
        }
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
    /// <param name="axis"><see cref="MovementAxis"/></param>
    /// <param name="eulerY">Y rotation degree to get direction</param>
    /// <returns></returns>
    private int[] GetRotationByAxis(string axis, float eulerY)
    {
        int[] rotation = new int[2];

        var rotateDirections = GetRotateDirections(eulerY);
        var rotateDegrees = GetRotateDegrees(axis);

        string upAxis = MovementAxis.UP;
        string downAxis = MovementAxis.DOWN;
        string rightAxis = MovementAxis.RIGHT;
        string leftAxis = MovementAxis.LEFT;
    
        // Get rotation by pressed axis.
        switch (axis) {
            case MovementAxis.UP:
                rotation[0] = rotateDirections[upAxis];
                rotation[1] = rotateDegrees[upAxis];
                break;
            case MovementAxis.DOWN:
                rotation[0] = rotateDirections[downAxis];
                rotation[1] = rotateDegrees[downAxis];
                break;
            case MovementAxis.RIGHT:
                rotation[0] = rotateDirections[rightAxis];
                rotation[1] = rotateDegrees[rightAxis];
                break;
            case MovementAxis.LEFT:
                rotation[0] = rotateDirections[leftAxis];
                rotation[1] = rotateDegrees[leftAxis];
                break;
        }

        return rotation;
    }

    /// <summary>
    /// Get rotate direction list bases on current Y rotation of object.
    /// </summary>
    /// <param name="eulerY">Y rotation degree to get direction</param>
    /// <returns></returns>
    private Dictionary<string, int> GetRotateDirections(float eulerY)
    {
        // Rotate directions when they are pressed.
        Dictionary<string, int> rotateDirections = new Dictionary<string, int>() {
            {MovementAxis.UP, 0}, {MovementAxis.DOWN, 0}, {MovementAxis.LEFT, 0}, {MovementAxis.RIGHT, 0},
        };

        // Seperate the circle to some euler parts and define rotate directions base on each part.
        if (BaseHelper.InFloatRange(eulerY, 0, 90)) {
            rotateDirections = new Dictionary<string, int>() {
                {MovementAxis.UP, -1}, {MovementAxis.DOWN, 1}, {MovementAxis.LEFT, -1}, {MovementAxis.RIGHT, 1},
            };
        } else if (BaseHelper.InFloatRange(eulerY, 90, 180)) {
            rotateDirections = new Dictionary<string, int>() {
                {MovementAxis.UP, -1}, {MovementAxis.DOWN, 1}, {MovementAxis.LEFT, 1}, {MovementAxis.RIGHT, -1},
            };
        } else if (BaseHelper.InFloatRange(eulerY, 180, 270)) {
            rotateDirections = new Dictionary<string, int>() {
                {MovementAxis.UP, 1}, {MovementAxis.DOWN, -1}, {MovementAxis.LEFT, 1}, {MovementAxis.RIGHT, -1},
            };
        } else if (BaseHelper.InFloatRange(eulerY, 270, 360)) {
            rotateDirections = new Dictionary<string, int>() {
                {MovementAxis.UP, 1}, {MovementAxis.DOWN, -1}, {MovementAxis.LEFT, -1}, {MovementAxis.RIGHT, 1},
            };
        }

        return rotateDirections;
    }

    /// <summary>
    /// Get rotate degree list bases on axis.
    /// </summary>
    /// <param name="axis"><see cref="MovementAxis"/></param>
    /// <returns></returns>
    private Dictionary<string, int> GetRotateDegrees(string axis)
    {
        Dictionary<string, int> rotateDegrees = new Dictionary<string, int>() {
            {MovementAxis.UP, 0},
            {MovementAxis.DOWN, 180},
            {MovementAxis.RIGHT, 90},
            {MovementAxis.LEFT, 270},
        };

        return rotateDegrees;
    }
}
