using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Transform player;

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position + new Vector3(0,9f,-14f);
    }
}
