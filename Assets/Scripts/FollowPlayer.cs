using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    private Vector3 initPosition;

    // Start is called before the first frame update
    void Start()
    {
        initPosition = transform.position;
    }

    void LateUpdate()
    {
        transform.position = initPosition + player.transform.position;    
    }
}
