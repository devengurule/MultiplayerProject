using System;
using UnityEngine;

public class GridController : MonoBehaviour
{
    [Tooltip("Layers To Perform Collision Check")]
    [SerializeField] private LayerMask collisionLayer;

    [Header("Grid Coordinates")]
    [SerializeField] private Vector2Int gridDimensions;

    public static bool[,] grid { get; private set; }

    public static event Action OnGridInitalized;

    private void Start()
    {
        InitializeGrid();
    }

    private void InitializeGrid()
    {
        grid = new bool[gridDimensions.x, gridDimensions.y];

        for (int y = 0; y < gridDimensions.y; y++)
        {
            for(int x = 0; x < gridDimensions.x; x++)
            {
                Vector3 collisionCheckPosition = new(x, 0, y);

                Vector3 boxHalfDimensions = new(0.5f, 0.5f, 0.5f);

                bool isHit = Physics.CheckBox(collisionCheckPosition, boxHalfDimensions, Quaternion.identity, collisionLayer);

                grid[x, y] = !isHit;
            }
        }

        OnGridInitalized?.Invoke();
    }
}
