using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    private bool isWalking;
    private bool movePressed;
    private bool forwardPressed;
    private bool backPressed;
    private bool leftPressed;
    private bool rightPressed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        isWalking = gameObject.GetComponent<Animator>().GetBool("isWalking");
        forwardPressed = Input.GetKey(KeyCode.W);
        backPressed = Input.GetKey(KeyCode.S);
        leftPressed = Input.GetKey(KeyCode.A);
        rightPressed = Input.GetKey(KeyCode.D);
        movePressed =  forwardPressed || backPressed || rightPressed || leftPressed;

        if (! isWalking && movePressed) {
            MoveDirection();
            gameObject.GetComponent<Animator>().SetBool("isWalking", true);
        }

        if (isWalking && ! movePressed) {
            gameObject.GetComponent<Animator>().SetBool("isWalking", false);
        }

        
        Move();
    }

    private void Move()
    {
        transform.Translate(new Vector3(
            Time.deltaTime * 2f * Input.GetAxis("Horizontal"),
            0,
            Time.deltaTime * 2f * Input.GetAxis("Vertical")
        ), Space.World);
    }

    private void MoveDirection()
    {
        if (forwardPressed) {
            ChangeRotationY(0);
        } else if (backPressed) {
            ChangeRotationY(180);
        } else if (rightPressed) {
            ChangeRotationY(90);
        } else if (leftPressed) {
            ChangeRotationY(-90);
        }
    }

    private void ChangeRotationY(float y)
    {
        var rotation = transform.rotation.eulerAngles;
        rotation.y = y;
        transform.rotation = Quaternion.Euler(rotation);
    }
}
