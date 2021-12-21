using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    private float offset;

    private void Awake()
    {
        offset = transform.position.y;
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(MovingCube.LastCube.transform.position.x + offset, 
            MovingCube.LastCube.transform.position.y + offset,MovingCube.LastCube.transform.position.z + offset);
    }
}
