using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovingCube : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;

    public static MovingCube CurrentCube { get; private set;}
    public static MovingCube LastCube { get; private set; }
    public CubeSpawner.MoveDirection MoveDirection { get; set; }

    private void Awake()
    {
        if (LastCube == null)
        {
            LastCube = GameObject.Find("Start Area").GetComponent<MovingCube>();
        }

        CurrentCube = this;
        GetComponent<Renderer>().material.color = GetRandomColor();

        transform.localScale = new Vector3(LastCube.transform.localScale.x, transform.localScale.y, LastCube.transform.localScale.z);
    }

    private void Update()
    {
        if (MoveDirection == CubeSpawner.MoveDirection.Z)
        {
            transform.position += transform.forward * Time.deltaTime * speed;
        }
        else
        {
            transform.position += transform.right * Time.deltaTime * speed;
        }
    }

    internal void Stop()
    {
        speed = 0f;

        float hangOver = GetHangover();

        float max = MoveDirection == CubeSpawner.MoveDirection.Z ? LastCube.transform.localScale.z : LastCube.transform.localScale.x;

        if (Mathf.Abs(hangOver) >= max)
        {
            LastCube = null;
            CurrentCube = null;

            SceneManager.LoadScene(0);
        }

        float direction = hangOver > 0 ? 1f : -1f;

        if (MoveDirection == CubeSpawner.MoveDirection.Z)
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
        if (MoveDirection == CubeSpawner.MoveDirection.Z)
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

        if (MoveDirection == CubeSpawner.MoveDirection.Z)
        {
            cube.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, fallingCubeSize);
            cube.transform.position = new Vector3(transform.position.x, transform.position.y, fallingCubePositionZ);
        }
        else
        {
            cube.transform.localScale = new Vector3(fallingCubeSize,transform.localScale.y, transform.localScale.z);
            cube.transform.position = new Vector3(fallingCubePositionZ, transform.position.y, transform.position.z);
        }

       

        cube.AddComponent<Rigidbody>();
        cube.GetComponent<Renderer>().material.color = GetComponent<Renderer>().material.color;
        Destroy(cube.gameObject,2f);
    }

    private Color GetRandomColor()
    {
        return new Color(UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f));
    }
}
