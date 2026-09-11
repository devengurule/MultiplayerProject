using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    [Header("Grid Coordinates")]
    [SerializeField] private Vector2 topLeft;
    [SerializeField] private Vector2 bottomRight;

    private List<List<bool>> coinSpawnGrid = new();

    private void Start()
    {
        
    }

    private void InitializeGrid()
    {

    }
}
