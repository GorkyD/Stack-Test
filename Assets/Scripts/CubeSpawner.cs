using UnityEngine;

public partial class CubeSpawner : MonoBehaviour
{
    [SerializeField] private MovingCube cubePrefab;
    [SerializeField] private MoveDirection moveDirection;

    public void SpawnCube()
    {
        var cube = Instantiate(cubePrefab);

        if (MovingCube.LastCube != null && MovingCube.LastCube.gameObject != GameObject.Find("Start Area"))
        {
            float x = moveDirection == MoveDirection.X ? transform.position.x : MovingCube.LastCube.transform.position.x;
            float negX = moveDirection == MoveDirection.NegX ? transform.position.x : MovingCube.LastCube.transform.position.x;
            float z = moveDirection == MoveDirection.Z ? transform.position.z : MovingCube.LastCube.transform.position.z;
            float negZ = moveDirection == MoveDirection.NegZ ? transform.position.z : MovingCube.LastCube.transform.position.z;

            if (moveDirection == MoveDirection.X || moveDirection == MoveDirection.Z)
            {
                cube.transform.position = new Vector3(x, MovingCube.LastCube.transform.position.y + cubePrefab.transform.localScale.y, z);
            }
            else
            {
                cube.transform.position = new Vector3(negX, MovingCube.LastCube.transform.position.y + cubePrefab.transform.localScale.y, negZ);
            }    
        }
        else
        {
            cube.transform.position = transform.position;
        }

        cube.MoveDirection = moveDirection;   
    }
}
