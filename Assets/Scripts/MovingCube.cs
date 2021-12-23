using UnityEngine;
using UnityEngine.SceneManagement;

public class MovingCube : MonoBehaviour
{
    #region SerializeField
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private GameObject pointX;
    [SerializeField] private GameObject pointZ;
    [SerializeField] private GameObject pointNegX;
    [SerializeField] private GameObject pointNegZ;
    #endregion

    #region Bool
    private bool moveZ;
    private bool moveX;
    private bool moveNegZ;
    private bool moveNegX;
    #endregion

    #region Public
    public static MovingCube CurrentCube { get; private set;}
    public static MovingCube LastCube { get; private set; }

    public CubeSpawner.MoveDirection MoveDirection;
    #endregion

    private void Awake()
    {
        if (LastCube == null)
        {
            LastCube = GameObject.Find("Start Area").GetComponent<MovingCube>();
        }
        CurrentCube = this;

        GetComponent<Renderer>().material.color = RandomColor.GetRandomColor();

        transform.localScale = new Vector3(LastCube.transform.localScale.x, transform.localScale.y, LastCube.transform.localScale.z);
    }

    private void FixedUpdate()
    {
        CycleMoving();
    }

    private void CycleMoving()
    {
        if (MoveDirection == CubeSpawner.MoveDirection.Z)
        {
            if (moveZ == false)
            {
                transform.position += transform.forward * Time.deltaTime * speed;
                if (transform.position.z > pointNegZ.transform.position.z)
                {
                    moveZ = true;
                }
            }
            else if (moveZ == true)
            {
                transform.position -= transform.forward * Time.deltaTime * speed;
                if (transform.position.z < pointZ.transform.position.z)
                {
                    moveZ = false;
                }
            }
        }
        else if (MoveDirection == CubeSpawner.MoveDirection.X)
        {
            if (moveX == false)
            {
                transform.position += transform.right * Time.deltaTime * speed;
                if (transform.position.x > pointNegX.transform.position.x)
                {
                    moveX = true;
                }
            }
            else if (moveX == true)
            {
                transform.position -= transform.right * Time.deltaTime * speed;
                if (transform.position.x < pointX.transform.position.x)
                {
                    moveX = false;
                }
            }
        }
        else if (MoveDirection == CubeSpawner.MoveDirection.NegZ)
        {
            if (moveNegZ == false)
            {
                transform.position -= transform.forward * Time.deltaTime * speed;
                if (transform.position.z < pointZ.transform.position.z)
                {
                    moveNegZ = true;
                }
            }
            else if (moveNegZ == true)
            {
                transform.position += transform.forward * Time.deltaTime * speed;
                if (transform.position.z > pointNegZ.transform.position.z)
                {
                    moveNegZ = false;
                }
            }
        }
        else if (MoveDirection == CubeSpawner.MoveDirection.NegX)
        {
            if (moveNegX == false)
            {
                transform.position -= transform.right * Time.deltaTime * speed;
                if (transform.position.x < pointX.transform.position.x)
                {
                    moveNegX = true;
                }
            }
            else if (moveNegX == true)
            {
                transform.position += transform.right * Time.deltaTime * speed;
                if (transform.position.x > pointNegX.transform.position.x)
                {
                    moveNegX = false;
                }
            }
        }
    }

    public void Stop()
    {
        speed = 0f;

        float hangOver = GetHangover();

        float max = MoveDirection == CubeSpawner.MoveDirection.Z 
            || MoveDirection == CubeSpawner.MoveDirection.NegZ ? LastCube.transform.localScale.z : LastCube.transform.localScale.x;

        if (Mathf.Abs(hangOver) >= max)
        {
            LastCube = null;
            CurrentCube = null;
            SceneManager.LoadScene(0);
        }

        float direction = hangOver > 0 ? 1f : -1f;

        if (MoveDirection == CubeSpawner.MoveDirection.Z || MoveDirection == CubeSpawner.MoveDirection.NegZ)
        {
            SliceCubeOnZ(hangOver, direction);
        }
        else
        {
            SliceCubeOnX(hangOver, direction);
        }
        
        LastCube = this;
    }

    private float GetHangover()
    {
        if (MoveDirection == CubeSpawner.MoveDirection.Z || MoveDirection == CubeSpawner.MoveDirection.NegZ)
        {
            return transform.position.z - LastCube.transform.position.z;
        }
        else
        {
            return transform.position.x - LastCube.transform.position.x;
        }
        
    }

    private void SliceCubeOnX(float hangOver, float direction)
    {
        float newCubeSizeX = LastCube.transform.localScale.x - Mathf.Abs(hangOver);
        float fallingCubeSize = transform.localScale.x - newCubeSizeX;

        float newCubePosition = LastCube.transform.position.x + (hangOver / 2);
        transform.localScale = new Vector3(newCubeSizeX, transform.localScale.y, transform.localScale.z);
        transform.position = new Vector3(newCubePosition, transform.position.y, transform.position.z);

        float fallingCubeEdge = transform.position.x + (newCubeSizeX / 2 * direction);
        float fallingCubePositionX = fallingCubeEdge + fallingCubeSize / 2f * direction;

        SpawnSlicedCube(fallingCubePositionX, fallingCubeSize);
    }
    private void SliceCubeOnZ(float hangOver, float direction)
    {
        float newCubeSizeZ = LastCube.transform.localScale.z - Mathf.Abs(hangOver);
        float fallingCubeSize = transform.localScale.z - newCubeSizeZ;

        float newCubePosition = LastCube.transform.position.z + (hangOver / 2);
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, newCubeSizeZ);
        transform.position = new Vector3(transform.position.x,transform.position.y,newCubePosition);

        float fallingCubeEdge = transform.position.z + (newCubeSizeZ / 2 * direction);
        float fallingCubePositionZ = fallingCubeEdge + fallingCubeSize / 2f * direction;

        SpawnSlicedCube(fallingCubePositionZ, fallingCubeSize);
    }

    private void SpawnSlicedCube(float fallingCubePositionZ, float fallingCubeSize)
    {
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        if (MoveDirection == CubeSpawner.MoveDirection.Z || MoveDirection == CubeSpawner.MoveDirection.NegZ)
        {
            cube.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, fallingCubeSize);
            cube.transform.position = new Vector3(transform.position.x, transform.position.y, fallingCubePositionZ);
        }
        else
        {
            cube.transform.localScale = new Vector3(fallingCubeSize, transform.localScale.y, transform.localScale.z);
            cube.transform.position = new Vector3(fallingCubePositionZ, transform.position.y, transform.position.z);
        }

        cube.AddComponent<Rigidbody>();
        cube.GetComponent<Renderer>().material.color = GetComponent<Renderer>().material.color;
        Destroy(cube.gameObject,2f);
    }

}
