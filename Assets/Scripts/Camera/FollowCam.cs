using UnityEngine;

public class FollowCam : MonoBehaviour
{
    private Vector3 offset;

    private void Start()
    {
        offset = transform.position;
    }

    private void LateUpdate()
    {
        MoveCam();    
    }

    private void MoveCam()
    {
        if (MovingCube.LastCube != null)
        {
            transform.position = Vector3.MoveTowards(transform.position,MovingCube.LastCube.transform.position + offset,Time.deltaTime / 2);
        }
    }
}
