using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private float spawnChance;
    [SerializeField] private GameObject coinFolder;

    private List<Vector2Int> spawnQueueList = new();

    private void OnEnable() => GridController.OnGridInitalized += SpawnCoins;

    private void OnDisable() => GridController.OnGridInitalized -= SpawnCoins;

    private void SpawnCoins()
    {
        Vector2Int gridDimensions = new(GridController.grid.GetLength(0), GridController.grid.GetLength(1));

        for (int y = 0; y < gridDimensions.y; y++)
        {
            for (int x = 0; x < gridDimensions.x; x++)
            {
                if (GridController.grid[x, y])
                {
                    spawnQueueList.Add(new Vector2Int(x, y));
                }
            }
        }

        foreach(Vector2Int item in ShuffleSpawnQueue(spawnQueueList))
        {
            InstantiateCoin(item.x, item.y);
        }
    }

    private List<Vector2Int> ShuffleSpawnQueue(List<Vector2Int> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            
            Vector2Int temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
        return list;
    }

    private void InstantiateCoin(int x, int z)
    {
        if (Random.Range(0f, 1f) <= spawnChance)
        {
            GameObject coin = Instantiate(coinPrefab, new(x, 0.25f, z), Quaternion.identity);
            coin.transform.parent = coinFolder.transform;
        }
    }
}
