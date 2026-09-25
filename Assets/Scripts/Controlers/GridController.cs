using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GridController : MonoBehaviour
{
    [Tooltip("Layers To Perform Collision Check")]
    [SerializeField] private LayerMask collisionLayer;

    [Header("Grid Coordinates")]
    [SerializeField] private Vector2Int gridDimensions;

    public bool[,] grid { get; private set; }

    public event Action OnGridInitalized;

    private void Start() => InitializeGrid();
    
    private void OnEnable() => CollectibleSpawner.ReInitalizeGrid += InitializeGrid;
    
    private void OnDisable() => CollectibleSpawner.ReInitalizeGrid -= InitializeGrid;

    private void InitializeGrid()
    {
        grid = new bool[gridDimensions.x, gridDimensions.y];

        for (int y = 0; y < gridDimensions.y; y++)
        {
            for(int x = 0; x < gridDimensions.x; x++)
            {
                Vector3 collisionCheckPosition = new(x, 0, y);

                Vector3 boxHalfDimensions = new(0.5f, 2f, 0.5f);

                bool isHit = Physics.CheckBox(collisionCheckPosition, boxHalfDimensions, Quaternion.identity, collisionLayer);

                grid[x, y] = !isHit;
            }
        }

        OnGridInitalized?.Invoke();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(Vector3.one, new(0.5f, 2f, 0.5f));
    }
}
