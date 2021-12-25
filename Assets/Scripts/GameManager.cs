using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action OnCubeSpawned = delegate { };

    [SerializeField] private CubeSpawner[] spawners;

    private CubeSpawner currentSpawner;

    private int spawnerIndex;

    private void Start()
    {
        spawners = FindObjectsOfType<CubeSpawner>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Spawn();
        }
    }

    private void Spawn()
    {
        if (MovingCube.CurrentCube != null)
        {
            MovingCube.CurrentCube.Stop();
        }

        spawnerIndex = UnityEngine.Random.Range(0,4);

        currentSpawner = spawners[spawnerIndex];

        currentSpawner.SpawnCube();

        OnCubeSpawned();
    }
}
